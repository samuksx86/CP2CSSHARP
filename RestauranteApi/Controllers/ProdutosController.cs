using Microsoft.AspNetCore.Mvc;
using RestauranteModel;
using RestauranteBusiness;

namespace RestauranteApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _service;

    public ProdutosController(IProdutoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> GetAll()
    {
        var produtos = await _service.GetAllAsync();
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Produto>> GetById(string id)
    {
        var produto = await _service.GetByIdAsync(id);
        if (produto == null)
            return NotFound();

        return Ok(produto);
    }

    [HttpPost]
    public async Task<ActionResult<Produto>> Create(Produto produto)
    {
        var createdProduto = await _service.CreateAsync(produto);
        return CreatedAtAction(nameof(GetById), new { id = createdProduto.Id }, createdProduto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Produto>> Update(string id, Produto produto)
    {
        var updatedProduto = await _service.UpdateAsync(id, produto);
        if (updatedProduto == null)
            return NotFound();

        return Ok(updatedProduto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var result = await _service.DeleteAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}
