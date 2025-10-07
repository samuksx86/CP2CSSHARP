namespace RestauranteModel;

public class Usuario
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Email { get; set; }
    public required string Senha { get; set; }
    public required string Nome { get; set; }
    public DateTime DataCadastro { get; set; } = DateTime.Now;
}
