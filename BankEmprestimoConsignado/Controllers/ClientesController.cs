using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankEmprestimoConsignado.Data;
using BankEmprestimoConsignado.Models;
using Microsoft.AspNetCore.Authorization;
using BankEmprestimoConsignado.Extensions;
using BankEmprestimoConsignado.Business;

namespace BankEmprestimoConsignado.Controllers
{
    //[Authorize( = "gerente")]
    //[ClaimsAuthorize("gerente", "editar")]
    public class ClientesController : Controller
    {
        private readonly BankContext _context;
        private EmprestimoConsignado regrasNegocio;
        private readonly BankContext2 _user;
        public ClientesController(BankContext context)
        {
            _context = context;
           regrasNegocio = new EmprestimoConsignado();
        }

        //[HttpPost]
        //public async Task<IActionResult> Login( string returnUrl = null)
        //{
        //    _user.Users.
        //    if (!ModelState.IsValid)
        //    {
        //        return View(user);
        //    }
            
        //    if(usuario != null)
        //    {
        //        FormsAuthentication.
        //    }
        //}

        // GET: Clientes
        //[AllowAnonymous]
        [Route("Clientes")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }

        // GET: Clientes/Details/5
        [Route("DetalhesCliente")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            var lista = _context.Emprestimos.Where(c => c.IdCliente == cliente.IdCliente).ToList();
            cliente.Emprestimos = lista;
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        [Route("CriarCliente")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,Nome,Cpf,Nascimento,Profissao,Margem,MargemCartao,Salario,Status")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        [Route("EditarClientes/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente,Nome,Cpf,Nascimento,Profissao,Margem,MargemCartao,Salario,Status")] Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.IdCliente))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        [Route("DeletarCliente")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.IdCliente == id);
        }
    }
}
