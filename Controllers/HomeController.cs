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
          
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        /*[HttpPost]
        public IActionResult Login(string login, string senha)
        {
            if(login != "admin" || senha != "123")
            {
                ViewData["Erro"] = "Senha inválida";
                return View();
            }
            else
            {
                HttpContext.Session.SetString("user", "admin");
                return RedirectToAction("Index");
            }
        }*/
          public IActionResult Cadastro()
        {
            if(HttpContext.Session.GetInt32("idUsuario")== null || HttpContext.Session.GetInt32("tipoUsuario")!= 0 )
                return RedirectToAction("Login","Home");
            return View();
        }
        public IActionResult cadastroCliente()
        {
            return View();
        }
        [HttpPost]
        public IActionResult cadastroCliente(Usuario u)
        {
            UsuarioBanco ub = new UsuarioBanco();
            ub.Insert(u);
            ViewBag.Mensagem="Usuario Cadastrado com Sucesso!";
            return View();
        }
        [HttpPost]
        public IActionResult Cadastro(Usuario usuario){
            UsuarioBanco ub = new UsuarioBanco();
            ub.Insert(usuario);
            ViewBag.Mensagem="Usuario Cadastrado com exito!";
            return View();
        }
        public IActionResult Listar()
        {
            if(HttpContext.Session.GetInt32("idUsuario")==null)
                return RedirectToAction("Login");
            UsuarioBanco ub = new UsuarioBanco();
            List<Usuario> listar = ub.Query();
            return View(listar);
        }
     
        public IActionResult Editar(int Id)
        {
            UsuarioBanco ub = new UsuarioBanco();
            Usuario usuario = ub.ConsultaPorId(Id);
            return View();
        }
        public IActionResult ListarColab()
        { 
            UsuarioBanco ub = new UsuarioBanco();
            List<Usuario> listar = ub.Query();
            return View(listar);
        }
        public IActionResult Gravar(Usuario usuario)
        {
            UsuarioBanco ub = new UsuarioBanco();
            ub.Atualizar(usuario);
            return RedirectToAction("Listar");
        }
        public IActionResult Remover(int Id)
        {
            UsuarioBanco ub = new UsuarioBanco();
            ub.Remover(Id);
            return RedirectToAction("Listar");
        }
       
        public IActionResult LoginCliente()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Usuario user)
        {
            UsuarioBanco ub = new UsuarioBanco();
            Usuario usuario = ub.QueryLogin(user);
            if(usuario != null)
            {
                HttpContext.Session.SetInt32("idUsuario", usuario.Id);
                HttpContext.Session.SetString("nomeUsuario", usuario.Nome);
                HttpContext.Session.SetInt32("tipoUsuario", usuario.Tipo);
                return RedirectToAction("Cadastro","Livro");
            }
            else
            {
                ViewBag.Mensagem = "Falha Login!";
                return View();
            }

        }
        [HttpPost]
        public IActionResult LoginCliente(Usuario user)
        {
            UsuarioBanco ub = new UsuarioBanco();
            Usuario usuario = ub.QueryLogin(user);
            if(usuario != null)
            {
                HttpContext.Session.SetInt32("idUsuario", usuario.Id);
                HttpContext.Session.SetString("nomeUsuario", usuario.Nome);
                 HttpContext.Session.SetInt32("tipoUsuario", usuario.Tipo);
                return RedirectToAction("Cadastro","Livro");
            }
            else
            {
                ViewBag.Mensagem = "Falha Login!";
                return View();
            }

        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
