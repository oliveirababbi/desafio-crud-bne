using HelloWorld.Data;
using HelloWorld.Models;
using HelloWorld.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace HelloWorld.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }
        public IActionResult Index()
        {
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos();
            return View(contatos);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult DeleteConfirm(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        [HttpPost] 
        public IActionResult Create(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = $"Cadastrado realizado com sucesso! 🚀";
                    return RedirectToAction("Index");
                }

                return View(contato);
            }
            
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o seu contato. \nErro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Cadastrado alterado com sucesso! 🚀";
                    return RedirectToAction("Index");
                }

                return View("Edit", contato);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizar o seu contato. \nErro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                   bool apagado = _contatoRepositorio.Deletar(id);

                    if (apagado)
                    {
                        TempData["MensagemSucesso"] = "Cadastrado deletado com sucesso! 🚀";
                    }
                    else
                    {
                        TempData["MensagemErro"] = $"Ops, não conseguimos deletar o seu contato.";

                    }

                   return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
