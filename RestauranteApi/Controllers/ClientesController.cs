using Microsoft.AspNetCore.Mvc;
using RestauranteModel;
using RestauranteBusiness;

namespace RestauranteApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _service;

    public ClientesController(IClienteService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetAll()
    {
        var clientes = await _service.GetAllAsync();
        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> GetById(string id)
    {
        var cliente = await _service.GetByIdAsync(id);
        if (cliente == null)
            return NotFound();

        return Ok(cliente);
    }

    [HttpPost]
    public async Task<ActionResult<Cliente>> Create(Cliente cliente)
    {
        var createdCliente = await _service.CreateAsync(cliente);
        return CreatedAtAction(nameof(GetById), new { id = createdCliente.Id }, createdCliente);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Cliente>> Update(string id, Cliente cliente)
    {
        var updatedCliente = await _service.UpdateAsync(id, cliente);
        if (updatedCliente == null)
            return NotFound();

        return Ok(updatedCliente);
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
