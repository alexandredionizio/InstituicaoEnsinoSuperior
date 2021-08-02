using InstituicaoEnsinoSuperior.Data;
using InstituicaoEnsinoSuperior.Data.DAL.Cadastros;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelo.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstituicaoEnsinoSuperior.Controllers
{
    public class InstituicaoController : Controller
    {
        private readonly IESContext _context;
        private readonly InstituicaoDAL instituicaoDAL; 

        public InstituicaoController(IESContext context)
        {
            //this._context = context;
            _context = context;
            instituicaoDAL = new InstituicaoDAL(context);
        }

        public async Task<IActionResult> Details(long? id)
        {
            return await ObterVisaoInstituicaoPorId(id);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            return await ObterVisaoInstituicaoPorId(id);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            return await ObterVisaoInstituicaoPorId(id);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Endereco")] Instituicao instituicao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //_context.Add(instituicao);
                    //await _context.SaveChangesAsync();
                    await instituicaoDAL.GravarInstituicao(instituicao);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }
            return View(instituicao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("InstituicaoID, Nome, Endereco")] Instituicao instituicao)
        {
            if (id != instituicao.InstituicaoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await instituicaoDAL.GravarInstituicao(instituicao);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await InstituicaoExists(instituicao.InstituicaoID))
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
            return View(instituicao);
        }

        public async Task<IActionResult> Index()
        {
            //return View(await _context.Instituicoes.OrderBy(c => c.Nome).ToListAsync());
            return View(await instituicaoDAL.ObterInstituicoesClassificadasPorNome().ToListAsync());
        }

        //private bool InstituicaoExists(long? id)
        //{
        //    return _context.Instituicoes.Any(e => e.InstituicaoID == id);
        //}

        private async Task<bool> InstituicaoExists(long? id)
        {
            return await instituicaoDAL.ObterInstituicaoPorId((long)id) != null;
        }

        //POST: Departamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var instituicao = await instituicaoDAL.EliminarInstituicaoPorId((long)id);
            TempData["Message"] = "Instituição " + instituicao.Nome.ToUpper() + " foi removida";
            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> ObterVisaoInstituicaoPorId(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituicao = await instituicaoDAL.ObterInstituicaoPorId((long) id);
            if (instituicao == null)
            {
                return NotFound();
            }
            return View(instituicao);
        }
    }
}
