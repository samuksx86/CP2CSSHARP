namespace RestauranteModel;

public class Pedido
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string ClienteId { get; set; }
    public Cliente? Cliente { get; set; }
    public DateTime DataPedido { get; set; } = DateTime.Now;
    public StatusPedido Status { get; set; } = StatusPedido.Aberto;
    public decimal ValorTotal { get; set; }
    public string? Observacoes { get; set; }

    // Navegação
    public ICollection<PedidoItem> Itens { get; set; } = new List<PedidoItem>();
    public Entrega? Entrega { get; set; }

    public bool IsSomenteLeitura() => Status == StatusPedido.Entregue;
}
