using cadastro.Models;
using cadastro.Repositorio;
using Microsoft.AspNetCore.Mvc;
using cadastro.Filters;


namespace cadastro.Controllers;

[PaginaRestritaSomenteAdmin]
public class UsuarioController : Controller
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly IContatoRepositorio _contatoRepositorio;

    public UsuarioController(IUsuarioRepositorio usuarioRepositorio,
                             IContatoRepositorio contatoRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _contatoRepositorio = contatoRepositorio;
    }

    public IActionResult Index()
    {
        List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();

        return View(usuarios);
    }

    public IActionResult Criar()
    {
        return View();
    }

    public IActionResult Editar(int id)
    {
        UsuarioModel usuario = _usuarioRepositorio.BuscarPorID(id);
        return View(usuario);
    }

    public IActionResult ApagarConfirmacao(int id)
    {
        UsuarioModel usuario = _usuarioRepositorio.BuscarPorID(id);
        return View(usuario);
    }

    public IActionResult Apagar(int id)
    {
        try
        {
            bool apagado = _usuarioRepositorio.Apagar(id);

            if (apagado) TempData["MensagemSucesso"] = "Usuário apagado com sucesso!"; else TempData["MensagemErro"] = "Ops, não conseguimos apagar seu usuário, tente novamante!";
            return RedirectToAction("Index");
        }
        catch (Exception erro)
        {
            TempData["MensagemErro"] = $"Ops, não conseguimos apagar seu usuário, tente novamante, detalhe do erro: {erro.Message}";
            return RedirectToAction("Index");
        }
    }

    public IActionResult ListarContatosPorUsuarioId(int id)
    {
        List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos(id);
        return PartialView("_ContatosUsuario", contatos);
    }
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<UsuarioModel>>>> Criar(UsuarioModel Usuario)
    {
        


        if (Usuario != null)

            await _usuarioRepositorio.Criar(Usuario);
        TempData["MensagemSucesso"] = "Usuário Adicionado com sucesso!";
        return RedirectToAction("Index");
    }
 
    

    [HttpPost]
    public IActionResult Editar(UsuarioSemSenhaModel usuarioSemSenhaModel)
    {
        try
        {
            UsuarioModel usuario = null;

            if (ModelState.IsValid)
            {
                usuario = new UsuarioModel()
                {
                    Id = usuarioSemSenhaModel.Id,
                    Nome = usuarioSemSenhaModel.Nome,
                    Login = usuarioSemSenhaModel.Login,
                    Email = usuarioSemSenhaModel.Email,
                    Perfil = usuarioSemSenhaModel.Perfil
                };

                usuario = _usuarioRepositorio.Atualizar(usuario);
                TempData["MensagemSucesso"] = "Usuário alterado com sucesso!";
                return RedirectToAction("Index");
            }

            return View(usuario);
        }
        catch (Exception erro)
        {
            TempData["MensagemErro"] = $"Ops, não conseguimos atualizar seu usuário, tente novamante, detalhe do erro: {erro.Message}";
            return RedirectToAction("Index");
        }
    }
}
