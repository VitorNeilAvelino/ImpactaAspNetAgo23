using Marketplace.Mvc.Models;
using Marketplace.Repositorios.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Marketplace.Mvc.Controllers
{
    public class PagamentosController : Controller
    {
        private readonly PagamentoRepositorio repositorio = new PagamentoRepositorio("http://localhost:34973/api"); // Erro de mime type se a porta mudar.

        public async Task<ActionResult> Index(Guid? guidCartao, string nomeCliente)
        {
            ViewBag.NomeCliente = nomeCliente;

            if (!guidCartao.HasValue)
            {
                return View(new List<PagamentoIndexViewModel>());
            }

            TempData["nomeCliente"] = nomeCliente;
            TempData["guidCartao"] = guidCartao;

            return View(PagamentoIndexViewModel.Mapear(await repositorio.GetByCartao(guidCartao.Value)));
        }

        public ActionResult Create()
        {
            ViewBag.NomeCliente = TempData.Peek("nomeCliente");
            ViewBag.GuidCartao = TempData.Peek("guidCartao");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(PagamentoCreateViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                var pagamentoResponse = await repositorio.Post(PagamentoCreateViewModel.Mapear(viewModel));

                if (pagamentoResponse.Status != (int)StatusPagamento.PagamentoOK)
                {
                    ModelState.AddModelError("", pagamentoResponse.DescricaoStatus);

                    return View(viewModel);
                }

                return RedirectToAction("Index", new { guidCartao = TempData["guidCartao"], nomeCliente = TempData["nomeCliente"] });
            }
            catch
            {
                return View("Error");
            }
        }        
    }
}