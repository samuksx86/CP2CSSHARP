using Microsoft.AspNetCore.Mvc;
using RestauranteModel;
using RestauranteBusiness;

namespace RestauranteApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly IPedidoService _service;

    public PedidosController(IPedidoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pedido>>> GetAll()
    {
        var pedidos = await _service.GetAllAsync();
        return Ok(pedidos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Pedido>> GetById(string id)
    {
        var pedido = await _service.GetByIdAsync(id);
        if (pedido == null)
            return NotFound();

        return Ok(pedido);
    }

    [HttpPost]
    public async Task<ActionResult<Pedido>> Create(Pedido pedido)
    {
        try
        {
            var createdPedido = await _service.CreateAsync(pedido);
            return CreatedAtAction(nameof(GetById), new { id = createdPedido.Id }, createdPedido);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Pedido>> Update(string id, Pedido pedido)
    {
        try
        {
            var updatedPedido = await _service.UpdateAsync(id, pedido);
            if (updatedPedido == null)
                return NotFound();

            return Ok(updatedPedido);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}/status")]
    public async Task<ActionResult> UpdateStatus(string id, [FromBody] StatusPedido novoStatus)
    {
        try
        {
            var result = await _service.AtualizarStatusAsync(id, novoStatus);
            if (!result)
                return NotFound();

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
