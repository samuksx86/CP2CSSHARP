using Microsoft.AspNetCore.Mvc;
using RestauranteModel;
using System.Text;
using System.Text.Json;

namespace RestauranteMvc.Controllers;

public class ClientesController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ClientesController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private bool IsAuthenticated()
    {
        return !string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioId"));
    }

    public async Task<IActionResult> Index()
    {
        if (!IsAuthenticated())
            return RedirectToAction("Index", "Login");

        var client = _httpClientFactory.CreateClient("RestauranteApi");
        var response = await client.GetAsync("/api/clientes");

        if (response.IsSuccessStatusCode)
        {
            var clientes = await response.Content.ReadFromJsonAsync<List<Cliente>>();
            return View(clientes);
        }

        return View(new List<Cliente>());
    }

    public IActionResult Create()
    {
        if (!IsAuthenticated())
            return RedirectToAction("Index", "Login");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Cliente cliente)
    {
        if (!IsAuthenticated())
            return RedirectToAction("Index", "Login");

        if (!ModelState.IsValid)
            return View(cliente);

        var client = _httpClientFactory.CreateClient("RestauranteApi");
        var content = new StringContent(JsonSerializer.Serialize(cliente), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/clientes", content);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        ViewBag.Error = "Erro ao criar cliente";
        return View(cliente);
    }

    public async Task<IActionResult> Edit(string id)
    {
        if (!IsAuthenticated())
            return RedirectToAction("Index", "Login");

        var client = _httpClientFactory.CreateClient("RestauranteApi");
        var response = await client.GetAsync($"/api/clientes/{id}");

        if (response.IsSuccessStatusCode)
        {
            var cliente = await response.Content.ReadFromJsonAsync<Cliente>();
            return View(cliente);
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, Cliente cliente)
    {
        if (!IsAuthenticated())
            return RedirectToAction("Index", "Login");

        if (!ModelState.IsValid)
            return View(cliente);

        var client = _httpClientFactory.CreateClient("RestauranteApi");
        var content = new StringContent(JsonSerializer.Serialize(cliente), Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"/api/clientes/{id}", content);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        ViewBag.Error = "Erro ao editar cliente";
        return View(cliente);
    }

    public async Task<IActionResult> Delete(string id)
    {
        if (!IsAuthenticated())
            return RedirectToAction("Index", "Login");

        var client = _httpClientFactory.CreateClient("RestauranteApi");
        await client.DeleteAsync($"/api/clientes/{id}");

        return RedirectToAction("Index");
    }
}
