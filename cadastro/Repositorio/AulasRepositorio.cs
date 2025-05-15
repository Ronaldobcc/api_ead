using cadastro.Data;
using cadastro.Dtos.AulasDto;
using cadastro.Dtos.DtoContato;
using cadastro.Helper;
using cadastro.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace cadastro.Repositorio
{
    public class AulasRepositorio : IAulasRepositorio
    {

        private readonly BancoContext _context;
        private readonly string _sistema;

        public AulasRepositorio(BancoContext bancoContext, IWebHostEnvironment sistema)
        {
            this._context = bancoContext;
            _sistema = sistema.WebRootPath;

        }


        public string GeraCaminhoArquivo(IFormFile foto)
        {
            var codigoUnico = Guid.NewGuid().ToString();
            var nomeCaminhoImagem = foto.FileName.Replace(" ", "").ToLower() + codigoUnico + ".png";

            var caminhoParaSalvarImagens = _sistema + "\\imagem\\";


            if (!Directory.Exists(caminhoParaSalvarImagens))
            {
                Directory.CreateDirectory(caminhoParaSalvarImagens);
            }

            using (var stream = File.Create(caminhoParaSalvarImagens + nomeCaminhoImagem))
            {
                foto.CopyToAsync(stream).Wait();
            }

            return nomeCaminhoImagem;
        }




        public async Task<AulaModel> Criar(CriarAulaDto criarAulaDto, IFormFile foto)
        {
            try
            {
                var nomeCaminhoImagem = GeraCaminhoArquivo(foto);

                var aula = new AulaModel
                {
                    Nome = criarAulaDto.Nome,
                    Descricao = criarAulaDto.Descricao,
                    VideoUrl = criarAulaDto.VideoUrl,
                    CapaUrl = nomeCaminhoImagem,
                    DataProgramada = criarAulaDto.DataProgramada
                };

                _context.Aulas.Add(aula);
                await _context.SaveChangesAsync();

                return aula;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<AulaModel> Atualizar(AulaModel aula, IFormFile? foto)
         {
             try
             {
                

                // Verificar se há uma foto associada e se o arquivo existe no sistema
                if (!string.IsNullOrEmpty(aula.CapaUrl))
                {
                    // Caminho físico da imagem
                    var caminhoImagem = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagem", aula.CapaUrl);

                    // Verificar se o arquivo existe
                    if (System.IO.File.Exists(caminhoImagem))
                    {
                        // Remover o arquivo físico
                        System.IO.File.Delete(caminhoImagem);
                    }
                }
                var aulaBanco = await _context.Aulas.AsNoTracking().FirstOrDefaultAsync(aulaBD => aulaBD.Id == aula.Id);

                 var nomeCaminhoImagem = "";

                 if (foto != null)
                 {
                     string caminhoCapaExistente = _sistema + "\\imagem\\" + aulaBanco.CapaUrl;

                     if (File.Exists(caminhoCapaExistente))
                     {
                         File.Delete(caminhoCapaExistente);
                     }

                     nomeCaminhoImagem = GeraCaminhoArquivo(foto);
                 }
                
                aulaBanco.Nome = aula.Nome;
                 aulaBanco.Descricao = aula.Descricao;
                 aulaBanco.VideoUrl = aula.VideoUrl;
                 aulaBanco.CapaUrl = aula.CapaUrl;
                aulaBanco.DataProgramada = aula.DataProgramada;

                 if (nomeCaminhoImagem != "")
                 {
                     aulaBanco.CapaUrl = nomeCaminhoImagem;
                 }
                 else
                 {
                     aulaBanco.CapaUrl = aula.CapaUrl;
                 }

                 _context.Aulas.Update(aulaBanco);
                 await _context.SaveChangesAsync();

                 return aula;

             }
             catch (Exception ex)
             {
                 throw new Exception(ex.Message);
             }
         }

        public async Task<AulaModel> Remover(int id)
        {
            try
            {
                // Buscar a aula no banco de dados
                var aula = await _context.Aulas.FirstOrDefaultAsync(aulabanco => aulabanco.Id == id);

                if (aula == null)
                {
                    throw new Exception("Aula não encontrada.");
                }

                // Verificar se há uma foto associada e se o arquivo existe no sistema
                if (!string.IsNullOrEmpty(aula.CapaUrl))
                {
                    // Caminho físico da imagem
                    var caminhoImagem = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagem", aula.CapaUrl);

                    // Verificar se o arquivo existe
                    if (System.IO.File.Exists(caminhoImagem))
                    {
                        // Remover o arquivo físico
                        System.IO.File.Delete(caminhoImagem);
                    }
                }


                // Remover a aula do banco de dados
                _context.Aulas.Remove(aula);
                await _context.SaveChangesAsync();

                return aula;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task <AulaModel> BuscarPorId(int id)
        
            {
            
                try
                {
                   

                    return await _context.Aulas.FirstOrDefaultAsync(aula => aula.Id == id);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        public async Task<List<AulaModel>> BuscarTodos()
        {
            try
            {

                return await _context.Aulas.ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<AulaModel> ListaAulas()
        {
            var aulas = _context.Aulas
                .Select(a => new AulaModel
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Descricao = a.Descricao,
                    VideoUrl = a.VideoUrl,
                    CapaUrl = a.CapaUrl,
                    DataProgramada = a.DataProgramada
                })
                .ToList();

            return aulas;
        }

    }
}

