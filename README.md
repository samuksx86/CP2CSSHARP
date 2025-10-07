# Sistema de Restaurante / Delivery

> ⚠️ **IMPORTANTE:** Antes de começar, leia o arquivo [INSTRUÇÕES_IMPORTANTES.md](INSTRUÇÕES_IMPORTANTES.md) para evitar erros comuns na configuração do Oracle!

## 👥 Integrantes

- **RM551717** - Enricco Rossi
- **RM99134** - Samuel Ramos

## 🎯 Tema

Sistema de Gestão para Restaurante com Delivery, desenvolvido em .NET 9 com arquitetura MVC + API.

## 📋 Descrição do Projeto

Sistema completo de gerenciamento de restaurante e delivery que implementa:

- **CRUD Completo** para Cliente e Produto
- **Sistema de Login** com autenticação e redirecionamento
- **Gestão de Pedidos** com fluxo de status (Aberto → Em Preparo → Saiu para Entrega → Entregue)
- **Gestão de Entregas** com controle de motoboys
- **API RESTful** consumida pelo frontend MVC
- **Banco de Dados Oracle** com Entity Framework Core

## 🏗️ Arquitetura

O projeto é dividido em camadas:

```
RestauranteDelivery/
├── RestauranteModel/      # Entidades do domínio
├── RestauranteData/       # Contexto EF Core e Migrations
├── RestauranteBusiness/   # Lógica de negócio e Services
├── RestauranteApi/        # API RESTful
└── RestauranteMvc/        # Interface Web MVC
```

## 🗂️ Entidades

### Obrigatórias
1. **Cliente** - Gerenciamento de clientes do restaurante
2. **Produto** - Cardápio e produtos disponíveis

### Adicionais
3. **Pedido** - Pedidos realizados pelos clientes
4. **PedidoItem** - Itens individuais do pedido
5. **Entrega** - Controle de entregas e motoboys
6. **Usuario** - Autenticação no sistema

## 🔄 Regras de Negócio

### Fluxo de Status do Pedido
O pedido segue um fluxo sequencial obrigatório:
1. **Aberto** - Pedido criado
2. **Em Preparo** - Pedido sendo preparado
3. **Saiu para Entrega** - Pedido a caminho
4. **Entregue** - Pedido concluído

### Restrições
- Pedidos com status **Entregue** são **somente leitura**
- Não é possível editar ou excluir pedidos entregues
- A transição de status deve seguir a ordem sequencial

## 🚀 Como Rodar o Projeto

### Pré-requisitos

1. **.NET 9 SDK** - [Download](https://dotnet.microsoft.com/download/dotnet/9.0)
2. **Oracle Database** - Pode ser Oracle Express Edition (XE)
3. **Git** (opcional)

### Configuração do Banco de Dados Oracle

1. Instale o Oracle Database XE (se ainda não tiver)
2. Verifique se o Oracle está rodando na porta `1521`
3. O sistema usa as seguintes credenciais por padrão:
   - **User Id**: `system`
   - **Password**: `oracle`
   - **Data Source**: `oracle.fiap.com.br:1521/ORCL`

> **Nota**: Você pode alterar a connection string nos arquivos `appsettings.json` da API e no `ApplicationDbContextFactory.cs`

### Passo a Passo

#### 1. Clone ou baixe o projeto

```bash
cd RestauranteDelivery
```

#### 2. Limpar Tabelas Antigas (se necessário)

Se você já tentou rodar as migrations antes e teve erro, execute o script de limpeza:

**Opção A - Via SQL Developer/SQL*Plus:**
```sql
-- Execute o arquivo drop_tables.sql no Oracle SQL Developer
-- O arquivo está na raiz do projeto RestauranteDelivery
```

**Opção B - Via linha de comando:**
```bash
# Conecte ao Oracle e execute:
sqlplus system/oracle@localhost:1521/XEPDB1 @drop_tables.sql
```

#### 3. Aplicar as Migrations no Banco de Dados

```bash
cd RestauranteData
dotnet ef database update --startup-project ../RestauranteApi
cd ..
```

Este comando criará todas as tabelas necessárias no Oracle.

#### 4. Popular o Banco com Dados Iniciais

Execute o script SQL para criar usuário admin e dados de exemplo:

**Via SQL Developer/SQL*Plus:**
```bash
# Execute o arquivo setup_database.sql
sqlplus system/oracle@localhost:1521/XEPDB1 @setup_database.sql
```

**OU manualmente via SQL Developer:**
- Abra e execute o arquivo `setup_database.sql`

Este script irá criar:
- **1 usuário administrador** para login
- **2 clientes** de exemplo
- **5 produtos** de exemplo (pizzas, lanches, bebidas, saladas)

**Credenciais de acesso:**
- Email: `admin@restaurante.com`
- Senha: `admin123`

#### 5. Rodar a API

Abra um terminal e execute:

```bash
cd RestauranteApi
dotnet run
```

A API estará disponível em `http://localhost:5209` (ou a porta indicada no console)

#### 6. Rodar o MVC (em outro terminal)

Abra outro terminal e execute:

```bash
cd RestauranteMvc
dotnet run
```

A aplicação web estará disponível em `http://localhost:5209` (ou a porta indicada no console)

#### 7. Acessar o Sistema

1. Abra o navegador em `http://localhost:5065`
2. Faça login com as credenciais:
   - **Email**: `admin@restaurante.com`
   - **Senha**: `admin123`
3. Navegue pelas funcionalidades:
   - **Clientes** - Cadastro completo de clientes
   - **Produtos** - Gerenciamento do cardápio

## 🔧 Configurações Importantes

### Ajustar a URL da API (se necessário)

A API por padrão roda em `http://localhost:5209`. Se estiver em uma porta diferente, ajuste em `RestauranteMvc/appsettings.json`:

```json
{
  "ApiSettings": {
    "BaseUrl": "http://localhost:5209"
  }
}
```

> **Nota:** Verifique a porta real no console ao executar `dotnet run` na API.

### Connection String do Oracle

Se precisar alterar as credenciais do Oracle:

1. Em `RestauranteApi/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=localhost:1521/XEPDB1"
  }
}
```

2. Em `RestauranteData/ApplicationDbContextFactory.cs`:
```csharp
optionsBuilder.UseOracle("User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=localhost:1521/XEPDB1");
```

## 📊 Endpoints da API

### Clientes
- `GET /api/clientes` - Lista todos os clientes
- `GET /api/clientes/{id}` - Busca cliente por ID
- `POST /api/clientes` - Cria novo cliente
- `PUT /api/clientes/{id}` - Atualiza cliente
- `DELETE /api/clientes/{id}` - Remove cliente

### Produtos
- `GET /api/produtos` - Lista todos os produtos
- `GET /api/produtos/{id}` - Busca produto por ID
- `POST /api/produtos` - Cria novo produto
- `PUT /api/produtos/{id}` - Atualiza produto
- `DELETE /api/produtos/{id}` - Remove produto

### Pedidos
- `GET /api/pedidos` - Lista todos os pedidos
- `GET /api/pedidos/{id}` - Busca pedido por ID
- `POST /api/pedidos` - Cria novo pedido
- `PUT /api/pedidos/{id}` - Atualiza pedido
- `PUT /api/pedidos/{id}/status` - Atualiza status do pedido
- `DELETE /api/pedidos/{id}` - Remove pedido

### Usuários
- `POST /api/usuarios/login` - Efetua login
- `POST /api/usuarios/register` - Registra novo usuário

## 🔗 Exemplos de cURL

### **Usuários**

#### Login
```bash
curl -X POST http://localhost:5000/api/Usuarios/login \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@restaurante.com","senha":"admin123"}'
```

#### Registro
```bash
curl -X POST http://localhost:5000/api/Usuarios/register \
  -H "Content-Type: application/json" \
  -d '{"nome":"João Silva","email":"joao@email.com","senha":"senha123"}'
```

### **Clientes**

#### Listar todos
```bash
curl -X GET http://localhost:5000/api/Clientes
```

#### Buscar por ID
```bash
curl -X GET http://localhost:5000/api/Clientes/{id}
```

#### Criar cliente
```bash
curl -X POST http://localhost:5000/api/Clientes \
  -H "Content-Type: application/json" \
  -d '{
    "nome":"Maria Santos",
    "email":"maria@email.com",
    "telefone":"11999999999",
    "endereco":"Rua das Flores, 123",
    "complemento":"Apto 45"
  }'
```

#### Atualizar cliente
```bash
curl -X PUT http://localhost:5000/api/Clientes/{id} \
  -H "Content-Type: application/json" \
  -d '{
    "id":"{id}",
    "nome":"Maria Santos Silva",
    "email":"maria@email.com",
    "telefone":"11988888888",
    "endereco":"Rua das Flores, 123",
    "complemento":"Apto 45"
  }'
```

#### Deletar cliente
```bash
curl -X DELETE http://localhost:5000/api/Clientes/{id}
```

### **Produtos**

#### Listar todos
```bash
curl -X GET http://localhost:5000/api/Produtos
```

#### Buscar por ID
```bash
curl -X GET http://localhost:5000/api/Produtos/{id}
```

#### Criar produto
```bash
curl -X POST http://localhost:5000/api/Produtos \
  -H "Content-Type: application/json" \
  -d '{
    "nome":"Pizza Margherita",
    "descricao":"Pizza tradicional com molho de tomate, mussarela e manjericão",
    "preco":45.90,
    "categoria":"Pizzas",
    "disponivel":true
  }'
```

#### Atualizar produto
```bash
curl -X PUT http://localhost:5000/api/Produtos/{id} \
  -H "Content-Type: application/json" \
  -d '{
    "id":"{id}",
    "nome":"Pizza Margherita Grande",
    "descricao":"Pizza tradicional com molho de tomate, mussarela e manjericão",
    "preco":55.90,
    "categoria":"Pizzas",
    "disponivel":true
  }'
```

#### Deletar produto
```bash
curl -X DELETE http://localhost:5000/api/Produtos/{id}
```

### **Pedidos**

#### Listar todos
```bash
curl -X GET http://localhost:5000/api/Pedidos
```

#### Buscar por ID
```bash
curl -X GET http://localhost:5000/api/Pedidos/{id}
```

#### Criar pedido
```bash
curl -X POST http://localhost:5000/api/Pedidos \
  -H "Content-Type: application/json" \
  -d '{
    "clienteId":"{clienteId}",
    "observacoes":"Sem cebola",
    "itens":[
      {
        "produtoId":"{produtoId}",
        "quantidade":2,
        "precoUnitario":45.90
      }
    ]
  }'
```

#### Atualizar pedido
```bash
curl -X PUT http://localhost:5000/api/Pedidos/{id} \
  -H "Content-Type: application/json" \
  -d '{
    "id":"{id}",
    "clienteId":"{clienteId}",
    "observacoes":"Sem cebola e sem azeitona",
    "itens":[
      {
        "produtoId":"{produtoId}",
        "quantidade":3,
        "precoUnitario":45.90
      }
    ]
  }'
```

#### Atualizar status
```bash
curl -X PUT http://localhost:5000/api/Pedidos/{id}/status \
  -H "Content-Type: application/json" \
  -d '2'
```

**Status disponíveis:**
- `1` = Aberto
- `2` = EmPreparo
- `3` = SaiuParaEntrega
- `4` = Entregue

#### Deletar pedido
```bash
curl -X DELETE http://localhost:5000/api/Pedidos/{id}
```

> **Nota:** Substitua `{id}`, `{clienteId}` e `{produtoId}` pelos IDs reais retornados pela API. Ajuste a porta conforme configuração do seu ambiente (verifique no console ao rodar a API).

## 📝 Tecnologias Utilizadas

- **.NET 9** - Framework principal
- **ASP.NET Core MVC** - Frontend
- **ASP.NET Core Web API** - Backend
- **Entity Framework Core 9** - ORM
- **Oracle.EntityFrameworkCore** - Provider Oracle
- **Bootstrap 5** - UI Framework
- **Oracle Database** - Banco de dados

## ✅ Checklist de Avaliação

- [x] **2 CRUDs completos** (Cliente e Produto) - 4 pontos
- [x] **Login + redirecionamento** - 1,5 pontos
- [x] **Oracle via EF Core** (mapeamento, migration, uso real) - 2 pontos
- [x] **Layout/UX simples e limpo** - 2 pontos
- [x] **README/Organização** - 0,5 ponto

## 🐛 Troubleshooting

### Erro de conexão com Oracle
- Verifique se o Oracle está rodando
- Confirme as credenciais e a connection string
- Teste a conexão usando SQL Developer ou similar

### Porta já em uso
- Altere as portas em `Properties/launchSettings.json` de cada projeto

### Migrations não aplicadas
```bash
cd RestauranteData
dotnet ef database update --startup-project ../RestauranteApi
```

### Erro de CORS
- Verifique se a API está configurada com CORS
- Confirme se a URL da API no MVC está correta

## 📞 Suporte

Em caso de dúvidas, entre em contato com os desenvolvedores:
- Enricco Rossi - RM551717
- Samuel Ramos - RM99134
