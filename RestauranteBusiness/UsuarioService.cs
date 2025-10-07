using Microsoft.EntityFrameworkCore;
using RestauranteData;
using RestauranteModel;

namespace RestauranteBusiness;

public class UsuarioService : IUsuarioService
{
    private readonly ApplicationDbContext _context;

    public UsuarioService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> ValidarLoginAsync(string email, string senha)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);
    }

    public async Task<Usuario> CreateAsync(Usuario usuario)
    {
        usuario.Id = Guid.NewGuid().ToString();
        usuario.DataCadastro = DateTime.Now;

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }
}
