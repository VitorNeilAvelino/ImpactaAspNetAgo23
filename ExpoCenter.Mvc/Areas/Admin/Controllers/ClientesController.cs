using ExpoCenter.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ExpoCenter.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClientesController : Controller
    {
        private readonly IClienteRepositorio _repositorio;

        public ClientesController(IClienteRepositorio repositorio)
        {
            _repositorio = repositorio;
            _repositorio.Caminho = "clientes";
            //_repositorio.Token = Request.Cookies["apiExpoCenterUserToken"];
        }

        public async Task<ActionResult> Index()
        {
            _repositorio.Token = Request.Cookies["apiExpoCenterUserToken"];

            try
            {
                return View(await _repositorio.Get());
            }
            catch (HttpRequestException ex)
            {
                switch (ex.StatusCode)
                {
                    case HttpStatusCode.Forbidden:
                        return Forbid();
                    case HttpStatusCode.Unauthorized:
                        //return Unauthorized();
                        return Redirect("~/Identity/Account/Login");
                }

                throw;
            }            
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClientesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClientesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}