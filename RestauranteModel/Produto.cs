namespace RestauranteModel;

public class Produto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public decimal Preco { get; set; }
    public required string Categoria { get; set; }
    public bool Disponivel { get; set; } = true;
    public DateTime DataCadastro { get; set; } = DateTime.Now;

    // Navegação
    public ICollection<PedidoItem> PedidoItens { get; set; } = new List<PedidoItem>();
}
