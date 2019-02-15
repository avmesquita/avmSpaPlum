using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpaPlum.Web.Contexto;
using SpaPlum.Web.Entity;
using PagedList;

namespace SpaPlum.Web.Controllers
{
    public class FornecedorController : SpaPlumBaseController
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        /*
        // GET: Fornecedor
        [Authorize]
        public async Task<ActionResult> Index()
        {
            //return View(await db.FornecedorModels.OrderBy(x => x.CodigoFilial).ThenBy(x => x.Nome).ToListAsync());

            var fornecedorModels = db.FornecedorModels.Include(f => f.Filial);
            return View(await fornecedorModels.ToListAsync());
        }
        */

        [Authorize]
        public ActionResult Index(int? page)
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			var fornecedorModels = db.FornecedorModels.Include(t => t.Filial)
                                                      .OrderBy(x => x.Filial.Nome)
                                                      .OrderBy(a => a.Nome).ToPagedList(page ?? 1, 10);
            return View(fornecedorModels);
        }



        // GET: Fornecedor/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			Fornecedor fornecedor = await db.FornecedorModels.FindAsync(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        // GET: Fornecedor/Create
        [Authorize]
        public ActionResult Create()
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			ViewBag.ListaFilial = new SelectList(db.FilialModels.OrderBy(m => m.Nome), "CodigoFilial", "Nome");
            return View();
        }

        // POST: Fornecedor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodigoFornecedor,Nome,ForneceOque,Telefone1,Telefone2,Telefone3,Celular,EmailPrincipal,EmailSecundario,Endereço,Observacao,Ativo,CodigoFilial")] Fornecedor fornecedor)
        {
            fornecedor.DataCadastro = DateTime.Now;            
            if (ModelState.IsValid)
            {
                db.FornecedorModels.Add(fornecedor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ListaFilial = new SelectList(db.FilialModels.OrderBy(m => m.Nome), "CodigoFilial", "Nome");
            return View(fornecedor);
        }

        // GET: Fornecedor/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			Fornecedor fornecedor = await db.FornecedorModels.FindAsync(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListaFilial = new SelectList(db.FilialModels.OrderBy(m => m.Nome), "CodigoFilial", "Nome");
            return View(fornecedor);
        }

        // POST: Fornecedor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CodigoFornecedor,Nome,ForneceOque,Telefone1,Telefone2,Telefone3,Celular,EmailPrincipal,EmailSecundario,Endereço,Observacao,Ativo,DataCadastro,CodigoFilial")] Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fornecedor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ListaFilial = new SelectList(db.FilialModels.OrderBy(m => m.Nome), "CodigoFilial", "Nome");
            return View(fornecedor);
        }

        // GET: Fornecedor/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			Fornecedor fornecedor = await db.FornecedorModels.FindAsync(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        // POST: Fornecedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Fornecedor fornecedor = await db.FornecedorModels.FindAsync(id);
            db.FornecedorModels.Remove(fornecedor);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
