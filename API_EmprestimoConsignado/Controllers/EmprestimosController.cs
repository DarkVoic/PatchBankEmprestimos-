using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankEmprestimoConsignado.Data;
using BankEmprestimoConsignado.Models;

namespace API_EmprestimoConsignado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmprestimosController : ControllerBase
    {
        private readonly BankContext _context;

        public EmprestimosController(BankContext context)
        {
            _context = context;
        }

        // GET: api/Emprestimoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emprestimo>>> GetEmprestimos()
        {
            return await _context.Emprestimos.ToListAsync();
        }

        // GET: api/Emprestimoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Emprestimo>> GetEmprestimo(int id)
        {
            var emprestimo = await _context.Emprestimos.FirstOrDefaultAsync(x => x.IdEmprestimo == id);

            if (emprestimo == null)
            {
                return NotFound();
            }

            return emprestimo;
        }

        // PUT: api/Emprestimoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmprestimo(int id, Emprestimo emprestimo)
        {
            if (id != emprestimo.IdEmprestimo)
            {
                return BadRequest();
            }

            _context.Entry(emprestimo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmprestimoExists(id))
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

        // POST: api/Emprestimoes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Emprestimo>> PostEmprestimo(Emprestimo emprestimo)
        {
            _context.Emprestimos.Add(emprestimo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmprestimoExists(emprestimo.IdEmprestimo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEmprestimo", new { id = emprestimo.IdEmprestimo }, emprestimo);
        }

        // DELETE: api/Emprestimoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Emprestimo>> DeleteEmprestimo(int id)
        {
            var emprestimo = await _context.Emprestimos.FindAsync(id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            _context.Emprestimos.Remove(emprestimo);
            await _context.SaveChangesAsync();

            return emprestimo;
        }

        private bool EmprestimoExists(int id)
        {
            return _context.Emprestimos.Any(e => e.IdEmprestimo == id);
        }
    }
}
