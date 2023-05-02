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
		[StringLength(20,ErrorMessage = "Máximo de 20 caracteres")]
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

        public int Autenticacao { get; set; } // codigo a ser enviado no email e validado
        public bool EmailValidado { get; set; } // se sim o login pode ser feito, senão solicite o código enviado no email ou reenviar.
        public string? Endereco_Cep { get; set; }
        public string? Endereco_Rua { get; set; }
        public string? Endereco_Numero { get; set; }
        public string? Endereco_Bairro { get; set; }
        public string? Endereco_Cidade { get; set; }
        public string? Endereco_Estado { get; set; }
        public string? CPF { get; set; }
        public string? RG { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Nascimento { get; set; }

    }
}
