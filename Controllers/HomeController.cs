using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP06_Zilbersztein.Models;

namespace TP06_Zilbersztein.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult MaMeIg()
    {
        return View("mameig");
    }
    public IActionResult MaMeIgSolo()
    {
        return View("mameigsolo");
    }
    public IActionResult nivelmameig()
    {
        return View("nivelmameig");
    }
    public IActionResult GuardarNivel(int numJuego, string nivel)
    {
        Informacion.EstablecerNivel(numJuego, nivel);
        return View("");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
