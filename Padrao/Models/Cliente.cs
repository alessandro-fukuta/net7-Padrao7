using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Oficina7.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [Display(Name = "Id")]
        [Column("Id")]
        [Key]
        public int ClienteId { get; set; }

        [Display(Name = "Nome")]
        [Column("Nome")]
        [Required(ErrorMessage = "Nome ou Razão Social é necessário.")]
     
        public string? nome { get; set; }

        [Display(Name = "Endereco")]
        [Column("Endereco")]
        public string? Endereco { get; set; }

        [Display(Name = "Bairro")]
        [Column("Bairro")]
        public string? bairro { get; set; }

        [Display(Name = "Cidade")]
        [Column("Cidade")]
        public string? cidade { get; set; }

        [Display(Name = "Estado")]
        [Column("Estado")]
        public string? estado { get; set; }

        [Display(Name = "Cep")]
        [Column("Cep")]
        public string? cep { get; set; }

        [Display(Name = "Telefone")]
        [Column("Telefone")]
      
        public string? telefone { get; set; }

        [Display(Name = "Celular")]
        [Column("Celular")]
        public string? celular { get; set; }

        [Display(Name = "Email")]
        [Column("Email")]
      
        public string? email { get; set; }

        [Display(Name = "Cpf")]
        [Column("Cpf")]
        public string? cpf { get; set; }

        [Display(Name = "RG")]
        [Column("RG")]
        public string? rg { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column("Nascimento")]
        public DateTime? nascimento { get; set; }
        // public virtual ICollection<OS> OS { get; set; }
     
        public Cliente()
        {
        }

    }
}
