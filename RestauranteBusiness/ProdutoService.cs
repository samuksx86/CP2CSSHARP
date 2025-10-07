using Microsoft.EntityFrameworkCore;
using RestauranteData;
using RestauranteModel;

namespace RestauranteBusiness;

public class ProdutoService : IProdutoService
{
    private readonly ApplicationDbContext _context;

    public ProdutoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Produto>> GetAllAsync()
    {
        return await _context.Produtos.ToListAsync();
    }

    public async Task<Produto?> GetByIdAsync(string id)
    {
        return await _context.Produtos.FindAsync(id);
    }

    public async Task<Produto> CreateAsync(Produto produto)
    {
        produto.Id = Guid.NewGuid().ToString();
        produto.DataCadastro = DateTime.Now;
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
        return produto;
    }

    public async Task<Produto?> UpdateAsync(string id, Produto produto)
    {
        var existingProduto = await _context.Produtos.FindAsync(id);
        if (existingProduto == null)
            return null;

        existingProduto.Nome = produto.Nome;
        existingProduto.Descricao = produto.Descricao;
        existingProduto.Preco = produto.Preco;
        existingProduto.Categoria = produto.Categoria;
        existingProduto.Disponivel = produto.Disponivel;

        await _context.SaveChangesAsync();
        return existingProduto;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
            return false;

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();
        return true;
    }
}
