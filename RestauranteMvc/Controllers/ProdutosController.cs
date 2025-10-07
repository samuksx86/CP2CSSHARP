using Microsoft.AspNetCore.Mvc;
using RestauranteModel;
using System.Text;
using System.Text.Json;

namespace RestauranteMvc.Controllers;

public class ProdutosController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProdutosController(IHttpClientFactory httpClientFactory)
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
        var response = await client.GetAsync("/api/produtos");

        if (response.IsSuccessStatusCode)
        {
            var produtos = await response.Content.ReadFromJsonAsync<List<Produto>>();
            return View(produtos);
        }

        return View(new List<Produto>());
    }

    public IActionResult Create()
    {
        if (!IsAuthenticated())
            return RedirectToAction("Index", "Login");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Produto produto)
    {
        if (!IsAuthenticated())
            return RedirectToAction("Index", "Login");

        if (!ModelState.IsValid)
        {
            var errors = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            ViewBag.Error = $"Validação falhou: {errors}";
            return View(produto);
        }

        var client = _httpClientFactory.CreateClient("RestauranteApi");
        var content = new StringContent(JsonSerializer.Serialize(produto), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/produtos", content);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        var errorContent = await response.Content.ReadAsStringAsync();
        ViewBag.Error = $"Erro ao criar produto: {response.StatusCode} - {errorContent}";
        return View(produto);
    }

    public async Task<IActionResult> Edit(string id)
    {
        if (!IsAuthenticated())
            return RedirectToAction("Index", "Login");

        var client = _httpClientFactory.CreateClient("RestauranteApi");
        var response = await client.GetAsync($"/api/produtos/{id}");

        if (response.IsSuccessStatusCode)
        {
            var produto = await response.Content.ReadFromJsonAsync<Produto>();
            return View(produto);
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, Produto produto)
    {
        if (!IsAuthenticated())
            return RedirectToAction("Index", "Login");

        if (!ModelState.IsValid)
            return View(produto);

        var client = _httpClientFactory.CreateClient("RestauranteApi");
        var content = new StringContent(JsonSerializer.Serialize(produto), Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"/api/produtos/{id}", content);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        ViewBag.Error = "Erro ao editar produto";
        return View(produto);
    }

    public async Task<IActionResult> Delete(string id)
    {
        if (!IsAuthenticated())
            return RedirectToAction("Index", "Login");

        var client = _httpClientFactory.CreateClient("RestauranteApi");
        await client.DeleteAsync($"/api/produtos/{id}");

        return RedirectToAction("Index");
    }
}
