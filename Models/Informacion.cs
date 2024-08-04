using System.ComponentModel;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;

namespace TP06_Zilbersztein.Models;
static public class Informacion
{
    static private string[] juegos { get; set; } = { "piedrapapeltijera", "mameig", "ahorcado", "rosco", "tateti", "sopadeletras" };
    static public int? juegoSeleccionado { get; set; }
    static public int Nivel { get; set; }
    static public int contadorIntentos { get; set; } = 0;
    static public int minimo { get; set; } = 0;
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
    static public string[] palabrasSopa { get; set; } = new string[9];
    static public string respuestaFinalSopa { get; set; }
    static public string respuestaIngresadaUsuario { get; set; }
    static public int sopaNumero { get; set; }
    static public int? sopaAnterior { get; set; }
    static public bool[] espaciosOcupadosCirculo = new bool[9];
    static public bool[] espaciosOcupadosCruz = new bool[9];
    static public int[] espaciosOcupados = new int[9];
    static public bool alguienGano { get; set; }

    static public void reestablecerValores()
    {
        juegoString = "";
        juegoSeleccionado = null;
        racha = 0;
        letrasDescubiertas.Clear();
        palabraElegida = "";
        maximo = 0;
        minimo = 0;
        contadorIntentos = 0;
        respuestaFinalSopa = "";
        for (int i = 0; i < palabrasSopa.Length; i++)
        {
            palabrasSopa[i] = "";
            espaciosOcupadosCirculo[i] = false;
            espaciosOcupadosCruz[i] = false;
            espaciosOcupados[i] = 0;
        }
        alguienGano = false;
    }
    static public string seleccionarJuego(int juego)
    {
        juegoSeleccionado = juego;
        juegoString = juegos[(int)juegoSeleccionado - 1];
        if (juegoSeleccionado == 5)
        {
            minimo = 1;
            maximo = 10;
        }
        return juegoString;
    }
    static public void EstablecerNivel(int nivel)
    {
        Nivel = nivel;
        if (juegoSeleccionado == 2)
        {
            if (nivel == 1) maximo = 20;
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
            if (nivel == 1) { minimo = 0; maximo = 3; }
            else if (nivel == 2) { minimo = 3; maximo = 6; }
            else if (nivel == 3) { minimo = 6; maximo = 9; }
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
    public static void EstablecerSopa()
    {
        sopaAnterior = sopaNumero;
        do
        {
            sopaNumero = calcularNumero(minimo, maximo);
        } while (sopaNumero == sopaAnterior);
        respuestaFinalSopa = null;
        for (int i = 0; i < palabrasSopa.Length; i++)
        { palabrasSopa[i] = ""; }

        int numeroSopa = sopaNumero;

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
            palabrasSopa[0] = "yrigoyen";
            palabrasSopa[1] = "fernandez";
            palabrasSopa[2] = "kirchner";
            palabrasSopa[3] = "peron";
            palabrasSopa[4] = "illia";
            palabrasSopa[5] = "macri";
            palabrasSopa[6] = "martinez";
        }
        else if (numeroSopa == 4)
        {
            palabrasSopa[0] = "lobo";
            palabrasSopa[1] = "avestruz";
            palabrasSopa[2] = "loro";
            palabrasSopa[3] = "gallina";
            palabrasSopa[4] = "ardilla";
            palabrasSopa[5] = "gato";
            palabrasSopa[6] = "perro";
            palabrasSopa[7] = "dinosaurio";
        }
        else if (numeroSopa == 5)
        {
            palabrasSopa[0] = "amarillo";
            palabrasSopa[1] = "verde";
            palabrasSopa[2] = "violeta";
            palabrasSopa[3] = "naranja";
            palabrasSopa[4] = "rojo";
            palabrasSopa[5] = "celeste";
            palabrasSopa[6] = "rosa";
            palabrasSopa[7] = "bordo";
            palabrasSopa[8] = "azul";
        }
        else if (numeroSopa == 6)
        {
            palabrasSopa[0] = "construcciones";
            palabrasSopa[1] = "gestion";
            palabrasSopa[2] = "informatica";
            palabrasSopa[3] = "produccion";
            palabrasSopa[4] = "medios";
            palabrasSopa[5] = "quimica";
            palabrasSopa[6] = "diseño";
            palabrasSopa[7] = "humanidades";
            palabrasSopa[8] = "mecatronica";
        }
        else if (numeroSopa == 7)
        {
            palabrasSopa[0] = "milanesa";
            palabrasSopa[1] = "salchicha";
            palabrasSopa[2] = "manzana";
            palabrasSopa[3] = "pizza";
            palabrasSopa[4] = "pescado";
            palabrasSopa[5] = "sushi";
            palabrasSopa[6] = "pure";
            palabrasSopa[7] = "kiwi";

        }
        else if (numeroSopa == 8)
        {
            palabrasSopa[0] = "spinosaurus";
            palabrasSopa[1] = "trex";
            palabrasSopa[2] = "velociraptor";
            palabrasSopa[3] = "allosaurus";
            palabrasSopa[4] = "tegosaurus";
            palabrasSopa[5] = "diplodocus";
            palabrasSopa[6] = "brachiosaurus";
            palabrasSopa[7] = "ankylosaurus";
        }
        for (int i = 0; i < palabrasSopa.Length; i++)
        {
            if (palabrasSopa[i] != null)
                respuestaFinalSopa += palabrasSopa[i];
        }

    }
    static public bool procesarSopa(string palabras)
    {
        bool esCorrecto = false;
        respuestaIngresadaUsuario = palabras;
        if (palabras.Equals(respuestaFinalSopa))
            esCorrecto = true;
        else if (contadorIntentos == 0)
            contadorIntentos++;
        return esCorrecto;
    }
    static public bool procesarTaTeTi(int jugada)
    {
        bool esPosible = false;
        jugada--;
        if (!espaciosOcupadosCirculo[jugada] && !espaciosOcupadosCruz[jugada] && !alguienGano)
        {
            espaciosOcupadosCirculo[jugada] = true;
            espaciosOcupados[jugada] = 1;
            esPosible = true;
        }
        return esPosible;
    }
    static public void jugadaBotTaTeTi()
    {
        int jugada, contadorEspacios = 0;
        bool esPosible = false, noOcupado = false;
        do
        {
            if (espaciosOcupados[contadorEspacios] == 0)
            {
                noOcupado = true;
            }
            else
                contadorEspacios++;
        } while (!noOcupado && contadorEspacios <= 8);
        if (noOcupado)
        {
            do
            {
                jugada = calcularNumero(minimo, maximo);
                jugada--;
                if (!espaciosOcupadosCirculo[jugada] && !espaciosOcupadosCruz[jugada])
                {
                    esPosible = true;
                    espaciosOcupadosCruz[jugada] = true;
                    espaciosOcupados[jugada] = 2;
                }

            } while (!esPosible);
        }



    }
    static public int[] procesarEspacios()
    {
        int[] espacios = new int[maximo];
        for (int i = 0; i < maximo - 1; i++)
        {
            if (espaciosOcupadosCirculo[i])
                espacios[i] = 1;
            else if (espaciosOcupadosCruz[i])
                espacios[i] = 2;
            else
                espacios[i] = 0;
        }
        return espacios;
    }
    static public void ReestablecerTaTeTi()
    {
        for (int i = 0; i < espaciosOcupados.Length; i++)
        {
            espaciosOcupados[i] = 0;
            espaciosOcupadosCruz[i] = false;
            espaciosOcupadosCirculo[i] = false;
            alguienGano = false;
        }
    }
    static public int esPrimeraJugada()
    {
        bool esPrimeraJugada = true;
        int contador = 0, jugada = 1;
        while (esPrimeraJugada && contador < maximo - 1)
        {
            if (espaciosOcupados[contador] != 0)
                esPrimeraJugada = false;
            else
                contador++;
        }
        if (esPrimeraJugada)
        {
            jugada = calcularNumero(1, 3);
        }
        return jugada;
    }
    static public string determinarGanador()
    {
        int contador = 0;
        bool espaciosLlenos = true;
        string mensajeGanador = "";
        if (espaciosOcupadosCirculo[0] && espaciosOcupadosCirculo[1] && espaciosOcupadosCirculo[2] || espaciosOcupadosCirculo[3] && espaciosOcupadosCirculo[4] && espaciosOcupadosCirculo[5] || espaciosOcupadosCirculo[6] && espaciosOcupadosCirculo[7] && espaciosOcupadosCirculo[8] || espaciosOcupadosCirculo[0] && espaciosOcupadosCirculo[3] && espaciosOcupadosCirculo[6] || espaciosOcupadosCirculo[1] && espaciosOcupadosCirculo[4] && espaciosOcupadosCirculo[7] || espaciosOcupadosCirculo[2] && espaciosOcupadosCirculo[5] && espaciosOcupadosCirculo[8] || espaciosOcupadosCirculo[0] && espaciosOcupadosCirculo[4] && espaciosOcupadosCirculo[8] || espaciosOcupadosCirculo[2] && espaciosOcupadosCirculo[4] && espaciosOcupadosCirculo[6])
        {
            mensajeGanador = "¡Ganaste!";
            alguienGano = true;
        }
        else if (espaciosOcupadosCruz[0] && espaciosOcupadosCruz[1] && espaciosOcupadosCruz[2] || espaciosOcupadosCruz[3] && espaciosOcupadosCruz[4] && espaciosOcupadosCruz[5] || espaciosOcupadosCruz[6] && espaciosOcupadosCruz[7] && espaciosOcupadosCruz[8] || espaciosOcupadosCruz[0] && espaciosOcupadosCruz[3] && espaciosOcupadosCruz[6] || espaciosOcupadosCruz[1] && espaciosOcupadosCruz[4] && espaciosOcupadosCruz[7] || espaciosOcupadosCruz[2] && espaciosOcupadosCruz[5] && espaciosOcupadosCruz[8] || espaciosOcupadosCruz[0] && espaciosOcupadosCruz[4] && espaciosOcupadosCruz[8] || espaciosOcupadosCruz[2] && espaciosOcupadosCruz[4] && espaciosOcupadosCruz[6])
        {
            mensajeGanador = "¡Perdiste!";
            alguienGano = true;
        }
        else
        {
            do
            {
                if (espaciosOcupados[contador] == 0)
                    espaciosLlenos = false;
                else
                    contador++;
            } while (contador < maximo - 1 && espaciosLlenos);
            if (espaciosLlenos) mensajeGanador = "¡Hubo un empate!";
        }
        return mensajeGanador;
    }
}
