namespace RestauranteModel;

public class Cliente
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public required string Telefone { get; set; }
    public required string Endereco { get; set; }
    public string? Complemento { get; set; }
    public DateTime DataCadastro { get; set; } = DateTime.Now;

    // Navegação
    public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
