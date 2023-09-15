using AutoMapper;
using ExpoCenter.Mvc.Models;
using ExpoCenter.Repositorios.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpoCenter.Mvc.Controllers
{
    public class ParticipantesController : Controller
    {
        private readonly ExpoCenterDbContext dbContext;
        private readonly IMapper mapper;

        public ParticipantesController(ExpoCenterDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            return View(mapper.Map<List<ParticipanteIndexViewModel>>(dbContext.Participantes)); 
        }

        // GET: ParticipantesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ParticipantesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParticipantesController/Create
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

        // GET: ParticipantesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ParticipantesController/Edit/5
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

        // GET: ParticipantesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ParticipantesController/Delete/5
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
