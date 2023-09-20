using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpoCenter.Dominio.Entidades;
using ExpoCenter.Repositorios.SqlServer;
using AutoMapper;
using ExpoCenter.Mvc.Models;
using Microsoft.Extensions.Logging;

namespace ExpoCenter.Mvc.Controllers
{
    public class EventosController : Controller
    {
        private readonly ExpoCenterDbContext _context;
        private readonly IMapper _mapper;

        public EventosController(ExpoCenterDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<List<EventoViewModel>>(await _context.Eventos.ToListAsync()));
        }

        public async Task<IActionResult> Participantes(int eventoId)
        {
            var evento = await _context.Eventos
                .Include(e => e.Participantes)
                .SingleOrDefaultAsync(e => e.Id == eventoId);

            var viewModel = _mapper.Map<EventoViewModel>(evento);

            viewModel.Participantes = _mapper.Map<List<ParticipanteGridViewModel>>(_context.Participantes.OrderBy(p => p.Nome));

            if (evento.Participantes != null)
            {
                foreach (var participante in evento.Participantes)
                {
                    viewModel.Participantes.Single(p => p.Id == participante.Id).Selecionado = true;
                }
            }

            return View(viewModel);        
        }

        [HttpPost]
        public async Task<IActionResult> Participantes(EventoViewModel viewModel)
        {
            var evento = await _context.Eventos
                //.Include(e => e.Participantes)
                .SingleOrDefaultAsync(e => e.Id == viewModel.Id);

            foreach (var participanteViewModel in viewModel.Participantes)
            {
                if (participanteViewModel.Selecionado)
                {
                    if (evento.Participantes.Any(p => p.Id == participanteViewModel.Id))
                    {
                        continue;
                    }

                    evento.Participantes.Add(await _context.Participantes.SingleAsync(p => p.Id == participanteViewModel.Id));
                }
                else
                {
                    evento.Participantes.Remove(await _context.Participantes.SingleAsync(p => p.Id == participanteViewModel.Id));
                }
            }

            _context.Update(evento);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Eventos == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FirstOrDefaultAsync(m => m.Id == id);

            if (evento == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<EventoViewModel>(evento));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(_mapper.Map<Evento>(viewModel));
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Eventos == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FindAsync(id);

            if (evento == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<EventoViewModel>(evento));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EventoViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_mapper.Map<Evento>(viewModel));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Eventos == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FirstOrDefaultAsync(m => m.Id == id);
            
            if (evento == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<EventoViewModel>(evento));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Eventos == null)
            {
                return Problem("Entity set 'ExpoCenterDbContext.Eventos'  is null.");
            }

            var evento = await _context.Eventos.FindAsync(id);

            if (evento != null)
            {
                _context.Eventos.Remove(evento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
          return (_context.Eventos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
