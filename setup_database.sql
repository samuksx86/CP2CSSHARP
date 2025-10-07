-- Script completo para configurar o banco de dados Oracle
-- Execute este script DEPOIS de rodar: dotnet ef database update

-- 1. Inserir usuário administrador para login
-- Senha: admin123 (SEM hash, texto plano)
INSERT INTO USUARIOS (ID, EMAIL, SENHA, NOME, DATACADASTRO)
VALUES (
  'admin-001',
  'admin@restaurante.com',
  'admin123',
  'Administrador',
  SYSDATE
);

-- 2. Inserir alguns clientes de exemplo
INSERT INTO CLIENTES (ID, NOME, EMAIL, TELEFONE, ENDERECO, COMPLEMENTO, DATACADASTRO)
VALUES (
  'cliente-001',
  'João Silva',
  'joao@email.com',
  '11987654321',
  'Rua das Flores, 123',
  'Apto 45',
  SYSDATE
);

INSERT INTO CLIENTES (ID, NOME, EMAIL, TELEFONE, ENDERECO, COMPLEMENTO, DATACADASTRO)
VALUES (
  'cliente-002',
  'Maria Santos',
  'maria@email.com',
  '11976543210',
  'Av. Paulista, 1000',
  'Cobertura',
  SYSDATE
);

-- 3. Inserir alguns produtos de exemplo
INSERT INTO PRODUTOS (ID, NOME, DESCRICAO, PRECO, CATEGORIA, DISPONIVEL, DATACADASTRO)
VALUES (
  'produto-001',
  'Pizza Margherita',
  'Pizza tradicional com molho de tomate, mussarela e manjericão',
  45.90,
  'Pizzas',
  1,
  SYSDATE
);

INSERT INTO PRODUTOS (ID, NOME, DESCRICAO, PRECO, CATEGORIA, DISPONIVEL, DATACADASTRO)
VALUES (
  'produto-002',
  'Pizza Calabresa',
  'Pizza com calabresa, cebola e azeitonas',
  48.90,
  'Pizzas',
  1,
  SYSDATE
);

INSERT INTO PRODUTOS (ID, NOME, DESCRICAO, PRECO, CATEGORIA, DISPONIVEL, DATACADASTRO)
VALUES (
  'produto-003',
  'Hambúrguer Artesanal',
  'Hambúrguer 180g com queijo, alface, tomate e molho especial',
  32.90,
  'Lanches',
  1,
  SYSDATE
);

INSERT INTO PRODUTOS (ID, NOME, DESCRICAO, PRECO, CATEGORIA, DISPONIVEL, DATACADASTRO)
VALUES (
  'produto-004',
  'Refrigerante Lata',
  'Refrigerante 350ml - diversos sabores',
  5.50,
  'Bebidas',
  1,
  SYSDATE
);

INSERT INTO PRODUTOS (ID, NOME, DESCRICAO, PRECO, CATEGORIA, DISPONIVEL, DATACADASTRO)
VALUES (
  'produto-005',
  'Salada Caesar',
  'Alface, croutons, parmesão e molho caesar',
  28.90,
  'Saladas',
  1,
  SYSDATE
);

COMMIT;

-- Verificar dados inseridos
SELECT 'Usuários cadastrados:' AS INFO FROM DUAL;
SELECT EMAIL, NOME FROM USUARIOS;

SELECT 'Clientes cadastrados:' AS INFO FROM DUAL;
SELECT NOME, EMAIL, TELEFONE FROM CLIENTES;

SELECT 'Produtos cadastrados:' AS INFO FROM DUAL;
SELECT NOME, CATEGORIA, PRECO FROM PRODUTOS;
