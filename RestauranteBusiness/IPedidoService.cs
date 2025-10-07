using RestauranteModel;

namespace RestauranteBusiness;

public interface IPedidoService
{
    Task<IEnumerable<Pedido>> GetAllAsync();
    Task<Pedido?> GetByIdAsync(string id);
    Task<Pedido> CreateAsync(Pedido pedido);
    Task<Pedido?> UpdateAsync(string id, Pedido pedido);
    Task<bool> DeleteAsync(string id);
    Task<bool> AtualizarStatusAsync(string id, StatusPedido novoStatus);
}
