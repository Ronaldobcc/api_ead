using cadastro.Data;
using cadastro.Dtos.DtoContato;
using cadastro.Helper;
using cadastro.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cadastro.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {

        private readonly BancoContext _context;
       // private readonly ISessao _sessao;

        public ContatoRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;

        }

        public ContatoModel BuscarPorID(int id)
        {
            return _context.Contatos.FirstOrDefault(x => x.Id == id);
        }

        public List<ContatoModel> BuscarTodos(int usuarioId)
        {
            return _context.Contatos.Where(x => x.UsuarioId == usuarioId).ToList();
        }


        public async Task<ServiceResponse<List<ContatoModel>>> Criar(ContatoModel contato)
        {
            ServiceResponse<List<ContatoModel>> serviceResponse = new ServiceResponse<List<ContatoModel>>();


            try
            {
                if (contato == null)
                {

                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Informar dados!";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                {
                   
                    _context.Contatos.Add(contato);
                    await _context.SaveChangesAsync();

                }


            }
            catch (Exception erro)
            {
                serviceResponse.Mensagem = erro.Message;
                serviceResponse.Sucesso = false;

            }

            return serviceResponse;


        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDB = BuscarPorID(contato.Id);

            if (contatoDB == null) throw new Exception("Houve um erro na atualização do contato!");

            contatoDB.Nome = contato.Nome;
            contatoDB.Email = contato.Email;
            contatoDB.Celular = contato.Celular;

            _context.Contatos.Update(contatoDB);
            _context.SaveChanges();

            return contatoDB;
        }

        public bool Apagar(int id)
        {
            ContatoModel contatoDB = BuscarPorID(id);

            if (contatoDB == null) throw new Exception("Houve um erro na deleção do contato!");

            _context.Contatos.Remove(contatoDB);
            _context.SaveChanges();

            return true;
        }

        
    }
}