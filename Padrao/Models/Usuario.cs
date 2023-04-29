using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Padrao.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(50,ErrorMessage = "Máx de 50 caracteres")]
        [Display(Name = "Nome Completo")]
        public string? NomeCompleto { get; set; }

		[Required(ErrorMessage = "Campo obrigatório")]
		[StringLength(20)]
		[Display(Name = "Usuario")]
		public string? NomeUsuario { get; set; }


		[Required(ErrorMessage = "Campo obrigatório")]
		[StringLength(100)]
        [Display(Name = "E-Mail")]
        public string? Email { get; set; }

		[Required(ErrorMessage = "Campo obrigatório")]
		[StringLength(14)]
        public string? Celular { get; set; }

		[Required(ErrorMessage = "Campo obrigatório")]
		[StringLength(18, MinimumLength = 8, ErrorMessage = "Mínimo de 8 Caracteres")]
        public string? Senha { get; set; }

		[NotMapped] // Does not effect with your database
		[Compare(nameof(Senha), ErrorMessage = "Senhas não conferem.")]
		public string? ConfirmeSenha { get; set; }

		[Display(Name = "Data Cadastro")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime Cadastro { get; set; }

        public bool Administrador { get; set; }
        public string? Chave { get; set; } // ligado a algum cadastro / empresa


    }
}
