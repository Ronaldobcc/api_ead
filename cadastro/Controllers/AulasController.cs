using cadastro.Models;
using cadastro.Filters;
using cadastro.Helper;
using cadastro.Repositorio;
using Microsoft.AspNetCore.Mvc;
using cadastro.Dtos.AulasDto;

namespace cadastro.Controllers
{
    [PaginaParaUsuarioLogado]
    
    public class AulasController : Controller
    {
        private readonly IAulasRepositorio _aulasRepositorio;
        private readonly ISessao _sessao;

        public AulasController(IAulasRepositorio aulasRepositorio,
                                 ISessao sessao)
        {
            _aulasRepositorio = aulasRepositorio;
            _sessao = sessao;
        }

        public async Task<IActionResult> Index()
        {
            // Buscar usuário logado
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();

            // Buscar aulas do repositório
            var aulas = await _aulasRepositorio.BuscarTodos();

            // Se 'aulas' for null, retornar uma lista vazia para evitar problemas
            if (aulas == null)
            {
                aulas = new List<AulaModel>();
            }

            return View(aulas);
        }

        public async Task<IActionResult> ListaAulas()
        {
            // Buscar usuário logado
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();

            // Buscar aulas do repositório
            var aulas = await _aulasRepositorio.BuscarTodos();


            // Se 'aulas' for null, retornar uma lista vazia para evitar problemas
            if (aulas == null)
            {
                aulas = new List<AulaModel>();
            }

            return View(aulas);
        }




        public async Task<IActionResult> Editar(int id)
        {
            var aula = await _aulasRepositorio.BuscarPorId(id);

            return View(aula);
        }

        public IAulasRepositorio Get_aulasRepositorio()
        {
            return _aulasRepositorio;
        }

        public IActionResult ApagarConfirmacao(int id)
        {
           var aula = _aulasRepositorio.BuscarPorId(id);
            return View(aula);
        }

        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                // Buscar usuário logado
                UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                var apagado = await _aulasRepositorio.Remover(id);

                return RedirectToAction("ListaAulas");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar a aula, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
        public IActionResult Criar()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Criar(CriarAulaDto criarAulaDto, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                // Verificar se a foto foi enviada
                if (foto != null && foto.Length > 0)
                {
                    // Definir o caminho onde a imagem será salva
                    var nomeArquivo = Path.GetFileName(foto.FileName);

                    // Definir o diretório para salvar a imagem
                    var caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagem/aulas", nomeArquivo);

                    // Criar o diretório se ele não existir
                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagem/aulas")))
                    {
                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagem/aulas"));
                    }

                    // Salvar o arquivo fisicamente no diretório
                    using (var stream = new FileStream(caminho, FileMode.Create))
                    {
                        await foto.CopyToAsync(stream);
                    }

                    // Atualizar o DTO com o caminho da imagem
                    criarAulaDto.CapaUrl = "/imagem/aulas/" + nomeArquivo;
                }

                // Criar a aula com o repositório
                UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
        
         
                    var aula = await _aulasRepositorio.Criar(criarAulaDto, foto);
                    return RedirectToAction("ListaAulas");
                
            }
            else
            {
                return View(criarAulaDto);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Editar(AulaModel aulaModel, IFormFile? foto)
        {
            if (ModelState.IsValid)
            {
              
                UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                var aula = await _aulasRepositorio.Atualizar(aulaModel, foto);
                return RedirectToAction("ListaAulas");
            }
            else
            {
                return View(aulaModel);
            }
        }

    }
}
    
