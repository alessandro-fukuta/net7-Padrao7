using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Padrao.Models
{
    public class Usuario_Dado
    {
        public Guid Id { get; set; }
        public int UsuarioID { get; set; }
        public Usuario? Usuario { get; set; }
        public string? Endereco_Cep { get; set; }
        public string? Endereco_Rua { get; set; }
        public string? Endereco_Numero { get; set; }
        public string? Endereco_Bairro { get; set; }
        public string? Endereco_Cidade { get; set; }
        public string? Endereco_Estado { get; set; }
        public string? CPF { get; set; }
        public string? RG { get; set; }
       
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Nascimento { get; set; }

    }
}
