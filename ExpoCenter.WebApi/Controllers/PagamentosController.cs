﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpoCenter.Dominio.Entidades;
using ExpoCenter.Repositorios.SqlServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ExpoCenter.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PagamentosController : ControllerBase
    {
        private readonly ExpoCenterDbContext _context;

        public PagamentosController(ExpoCenterDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Agente")]
        public async Task<ActionResult<IEnumerable<Pagamento>>> GetPagamentos()
        {
          if (_context.Pagamentos == null)
          {
              return NotFound();
          }
            return await _context.Pagamentos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pagamento>> GetPagamento(int id)
        {
          if (_context.Pagamentos == null)
          {
              return NotFound();
          }
            var pagamento = await _context.Pagamentos.FindAsync(id);

            if (pagamento == null)
            {
                return NotFound();
            }

            return pagamento;
        }

        [HttpGet("cartao/{guidCartao}")]
        public async Task<ActionResult<IEnumerable<Pagamento>>> GetPagamentosByCartao(Guid guidCartao)
        {
            if (_context.Pagamentos == null)
            {
                return NotFound();
            }

            return await _context.Pagamentos
                .Where(p => p.IdCartao == guidCartao)
                .ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPagamento(int id, Pagamento pagamento)
        {
            if (id != pagamento.Id)
            {
                return BadRequest();
            }

            _context.Entry(pagamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagamentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pagamentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pagamento>> PostPagamento(Pagamento pagamento)
        {
          if (_context.Pagamentos == null)
          {
              return Problem("Entity set 'ExpoCenterDbContext.Pagamentos'  is null.");
          }
            _context.Pagamentos.Add(pagamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPagamento", new { id = pagamento.Id }, pagamento);
        }

        // DELETE: api/Pagamentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePagamento(int id)
        {
            if (_context.Pagamentos == null)
            {
                return NotFound();
            }
            var pagamento = await _context.Pagamentos.FindAsync(id);
            if (pagamento == null)
            {
                return NotFound();
            }

            _context.Pagamentos.Remove(pagamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PagamentoExists(int id)
        {
            return (_context.Pagamentos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
