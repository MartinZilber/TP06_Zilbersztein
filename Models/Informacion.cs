namespace TP06_Zilbersztein.Models;
static public class Informacion
{
    static public int numeroMaMeIg{get;set;}
    static private int juegoSeleccionado{get;set;}
    static private string[] juegos{get;set;}=  {"Piedra, papel o tijera", "MAMEIG", "Ahorcado", "El rosco", "Ta-te-ti", ""};

    static public void seleccionarJuego(int juego){
        juegoSeleccionado = juego;
    } 
    static public void EstablecerNivel(int numeroJuego, string nivel){
        if (numeroJuego == 1)
        {

        }
    }
    static public int calcularNumero(string nivel){
        Random R = new Random();
        int numero = R.Next(1,100);
        return 0;
    }
}