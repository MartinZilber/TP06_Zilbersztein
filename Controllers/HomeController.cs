using System.Diagnostics;
using System.Xml.Schema;
using Microsoft.AspNetCore.Http.Features;
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
    public IActionResult sobreNosotros()
    {
        return View("sobrenosotros");
    }
    public IActionResult GuardarJuegoSeleccionado(int juego)
    {
        Informacion.reestablecerValores();
        string ViewRetorno = Informacion.seleccionarJuego(juego);
        return View("explicacion" + ViewRetorno);
    }
    public IActionResult nivelform()
    {
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
    public IActionResult explicacionpiedrapapeltijera()
    {
        return View("explicacionpiedrapapeltijera");
    }
    public IActionResult explicacionahorcado()
    {
        return View("explicacionahorcado");
    }
    public IActionResult explicacionsopadeletras()
    {
        return View("explicacionsopadeletras");
    }
    public IActionResult explicaciontateti()
    {
        return View("explicaciontateti");
    }
    public IActionResult explicacionadivinapalabra()
    {
        Informacion.reestablecerValores();
        return View("explicacionadivinapalabra");
    }
    public IActionResult mameig()
    {
        int numero = Informacion.calcularNumero(0, Informacion.maximo);
        ViewBag.numero = numero;
        return View("mameig");
    }
    public IActionResult validarmameig(string accion)
    {
        bool esCorrecto = Informacion.validarMaMeIg(accion);
        if (esCorrecto) ViewBag.Dato = "Felicidades, le pegaste!";
        else ViewBag.Dato = "No le pegaste";
        ViewBag.numero = Informacion.DevolverNumero();
        ViewBag.contador = Informacion.racha;
        return View("mameig");
    }
    public IActionResult piedrapapeltijera()
    {
        return View("piedrapapeltijera");
    }
    public IActionResult procesarPPT(string jugada)
    {
        string jugadaBot = Informacion.ProcesarPPT(jugada);
        ViewBag.contador = Informacion.racha;
        ViewBag.jugada = $"/images/{jugada.ToLower()}.png";
        ViewBag.jugadaBot = $"/images/{jugadaBot.ToLower()}.png";
        return View("piedrapapeltijera");
    }
    public IActionResult ahorcado(bool victoria)
    {
        ViewBag.vidas = Informacion.vidas;
        if (ViewBag.vidas == 0)
        {
            Informacion.PrepararAhorcado();
            ViewBag.Final = "¡Perdiste!";
        }
        else if (victoria)
        {
            Informacion.PrepararAhorcado();
            ViewBag.Final = "¡Ganaste!";
        }
        ViewBag.palabra = Informacion.RetornarPalabra();
        ViewBag.longitudPalabra = ViewBag.palabra.Length;
        ViewBag.letrasDescubiertas = Informacion.RetornarLetrasDescubiertas();
        ViewBag.imagenVidas = "/images/" + Informacion.vidas + "vidas.png";
        return View("ahorcado");
    }
    public IActionResult ProcesarAhorcado(string jugada)
    {
        bool gano = false;
        if (jugada.Length == Informacion.RetornarPalabra().Length)
            gano = Informacion.ProcesarAhorcadoPalabra(jugada);
        else if (jugada.Length == 1)
            gano = Informacion.ProcesarAhorcadoLetra(jugada);
        return RedirectToAction(Informacion.juegoString, new { gano });
    }
    public IActionResult sopadeletras(bool gano = false)
    {
        if (gano || Informacion.contadorIntentos == 0)
        {
            if (gano)
            {
                ViewBag.Mensaje = "Felicidades, ¡ganaste!";
                Informacion.contadorIntentos = 0;
            }
            else
            {
                Informacion.contadorIntentos++;
            }
            Informacion.EstablecerSopa();
        }
        else
            ViewBag.Mensaje = "Oh, no, ¡algo hiciste mal!";
        ViewBag.imagensopa = "/images/Sopa" + Informacion.sopaNumero + ".png";
        return View(Informacion.juegoString);
    }


    public IActionResult procesarSopa(string palabras)
    {
        bool esCorrecto = Informacion.procesarSopa(palabras);
        if (esCorrecto)
            return RedirectToAction("sopadeletras", new { gano = true });
        else
            return RedirectToAction("sopadeletras", new { gano = false });
    }
    public IActionResult tateti()
    {
        string ganador;
        int jugada = Informacion.esPrimeraJugada();
        if (jugada == 2)
        {
            Informacion.jugadaBotTaTeTi();
        }
        int[] espaciosOcupados = Informacion.procesarEspacios();
        ViewBag.espaciosOcupados = espaciosOcupados;
        ganador = Informacion.determinarGanador();
        ViewBag.mensajeGanador = ganador;
        return View("tateti");
    }
    public IActionResult ProcesarTaTeTi(int jugada)
    {
        bool esPosibleJugador = Informacion.procesarTaTeTi(jugada);
        string ganador = Informacion.determinarGanador();
        if (esPosibleJugador && ganador == "" && !Informacion.alguienGano)
            Informacion.jugadaBotTaTeTi();
        return RedirectToAction("tateti");
    }
    public IActionResult ReintentarTaTeTi()
    {
        Informacion.ReestablecerTaTeTi();
        return RedirectToAction("tateti");
    }

    public IActionResult adivinapalabra()
    {
        string definicion = Informacion.elegirPalabra();
        ViewBag.definicion = definicion;
        ViewBag.contador = Informacion.racha;
        ViewBag.Mensaje = Informacion.mensajeAdivinarPalabra;
        return View("adivinapalabra");
    }
    public IActionResult ProcesarAdivinarPalabra(string palabra)
    {
        string mensajeGano = Informacion.ProcesarAdivinaPalabra(palabra);
        ViewBag.mensaje = mensajeGano;
        return RedirectToAction("adivinapalabra");
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
