using cadastro.Models;
using System.ComponentModel.DataAnnotations;

namespace cadastro.Dtos.DtoContato;

public class CriarContatosDto
{
    
    public int Id { get; set; }
    [Required(ErrorMessage = "Digite o nome do contato")]
    [StringLength(255)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Digite o e-mail do contato")]
    [EmailAddress(ErrorMessage = "O e-mail informado não é valido!")]
    [StringLength(255)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Digite o celular do contato")]
    [Phone(ErrorMessage = "O celular informado não é valido!")]
    [StringLength(255)]
    public string Celular { get; set; }

    public int? UsuarioId { get; set; }
    [StringLength(255)]

    public UsuarioModel Usuario { get; set; }
}


