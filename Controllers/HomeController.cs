using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario u)
        {
            UsuarioService service = new UsuarioService();
            Usuario usuarioEncontrado = service.ObterDados(u.Login, u.Senha);

            if(usuarioEncontrado == null)
            {
                ViewData["Erro"] = "Login ou senha incorreta";
                return View();
            }
            else
            {
                HttpContext.Session.SetString("user", usuarioEncontrado.Login);
                return RedirectToAction("Index");
            }

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
