using Microsoft.AspNetCore.Mvc;

namespace TP06_Zilbersztein.Models;
static public class Informacion
{
    static private string[] juegos { get; set; } = { "piedrapapeltijera", "mameig", "ahorcado", "rosco", "tateti", "sopadeletras" };
    static public int? juegoSeleccionado { get; set; }
    static public int Nivel { get; set; }
    static private int minimo { get; set; } = 0;
    static public int maximo { get; set; }
    static private int? primerNumeroMaMeIg { get; set; }
    static private int? segundoNumeroMaMeIg { get; set; }
    static public int racha { get; set; } = 0;
    static public string juegoString { get; set; }
    static private string[] jugadasPPT { get; set; } = { "piedra", "papel", "tijera" };
    static private string[] palabrasAhorcado { get; set; } = { "casa", "agua", "silla", "mesa", "rápido", "panal", "verde", "luna", "gato", "cielo", "lente", "cama", "miel", "tren", "flor", "pared", "reloj", "fresa", "nieve", "llave", "pluma", "huevo", "ropa", "arena", "rata", "boca", "soler", "viento", "fumar", "dedo", "recorrido", "peculiaridad", "satisfactorio", "abandonado", "corregiría", "ponderarían", "adquisición", "identificar", "construcción", "simplificación", "actividades", "comprensible", "proporcionar", "refrigerante", "conocimiento", "establecería", "percepción", "desarrollar", "regularidad", "contradictor", "reputación", "notificaciones", "desigualdad", "manipulador", "funcionalidad", "producción", "colaboración", "decodificación", "diferenciación", "aplicación", "desafortunadamente", "electroencefalograma", "inconstitucionalidad", "contrarrevolucionario", "desproporcionado", "desafiliación", "extraordinario", "incompatibilidad", "multidisciplinario", "transcontinental", "hiperpolarización", "preternaturalmente", "restauracionista", "descentralización", "neurotransmisores" };
    static private string palabraElegida { get; set; }
    static private List<char> letrasDescubiertas { get; set; } = new List<char>() { 'a', 'e', 'i', 'o', 'u' };
    static public int vidas { get; set; } = 7;
    static private string[] palabrasSopa { get; set; }

    static public void reestablecerValores()
    {
        juegoString = null;
        juegoSeleccionado = null;
        racha = 0;
        letrasDescubiertas.Clear();
        letrasDescubiertas.Add('a');
        palabraElegida = null;
    }
    static public string seleccionarJuego(int juego)
    {
        juegoSeleccionado = juego;
        juegoString = juegos[(int)juegoSeleccionado - 1];
        return juegoString;
    }
    static public void EstablecerNivel(int nivel)
    {
        Nivel = nivel;
        if (juegoSeleccionado == 2)
        {
            if (nivel == 1) minimo = 20;
            else if (nivel == 2) maximo = 50;
            else maximo = 100;
        }
        else if (juegoSeleccionado == 3)
        {
            if (nivel == 1) { minimo = 0; maximo = 31; }
            else if (nivel == 2) { minimo = 30; maximo = 62; }
            else { minimo = 59; maximo = 76; }
            PrepararAhorcado();
        }
        else if (juegoSeleccionado == 6)
        {
            if (nivel == 1) { minimo = 0; maximo = 2; }
            else if (nivel == 2) { minimo = 3; maximo = 5; }
            else { minimo = 6; maximo = 8; }
        }

    }
    static public void PrepararAhorcado()
    {
        int numero = calcularNumero(minimo, maximo);
        palabraElegida = palabrasAhorcado[numero];
        letrasDescubiertas.Clear();
        vidas = 7;
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
        segundoNumeroMaMeIg = calcularNumero(minimo, maximo);
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
        string jugadaBot = jugadasPPT[calcularNumero(0, 3)];
        if (jugada == "papel" && jugadaBot == "piedra" || jugada == "tijera" && jugadaBot == "papel" || jugada == "piedra" && jugadaBot == "tijera")
        {
            racha++;
        }
        else if (jugada != jugadaBot)
            racha = 0;
        return jugadaBot;
    }
    static public string RetornarPalabra()
    {
        return palabraElegida;
    }
    static public List<char> RetornarLetrasDescubiertas()
    {
        return letrasDescubiertas;
    }
    static public bool ProcesarAhorcadoLetra(string jugada)
    {
        bool gano = false;
        if (palabraElegida.Contains(jugada))
        {
            if (!letrasDescubiertas.Contains(jugada[0]))
                letrasDescubiertas.Add(jugada[0]);
            gano = victoria();
        }
        else
            vidas--;
        return gano;
    }
    static public bool ProcesarAhorcadoPalabra(string jugada)
    {
        bool gano = false;
        if (jugada != palabraElegida)
            vidas = 0;
        else
            gano = true;
        return gano;
    }
    static public bool victoria()
    {
        bool gano = false;
        int contadorVictoria = 0;
        for (int i = 0; i < palabraElegida.Length; i++)
        {
            if (letrasDescubiertas.IndexOf(palabraElegida[i]) != -1)
            {
                contadorVictoria++;
            }
        }
        if (contadorVictoria == palabraElegida.Length)
            gano = true;
        return gano;
    }
    public static int EstablecerSopa()
    {
        int numeroSopa = calcularNumero(minimo, maximo);
        if (numeroSopa == 0)
        {
            palabrasSopa[0] = "envidia";
            palabrasSopa[1] = "temor";
            palabrasSopa[2] = "sorpresa";
            palabrasSopa[3] = "celos";
            palabrasSopa[4] = "tristeza";
            palabrasSopa[5] = "alegria";
            palabrasSopa[6] = "ansiedad";
            palabrasSopa[7] = "enojo";
        }
        else if (numeroSopa == 1)
        {
            palabrasSopa[0] = "remera";
            palabrasSopa[1] = "polera";
            palabrasSopa[2] = "falda";
            palabrasSopa[3] = "pantalon";
            palabrasSopa[4] = "poncho";
            palabrasSopa[5] = "campera";
            palabrasSopa[6] = "vestido";
            palabrasSopa[7] = "buzo";
            palabrasSopa[8] = "boina";
        }
        else if (numeroSopa == 2)
        {
            palabrasSopa[0] = "camara";
            palabrasSopa[1] = "monitor";
            palabrasSopa[2] = "heladera";
            palabrasSopa[3] = "tablet";
            palabrasSopa[4] = "celular";
            palabrasSopa[5] = "impresora";
            palabrasSopa[6] = "laptop";
            palabrasSopa[7] = "auriculares";
            palabrasSopa[8] = "computadora";
        }
        else if (numeroSopa == 3)
        {
            
        }

        return numeroSopa;
    }
}