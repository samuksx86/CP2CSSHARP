using Microsoft.EntityFrameworkCore;
using RestauranteData;
using RestauranteModel;

namespace RestauranteBusiness;

public class PedidoService : IPedidoService
{
    private readonly ApplicationDbContext _context;

    public PedidoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pedido>> GetAllAsync()
    {
        return await _context.Pedidos
            .Include(p => p.Cliente)
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .ToListAsync();
    }

    public async Task<Pedido?> GetByIdAsync(string id)
    {
        return await _context.Pedidos
            .Include(p => p.Cliente)
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .Include(p => p.Entrega)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Pedido> CreateAsync(Pedido pedido)
    {
        pedido.Id = Guid.NewGuid().ToString();
        pedido.DataPedido = DateTime.Now;
        pedido.Status = StatusPedido.Aberto;

        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();
        return pedido;
    }

    public async Task<Pedido?> UpdateAsync(string id, Pedido pedido)
    {
        var existingPedido = await _context.Pedidos.FindAsync(id);
        if (existingPedido == null)
            return null;

        // Regra: Pedido entregue é somente leitura
        if (existingPedido.IsSomenteLeitura())
            throw new InvalidOperationException("Pedido entregue não pode ser modificado");

        existingPedido.Observacoes = pedido.Observacoes;
        existingPedido.ValorTotal = pedido.ValorTotal;

        await _context.SaveChangesAsync();
        return existingPedido;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var pedido = await _context.Pedidos.FindAsync(id);
        if (pedido == null)
            return false;

        // Regra: Pedido entregue é somente leitura
        if (pedido.IsSomenteLeitura())
            throw new InvalidOperationException("Pedido entregue não pode ser excluído");

        _context.Pedidos.Remove(pedido);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AtualizarStatusAsync(string id, StatusPedido novoStatus)
    {
        var pedido = await _context.Pedidos.FindAsync(id);
        if (pedido == null)
            return false;

        // Regra: Pedido entregue é somente leitura
        if (pedido.IsSomenteLeitura())
            throw new InvalidOperationException("Pedido entregue não pode ter o status alterado");

        // Validar fluxo de status: Aberto → EmPreparo → SaiuParaEntrega → Entregue
        if (!IsValidStatusTransition(pedido.Status, novoStatus))
            throw new InvalidOperationException($"Transição de status inválida: {pedido.Status} para {novoStatus}");

        pedido.Status = novoStatus;
        await _context.SaveChangesAsync();
        return true;
    }

    private bool IsValidStatusTransition(StatusPedido statusAtual, StatusPedido novoStatus)
    {
        return novoStatus == (statusAtual + 1) || novoStatus == statusAtual;
    }
}
