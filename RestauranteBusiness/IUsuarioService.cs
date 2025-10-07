using RestauranteModel;

namespace RestauranteBusiness;

public interface IUsuarioService
{
    Task<Usuario?> ValidarLoginAsync(string email, string senha);
    Task<Usuario> CreateAsync(Usuario usuario);
}
