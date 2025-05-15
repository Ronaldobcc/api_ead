
using cadastro.Models;
using System.Collections.Generic;

namespace cadastro.Repositorio
{
   
    public interface IContatoRepositorio
    {
        List<ContatoModel> BuscarTodos(int usuarioId);
        ContatoModel BuscarPorID(int id);
        Task<ServiceResponse<List<ContatoModel>>> Criar(ContatoModel contato);
        ContatoModel Atualizar(ContatoModel contato);
        bool Apagar(int id);
        
    }
}
