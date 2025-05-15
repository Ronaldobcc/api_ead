using cadastro.Models;
using System.ComponentModel.DataAnnotations;

namespace cadastro.Dtos.AulasDto;

    public class CriarAulaDto
    {

    [StringLength(255)]
    public string? Nome { get; set; } = string.Empty;
    [StringLength(255)]
    public string? Descricao { get; set; } = string.Empty;
    [StringLength(255)]
    public string? VideoUrl { get; set; } = string.Empty;
    [StringLength(255)]
    public string? CapaUrl { get; set; } // URL da capa da aula
    [StringLength(255)]
    public DateTime DataProgramada { get; set; } 

    }

