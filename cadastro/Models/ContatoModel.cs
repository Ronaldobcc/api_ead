using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cadastro.Models
{
    public class ContatoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Define auto-incremento
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome do contato")]
        [MaxLength(255)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o e-mail do contato")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido!")]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite o celular do contato")]
        [Phone(ErrorMessage = "O celular informado não é valido!")]
        [MaxLength(20)]
        public string Celular { get; set; }

        public int? UsuarioId { get; set; }

        public UsuarioModel Usuario { get; set; }
    }
}
