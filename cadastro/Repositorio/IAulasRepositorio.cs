
using cadastro.Dtos.AulasDto;
using cadastro.Models;
using System.Collections.Generic;

namespace cadastro.Repositorio
{
   
    public interface IAulasRepositorio
    {
        Task<AulaModel> Criar(CriarAulaDto criarAulaDto, IFormFile foto);
        Task <List<AulaModel>> BuscarTodos();
        List<AulaModel> ListaAulas();
        Task<AulaModel> BuscarPorId(int Id);
        Task<AulaModel> Atualizar(AulaModel aula, IFormFile foto);
        
        Task<AulaModel> Remover(int id);
        
       
        
    }
}
