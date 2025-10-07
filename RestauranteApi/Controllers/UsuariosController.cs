using Microsoft.AspNetCore.Mvc;
using RestauranteModel;
using RestauranteBusiness;

namespace RestauranteApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _service;

    public UsuariosController(IUsuarioService service)
    {
        _service = service;
    }

    [HttpPost("login")]
    public async Task<ActionResult<Usuario>> Login([FromBody] LoginRequest request)
    {
        var usuario = await _service.ValidarLoginAsync(request.Email, request.Senha);
        if (usuario == null)
            return Unauthorized();

        return Ok(usuario);
    }

    [HttpPost("register")]
    public async Task<ActionResult<Usuario>> Register(Usuario usuario)
    {
        var createdUsuario = await _service.CreateAsync(usuario);
        return CreatedAtAction(nameof(Login), new { email = createdUsuario.Email }, createdUsuario);
    }
}

public record LoginRequest(string Email, string Senha);
