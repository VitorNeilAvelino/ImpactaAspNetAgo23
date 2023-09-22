using AutoMapper;
using ExpoCenter.Dominio.Entidades;
using ExpoCenter.Mvc.Helpers;
using ExpoCenter.Mvc.Models;
using ExpoCenter.Repositorios.SqlServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json;
using static ExpoCenter.Dominio.Entidades.PerfilUsuario;

namespace ExpoCenter.Mvc.Controllers
{
    [Authorize]
    public class ParticipantesController : Controller
    {
        private readonly ExpoCenterDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<ParticipantesController> logger;

        public ParticipantesController(ExpoCenterDbContext dbContext, IMapper mapper, ILogger<ParticipantesController> logger)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
        }

        [AllowAnonymous]
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
            var viewModel = new ParticipanteCreateViewModel();

            viewModel.Eventos = mapper.Map<List<EventoGridViewModel>>(dbContext.Eventos);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ParticipanteCreateViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                var participante = mapper.Map<Participante>(viewModel);

                participante.Eventos = new List<Evento>();

                foreach (var eventoViewModel in viewModel.Eventos.Where(e => e.Selecionado))
                {
                    participante.Eventos.Add(dbContext.Eventos.Find(eventoViewModel.Id));
                }

                dbContext.Participantes.Add(participante);
                dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Error");
            }
        }

        //[Authorize(Roles = "Administrador, Gerente")]
        [AuthorizeRole(Supervisor, Gerente, Agente)]
        public ActionResult Edit(int id)
        {
            var participante = dbContext.Participantes
                .Include(p => p.Eventos)
                .SingleOrDefault(p => p.Id == id);

            if (participante == null)
            {
                return NotFound();
            }

            var viewModel = mapper.Map<ParticipanteCreateViewModel>(participante);

            viewModel.Eventos = mapper.Map<List<EventoGridViewModel>>(dbContext.Eventos);

            if (participante.Eventos != null)
            {
                foreach (var evento in participante.Eventos)
                {
                    viewModel.Eventos.Single(e => e.Id == evento.Id).Selecionado = true;
                }
            }

            return View(viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ParticipanteCreateViewModel viewModel)
        {
            try
            {
                throw new Exception("Teste de erro.");

                logger.LogInformation(JsonSerializer.Serialize(viewModel));

                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                var participante = dbContext.Participantes
                    .Include(p => p.Eventos)
                    .SingleOrDefault(p => p.Id == viewModel.Id);

                if (participante == null)
                {
                    return NotFound();
                }

                dbContext.Entry(participante).CurrentValues.SetValues(viewModel);

                foreach (var eventoViewModel in viewModel.Eventos)
                {
                    if (eventoViewModel.Selecionado)
                    {
                        if (participante.Eventos.Any(e => e.Id == eventoViewModel.Id))
                        {
                            continue;
                        }

                        participante.Eventos.Add(dbContext.Eventos.Single(e => e.Id == eventoViewModel.Id));
                    }
                    else
                    {
                        participante.Eventos.Remove(dbContext.Eventos.Single(e => e.Id == eventoViewModel.Id));
                    }
                }

                dbContext.Update(participante);
                dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException)
                {
                    switch (sqlException.Message)
                    {
                        case string mensagem when mensagem.Contains("IX_Participante_Cpf"):
                            ModelState.AddModelError("", $"O CPF {viewModel.Cpf} já está cadastrado.");
                            break;
                        case string mensagem when mensagem.Contains("IX_Participante_Email"):
                            ModelState.AddModelError("", $"O email {viewModel.Email} já está cadastrado.");
                            break;
                    }

                    if (!ModelState.IsValid)
                    {
                        return View(viewModel);
                    }
                }

                throw;
            }
            catch(Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Exception = ex });
            }
        }

        [Authorize(Policy = "ParticipantesExcluir")]
        public ActionResult Delete(int id)
        {
            //if (!User.HasClaim("Participantes", "Deletar"))
            //{
            //    return new ForbidResult();
            //}

            var participante = dbContext.Participantes
                //.Include(p => p.Eventos)
                .SingleOrDefault(p => p.Id == id);

            if (participante == null)
            {
                return NotFound();
            }

            return View(mapper.Map<ParticipanteIndexViewModel>(participante));
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
