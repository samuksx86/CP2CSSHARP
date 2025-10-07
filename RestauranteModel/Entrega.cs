namespace RestauranteModel;

public class Entrega
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string PedidoId { get; set; }
    public Pedido? Pedido { get; set; }
    public required string NomeMotoboy { get; set; }
    public required string TelefoneMotoboy { get; set; }
    public DateTime? DataSaida { get; set; }
    public DateTime? DataEntrega { get; set; }
    public string? ObservacoesEntrega { get; set; }
}
