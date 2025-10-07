using RestauranteModel;

namespace RestauranteBusiness;

public interface IClienteService
{
    Task<IEnumerable<Cliente>> GetAllAsync();
    Task<Cliente?> GetByIdAsync(string id);
    Task<Cliente> CreateAsync(Cliente cliente);
    Task<Cliente?> UpdateAsync(string id, Cliente cliente);
    Task<bool> DeleteAsync(string id);
}
