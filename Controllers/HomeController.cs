using System.Diagnostics;
using System.Xml.Schema;
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
        Informacion.reestablecerValores();
        return View();
    }
    public IActionResult GuardarJuegoSeleccionado(int juego)
    {
        string ViewRetorno = Informacion.seleccionarJuego(juego);
        return View("explicacion" + ViewRetorno);
    }
    public IActionResult nivelform(){
        return View("nivelform");
    }
    public IActionResult GuardarNivel(int nivel)
    {
        Informacion.EstablecerNivel(nivel);
        return RedirectToAction(Informacion.juegoString);
    }

    public IActionResult explicacionmameig()
    {
        return View("explicacionmameig");
    }
    public IActionResult explicacionpiedrapapeltijera(){
        return View("explicacionpiedrapapeltijera");
    }
    public IActionResult mameig(){
        int numero = Informacion.calcularNumero(0,Informacion.maximoMaMeIg);
        ViewBag.numero = numero;
        return View("mameig");
    }
    public IActionResult validarmameig(string accion){
        bool esCorrecto = Informacion.validarMaMeIg(accion);
        if (esCorrecto) ViewBag.Dato = "Felicidades, le pegaste!";
        else ViewBag.Dato = "No le pegaste";
        ViewBag.numero = Informacion.DevolverNumero();
        ViewBag.contador = Informacion.racha;
        return View("mameig");
    }
    public IActionResult piedrapapeltijera(){
        return View("piedrapapeltijera");
    }
    public IActionResult procesarPPT(string jugada){
        string jugadaBot = Informacion.ProcesarPPT(jugada);
        ViewBag.contador = Informacion.racha;
        ViewBag.jugada = $"/images/{jugada.ToLower()}.png";
        ViewBag.jugadaBot =  $"/images/{jugadaBot.ToLower()}.png";
        return View("piedrapapeltijera");
    }









    public IActionResult Privacy()
    {
        ViewBag.NumeroJuego = Informacion.juegoSeleccionado;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
