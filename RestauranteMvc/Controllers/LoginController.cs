using Microsoft.AspNetCore.Mvc;
using RestauranteModel;
using System.Text;
using System.Text.Json;

namespace RestauranteMvc.Controllers;

public class LoginController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public LoginController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(string email, string senha)
    {
        var client = _httpClientFactory.CreateClient("RestauranteApi");

        var loginRequest = new { Email = email, Senha = senha };
        var content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/usuarios/login", content);

        if (response.IsSuccessStatusCode)
        {
            var usuario = await response.Content.ReadFromJsonAsync<Usuario>();
            HttpContext.Session.SetString("UsuarioId", usuario!.Id);
            HttpContext.Session.SetString("UsuarioNome", usuario.Nome);

            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Email ou senha inválidos";
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
}
