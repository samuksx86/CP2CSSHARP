using RestauranteModel;

namespace RestauranteBusiness;

public interface IProdutoService
{
    Task<IEnumerable<Produto>> GetAllAsync();
    Task<Produto?> GetByIdAsync(string id);
    Task<Produto> CreateAsync(Produto produto);
    Task<Produto?> UpdateAsync(string id, Produto produto);
    Task<bool> DeleteAsync(string id);
}
