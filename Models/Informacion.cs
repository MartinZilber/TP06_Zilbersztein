namespace TP06_Zilbersztein.Models;
static public class Informacion
{
    static public int numeroMaMeIg{get;set;}

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