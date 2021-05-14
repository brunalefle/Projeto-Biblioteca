using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Cadastro()
        {
            if (HttpContext.Session.GetString("user") != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Usuario u)
        {
            UsuarioService usuarioService = new UsuarioService();

            if (u.Id == 0)
            {

                usuarioService.Inserir(u);
            }
            else
            {
                usuarioService.Atualizar(u);
            }

            return RedirectToAction("Listagem");
        }

        public IActionResult Listagem()
        {
            if (HttpContext.Session.GetString("user") != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            UsuarioService service = new UsuarioService();
            return View(service.ListarTodos());
        }


        public IActionResult Exclusao(int id)
        {
            if (HttpContext.Session.GetString("user") != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            UsuarioService service = new UsuarioService();
            Usuario usuario = service.ObterPorId(id);

            return View(usuario);
        }

        [HttpPost]
        public IActionResult Exclusao(int id, string decisao)
        {
            if (decisao == "s")
            {
                UsuarioService service = new UsuarioService();
                service.Excluir(id);
            }

            return RedirectToAction("Listagem");
        }

        public IActionResult Edicao(int id)
        {
            if (HttpContext.Session.GetString("user") != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            UsuarioService service = new UsuarioService();
            Usuario u = service.ObterPorId(id);
            return View(u);
        }
    }
}