using Marketplace.Mvc.Models;
using Marketplace.Repositorios.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Marketplace.Mvc.Controllers
{
    public class PagamentosController : Controller
    {
        private readonly PagamentoRepositorio repositorio = new PagamentoRepositorio("http://localhost:34973/api");

        public async Task<ActionResult> Index(Guid? guidCartao, string nomeCliente)
        {
            if (!guidCartao.HasValue)
            {
                return View(new List<PagamentoIndexViewModel>());
            }

            return View(PagamentoIndexViewModel.Mapear(await repositorio.GetByCartao(guidCartao.Value)));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }        
    }
}
