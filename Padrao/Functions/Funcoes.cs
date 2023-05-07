namespace Oficina7.Functions
{
    public class Funcoes
    {

        public int GeraID()
        {
            Random rnd = new Random();
            int Numero = rnd.Next(100000, 999999);
            return Numero;
        }

    }
}
