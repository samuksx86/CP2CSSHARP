using Microsoft.EntityFrameworkCore;
using RestauranteModel;

namespace RestauranteData;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoItem> PedidoItens { get; set; }
    public DbSet<Entrega> Entregas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração Cliente
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("CLIENTES");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID").HasMaxLength(50);
            entity.Property(e => e.Nome).HasColumnName("NOME").IsRequired().HasMaxLength(200);
            entity.Property(e => e.Email).HasColumnName("EMAIL").IsRequired().HasMaxLength(200);
            entity.Property(e => e.Telefone).HasColumnName("TELEFONE").IsRequired().HasMaxLength(20);
            entity.Property(e => e.Endereco).HasColumnName("ENDERECO").IsRequired().HasMaxLength(500);
            entity.Property(e => e.Complemento).HasColumnName("COMPLEMENTO").HasMaxLength(200);
            entity.Property(e => e.DataCadastro).HasColumnName("DATACADASTRO");
        });

        // Configuração Produto
        modelBuilder.Entity<Produto>(entity =>
        {
            entity.ToTable("PRODUTOS");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID").HasMaxLength(50);
            entity.Property(e => e.Nome).HasColumnName("NOME").IsRequired().HasMaxLength(200);
            entity.Property(e => e.Descricao).HasColumnName("DESCRICAO").IsRequired().HasMaxLength(500);
            entity.Property(e => e.Preco).HasColumnName("PRECO").HasColumnType("decimal(18,2)");
            entity.Property(e => e.Categoria).HasColumnName("CATEGORIA").IsRequired().HasMaxLength(100);
            entity.Property(e => e.Disponivel).HasColumnName("DISPONIVEL").IsRequired().HasColumnType("NUMBER(1)");
            entity.Property(e => e.DataCadastro).HasColumnName("DATACADASTRO");
        });

        // Configuração Pedido
        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.ToTable("PEDIDOS");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID").HasMaxLength(50);
            entity.Property(e => e.ClienteId).HasColumnName("CLIENTEID").IsRequired().HasMaxLength(50);
            entity.Property(e => e.DataPedido).HasColumnName("DATAPEDIDO");
            entity.Property(e => e.Status).HasColumnName("STATUS").IsRequired();
            entity.Property(e => e.ValorTotal).HasColumnName("VALORTOTAL").HasColumnType("decimal(18,2)");
            entity.Property(e => e.Observacoes).HasColumnName("OBSERVACOES").HasMaxLength(500);

            entity.HasOne(e => e.Cliente)
                  .WithMany(c => c.Pedidos)
                  .HasForeignKey(e => e.ClienteId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuração PedidoItem
        modelBuilder.Entity<PedidoItem>(entity =>
        {
            entity.ToTable("PEDIDO_ITENS");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID").HasMaxLength(50);
            entity.Property(e => e.PedidoId).HasColumnName("PEDIDOID").IsRequired().HasMaxLength(50);
            entity.Property(e => e.ProdutoId).HasColumnName("PRODUTOID").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Quantidade).HasColumnName("QUANTIDADE").IsRequired();
            entity.Property(e => e.PrecoUnitario).HasColumnName("PRECOUNITARIO").HasColumnType("decimal(18,2)");
            entity.Ignore(e => e.Subtotal);

            entity.HasOne(e => e.Pedido)
                  .WithMany(p => p.Itens)
                  .HasForeignKey(e => e.PedidoId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Produto)
                  .WithMany(p => p.PedidoItens)
                  .HasForeignKey(e => e.ProdutoId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuração Entrega
        modelBuilder.Entity<Entrega>(entity =>
        {
            entity.ToTable("ENTREGAS");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID").HasMaxLength(50);
            entity.Property(e => e.PedidoId).HasColumnName("PEDIDOID").IsRequired().HasMaxLength(50);
            entity.Property(e => e.NomeMotoboy).HasColumnName("NOMEMOTOBOY").IsRequired().HasMaxLength(200);
            entity.Property(e => e.TelefoneMotoboy).HasColumnName("TELEFONEMOTOBOY").IsRequired().HasMaxLength(20);
            entity.Property(e => e.DataSaida).HasColumnName("DATASAIDA");
            entity.Property(e => e.DataEntrega).HasColumnName("DATAENTREGA");
            entity.Property(e => e.ObservacoesEntrega).HasColumnName("OBSERVACOESENTREGA").HasMaxLength(500);

            entity.HasOne(e => e.Pedido)
                  .WithOne(p => p.Entrega)
                  .HasForeignKey<Entrega>(e => e.PedidoId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuração Usuario
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("USUARIOS");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID").HasMaxLength(50);
            entity.Property(e => e.Email).HasColumnName("EMAIL").IsRequired().HasMaxLength(200);
            entity.Property(e => e.Senha).HasColumnName("SENHA").IsRequired().HasMaxLength(500);
            entity.Property(e => e.Nome).HasColumnName("NOME").IsRequired().HasMaxLength(200);
            entity.Property(e => e.DataCadastro).HasColumnName("DATACADASTRO");
        });
    }
}
