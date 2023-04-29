namespace Padrao.Models
{
    public class Registro
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Chave { get; set; }
        public DateTime Implantacao { get; set; }
        public int Expira { get; set; }

    }
}
