using Microsoft.AspNetCore.Mvc;

namespace TP06_Zilbersztein.Models;
static public class Informacion
{
    static private int minimoMaMeIg { get; set; } = 0;
    static public int maximoMaMeIg { get; set; }
    static private int? primerNumeroMaMeIg { get; set; }
    static private int? segundoNumeroMaMeIg { get; set; }
    static public int racha { get; set; } = 0;
    static private int cantLetrasAhorcado { get; set; }
    static public int? juegoSeleccionado { get; set; }
    static public string juegoString { get; set; }
    static private string[] juegos { get; set; } = { "piedrapapeltijera", "mameig", "Ahorcado", "rosco", "Tateti", "" };
    static private int minimoPPT { get; set; } = 0;
    static private int maximoPPT { get; set; } = 2;
    static private string[] jugadasPPT{get;set;} = {"piedra", "papel", "tijera"};

    static public void reestablecerValores()
    {
        juegoString = null;
        juegoSeleccionado = null;
        racha = 0;
    }
    static public string seleccionarJuego(int juego)
    {
        juegoSeleccionado = juego;
        juegoString = juegos[(int)juegoSeleccionado - 1];
        return juegoString;
    }
    static public void EstablecerNivel(int nivel)
    {
        if (juegoSeleccionado == 2)
        {
            if (nivel == 1) maximoMaMeIg = 20;
            else if (nivel == 2) maximoMaMeIg = 50;
            else maximoMaMeIg = 100;
        }
        else if (juegoSeleccionado == 3)
        {
            if (nivel == 1) cantLetrasAhorcado = 5;
            else if (nivel == 2) cantLetrasAhorcado = 10;
            else cantLetrasAhorcado = 20;
        }

    }
    static public int calcularNumero(int minimo, int maximo)
    {
        Random R = new Random();
        int numero;
        numero = R.Next(minimo, maximo);
        if (primerNumeroMaMeIg == null)
            primerNumeroMaMeIg = numero;
        return numero;
    }
    static public int DevolverNumero()
    {
        return (int)primerNumeroMaMeIg;
    }
    static public bool validarMaMeIg(string accion)
    {
        bool esCorrecto = false;
        accion = accion.ToLower();
        segundoNumeroMaMeIg = calcularNumero(minimoMaMeIg, maximoMaMeIg);
        if (segundoNumeroMaMeIg > primerNumeroMaMeIg && accion == "mayor" || segundoNumeroMaMeIg < primerNumeroMaMeIg && accion == "menor" || segundoNumeroMaMeIg == primerNumeroMaMeIg && accion == "igual")
        {
            esCorrecto = true;
            racha++;
        }
        else
            racha = 0;
        primerNumeroMaMeIg = segundoNumeroMaMeIg;
        return esCorrecto;
    }
    static public string ProcesarPPT(string jugada)
    {
        string jugadaBot = jugadasPPT[calcularNumero(0,3)];
        if (jugada == "papel" && jugadaBot == "piedra" || jugada == "tijera" && jugadaBot == "papel" || jugada == "piedra" && jugadaBot == "tijera")
        {
            racha++;
        }
        else if (jugada != jugadaBot)
        racha = 0;
        return jugadaBot;
    }
}