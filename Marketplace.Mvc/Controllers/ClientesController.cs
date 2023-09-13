using Marketplace.Mvc.Models;
using Marketplace.Repositorios.SqlServer.DbFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Marketplace.Mvc.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly ClienteRepositorio clienteRepositorio = new ClienteRepositorio();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(Mapear(clienteRepositorio.Selecionar()));
        }

        private List<ClienteViewModel> Mapear(List<Cliente> clientes)
        {
            var viewModel = new List<ClienteViewModel>();

            foreach (var cliente in clientes)
            {
                viewModel.Add(Mapear(cliente));
            }

            return viewModel;
        }

        private ClienteViewModel Mapear(Cliente cliente)
        {
            var viewModel = new ClienteViewModel();

            viewModel.Telefone = Convert.ToUInt64(Regex.Replace(cliente.Telefone, @"\D", "")).ToString(@"(00) 0 0000-0000");
            viewModel.Nome = cliente.Nome;
            viewModel.Email = cliente.Email;
            viewModel.Documento = Convert.ToUInt64(Regex.Replace(cliente.Documento, @"\D", "")).ToString(@"000\.000\.000-00");
            viewModel.Id = cliente.Id;
            viewModel.GuidCartao = cliente.Cartoes.FirstOrDefault()?.Guid;

            return viewModel;
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int id)
        {
            return View(Mapear(clienteRepositorio.Selecionar(id)));
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                clienteRepositorio.Inserir(Mapear(viewModel));

                return RedirectToAction("Index");
            }
            catch
            {
               return View("Error");                
            }
        }

        private Cliente Mapear(ClienteViewModel viewModel)
        {
            var cliente = new Cliente();

            cliente.Telefone = Regex.Replace(viewModel.Telefone, @"\D", "");
            cliente.Email = viewModel.Email;
            cliente.Documento = viewModel.Documento.Replace(".", "").Replace("-", "");
            cliente.Id = viewModel.Id;
            cliente.Nome = viewModel.Nome;

            return cliente;
        }

        [Authorize(Roles = "Gerente, PosVenda, Tecnologia")]
        public ActionResult Edit(int id)
        {
            return View(Mapear(clienteRepositorio.Selecionar(id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gerente, PosVenda, Tecnologia")]
        public ActionResult Edit(ClienteViewModel viewModel)
        {
            try
            {
                //var nome = collection["nome"];

                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                clienteRepositorio.Atualizar(Mapear(viewModel));

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult Delete(int id)
        {
            var usuarioLogado = (ClaimsPrincipal)Thread.CurrentPrincipal;

            if (!usuarioLogado.HasClaim("Clientes", "Deletar"))
            {
                return RedirectToAction("Login", "Account");
            }

            return View(Mapear(clienteRepositorio.Selecionar(id)));
        }

        // POST: Clientes/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                clienteRepositorio.Excluir(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
