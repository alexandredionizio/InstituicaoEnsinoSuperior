using InstituicaoEnsinoSuperior.Data;
using InstituicaoEnsinoSuperior.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstituicaoEnsinoSuperior.Controllers
{
    public class InstituicaoController : Controller
    {
        private readonly IESContext _context;

        public InstituicaoController(IESContext context)
        {
            this._context = context;
        }


        //private static IList<Instituicao> instituicoes = new List<Instituicao>()
        //{
        //    new Instituicao()
        //    {
        //        InstituicaoID = 1,
        //        Nome = "UniParaná",
        //        Endereco = "Paraná"
        //    },
        //    new Instituicao()
        //    {
        //        InstituicaoID = 2,
        //        Nome = "UniSanta",
        //        Endereco = "Santa Catarina"
        //    },
        //    new Instituicao()
        //    {
        //        InstituicaoID = 3,
        //        Nome = "UniSãoPaulo",
        //        Endereco = "São Paulo"
        //    },
        //    new Instituicao()
        //    {
        //        InstituicaoID = 4,
        //        Nome = "UniSulGrandense",
        //        Endereco = "Rio Grande do Sul"
        //    },
        //    new Instituicao()
        //    {
        //        InstituicaoID = 5,
        //        Nome = "UniCarioca",
        //        Endereco = "Rio de Janeiro"
        //    }
        //};


        ////Get: Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        public IActionResult Create()
        {
            return View();
        }

        //public IActionResult Index()
        //{
        //    //return View(instituicoes);
        //    return View(instituicoes.OrderBy(i => i.Nome));
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstituicaoID,Nome,Endereco")] Instituicao instituicao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(instituicao);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }
            return View(instituicao);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Instituicoes.OrderBy(c => c.Nome).ToListAsync());
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Instituicao instituicao)
        //{
        //    instituicoes.Add(instituicao);
        //    instituicao.InstituicaoID = instituicoes.Select(i => i.InstituicaoID).Max() + 1;
        //    return RedirectToAction("Index");
        //}

        //public ActionResult Edit(long id)
        //{
        //    return View(instituicoes.Where(i => i.InstituicaoID == id).First());
        //}

        public async Task<IActionResult> Edit(long? id)
        {
            //Verifica se o id veio nulo e caso positivo gero um erro HTTP 404
            if (id == null)
            {
                return NotFound();
            }

            //busca o id pela PK e verifico se encontrou. Caso negativo gero outro erro HTTP 404
            var instituicao = await _context.Instituicoes.SingleOrDefaultAsync(m => m.InstituicaoID == id);
            if (instituicao == null)
            {
                return NotFound();
            }

            //caso encontrou o id chamo a view passando o departamento
            return View(instituicao);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Instituicao instituicao)
        //{
        //    instituicoes.Remove(instituicoes.Where(i => i.InstituicaoID == instituicao.InstituicaoID).First());
        //    instituicoes.Add(instituicao);
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("InstituicaoID,Nome,Endereco")] Instituicao instituicao)
        {
            //Verifica se os valores recebidos para o id e InstituicaoID do objeto insituicao são iguais.
            //Caso negativo dispara o erro HTTP 404
            if (id != instituicao.InstituicaoID)
            {
                return NotFound();
            }

            //Verifica se o modelo é válido, ou seja, se não há nenhuma validação de erro
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instituicao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstituicaoExists(instituicao.InstituicaoID))
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

        private bool InstituicaoExists(long? id)
        {
            return _context.Instituicoes.Any(e => e.InstituicaoID == id);
        }

        //public ActionResult Details(long id)
        //{
        //    return View(instituicoes.Where(i => i.InstituicaoID == id).First());
        //}

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituicao = await _context.Instituicoes.SingleOrDefaultAsync(m => m.InstituicaoID == id);
            if (instituicao == null)
            {
                return NotFound();
            }
            return View(instituicao);
        }

        //public ActionResult Delete(long id)
        //{
        //    return View(instituicoes.Where(i => i.InstituicaoID == id).First());
        //}

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituicao = await _context.Instituicoes.SingleOrDefaultAsync(m => m.InstituicaoID == id);
            if (instituicao == null)
            {
                return NotFound();
            }
            return View(instituicao);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(Instituicao instituicao)
        //{
        //    instituicoes.Remove(instituicoes.Where(i => i.InstituicaoID == instituicao.InstituicaoID).First());
        //    return RedirectToAction("Index");
        //}



        //POST: Departamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var instituicao = await _context.Instituicoes.SingleOrDefaultAsync(m => m.InstituicaoID == id);
            _context.Instituicoes.Remove(instituicao);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Instituição " + instituicao.Nome.ToUpper() + " foi removida";
            return RedirectToAction(nameof(Index));
        }
    }
}
