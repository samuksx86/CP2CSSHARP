namespace RestauranteModel;

public class PedidoItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string PedidoId { get; set; }
    public Pedido? Pedido { get; set; }
    public required string ProdutoId { get; set; }
    public Produto? Produto { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal Subtotal => Quantidade * PrecoUnitario;
}
