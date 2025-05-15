using System.ComponentModel.DataAnnotations;

namespace cadastro.Models;

public class AulaModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string VideoUrl { get; set; }
    public string CapaUrl { get; set; }

    public int? UsuarioId { get; set; }
    public DateTime DataProgramada { get; set; }
   
   
    public string ExtractYouTubeVideoId()
    {
        if (string.IsNullOrEmpty(VideoUrl))
            return string.Empty;

        Uri uri = new Uri(VideoUrl);
        // Caso seja o formato encurtado
        if (uri.Host.Contains("youtu.be"))
        {
            return uri.AbsolutePath.Trim('/');
        }

        // Caso seja um formato completo como https://www.youtube.com/watch?v=VIDEO_ID
        var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
        return query["v"];
    }
}



