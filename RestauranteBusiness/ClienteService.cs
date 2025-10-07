using Microsoft.EntityFrameworkCore;
using RestauranteData;
using RestauranteModel;

namespace RestauranteBusiness;

public class ClienteService : IClienteService
{
    private readonly ApplicationDbContext _context;

    public ClienteService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        return await _context.Clientes.ToListAsync();
    }

    public async Task<Cliente?> GetByIdAsync(string id)
    {
        return await _context.Clientes.FindAsync(id);
    }

    public async Task<Cliente> CreateAsync(Cliente cliente)
    {
        cliente.Id = Guid.NewGuid().ToString();
        cliente.DataCadastro = DateTime.Now;
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task<Cliente?> UpdateAsync(string id, Cliente cliente)
    {
        var existingCliente = await _context.Clientes.FindAsync(id);
        if (existingCliente == null)
            return null;

        existingCliente.Nome = cliente.Nome;
        existingCliente.Email = cliente.Email;
        existingCliente.Telefone = cliente.Telefone;
        existingCliente.Endereco = cliente.Endereco;
        existingCliente.Complemento = cliente.Complemento;

        await _context.SaveChangesAsync();
        return existingCliente;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null)
            return false;

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
        return true;
    }
}
