using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankEmprestimoConsignado.Data;
using BankEmprestimoConsignado.Models;
using BankEmprestimoConsignado.Business;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BankEmprestimoConsignado.Extensions;

namespace BankEmprestimoConsignado.Controllers
{
    //[Authorize]
    [Authorize]
    public class EmprestimosController : Controller
    {
        private readonly BankContext _context;
        private EmprestimoConsignado regrasNegocio;
        private SignInManager<IdentityUser> SignInManager;
        private UserManager<IdentityUser> UserManager;
        public EmprestimosController(BankContext context)
        {
            _context = context;
           regrasNegocio = new EmprestimoConsignado();
        }

        // GET: Emprestimos
        //[AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            // Adicionados validação de usuário
            var usuarioId = User.Identity;
            var usuario = User.Claims.ToArray();
            //if (usuario[3].Type == "cliente")
            //{
            //    var emprestimoconsignadoContext = _context.Emprestimos.Include(e => e.IdClienteNavigation).Where(u => u.IdClienteNavigation == usuarioId);
            //    return View(await emprestimoconsignadoContext.ToListAsync());
            //}
            var bankContext = _context.Emprestimos.Include(e => e.IdClienteNavigation);
            return View(await bankContext.ToListAsync());
        }

        // GET: Emprestimos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimo = await _context.Emprestimos
                .Include(e => e.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdEmprestimo == id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }

        // GET: Emprestimos/Create
        //[ClaimsAuthorize("gerente", "editar")]
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Nome");
            return View();
        }

        // POST: Emprestimos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEmprestimo,IdCliente,ValorLiberado,ValorEmprestimo,DataVenc,ValorParcela,QtdParcela,TaxaJuros,QtdParcelaRest,StatusEmprest,TipoEmprest")] Emprestimo emprestimo)
        {
            if (ModelState.IsValid)
            {
                //RetornaCliente(emprestimo.IdCliente);
                //if (regrasNegocio.PropostaDeEmprestimo(emprestimo.IdClienteNavigation, emprestimo.ValorEmprestimo, emprestimo.QtdParcela, emprestimo.TaxaJuros, emprestimo.TipoEmprest, emprestimo.DataVenc))
                //{

                //}
                _context.Add(emprestimo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Nome", emprestimo.IdCliente);
            return View(emprestimo);
        }

        // GET: Emprestimos/Edit/5
        //[ClaimsAuthorize("gerente", "editar")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimo = await _context.Emprestimos.FindAsync(id);
            if (emprestimo == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Nome", emprestimo.IdCliente);
            return View(emprestimo);
        }

        // POST: Emprestimos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEmprestimo,IdCliente,ValorLiberado,ValorEmprestimo,DataVenc,ValorParcela,QtdParcela,TaxaJuros,QtdParcelaRest,StatusEmprest,TipoEmprest")] Emprestimo emprestimo)
        {
            if (id != emprestimo.IdEmprestimo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emprestimo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmprestimoExists(emprestimo.IdEmprestimo))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Nome", emprestimo.IdCliente);
            return View(emprestimo);
        }

        // GET: Emprestimos/Delete/5
        //[ClaimsAuthorize("gerente", "editar")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimo = await _context.Emprestimos
                .Include(e => e.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdEmprestimo == id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }

        // POST: Emprestimos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emprestimo = await _context.Emprestimos.FindAsync(id);
            _context.Emprestimos.Remove(emprestimo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmprestimoExists(int id)
        {
            return _context.Emprestimos.Any(e => e.IdEmprestimo == id);
        }
        private Cliente RetornaCliente(int cpfCliente)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.Cpf == cpfCliente);
            return cliente;
        }
    }
}
