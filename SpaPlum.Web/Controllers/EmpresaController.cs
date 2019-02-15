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

namespace SpaPlum.Web.Controllers
{
    public class EmpresaController : Controller
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        // GET: Empresa
        public async Task<ActionResult> Index()
        {
			if (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0"))
				RedirectToAction("NotAuthorized", "Erro");

			var empresas = db.EmpresaModels.Include(e => e.Plano);
            return View(await empresas.ToListAsync());
        }

        // GET: Empresa/Details/5
        public async Task<ActionResult> Details(int? id)
        {
			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			if (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0"))
				RedirectToAction("NotAuthorized", "Erro");
			Empresa empresa = await db.EmpresaModels.FindAsync(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // GET: Empresa/Create
        public ActionResult Create()
        {
			if (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0"))
				RedirectToAction("NotAuthorized", "Erro");

			ViewBag.CodigoPlano = new SelectList(db.PlanoModels, "CodigoPlano", "Nome");
            return View();
        }

        // POST: Empresa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodigoEmpresa,Nome,RazaoSocial,CNPJ,Email,Telefone,Ativo,DataCadastro,CodigoPlano")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.EmpresaModels.Add(empresa);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoPlano = new SelectList(db.PlanoModels, "CodigoPlano", "Nome", empresa.CodigoPlano);
            return View(empresa);
        }

        // GET: Empresa/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			if (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0"))
				RedirectToAction("NotAuthorized", "Erro");

			Empresa empresa = await db.EmpresaModels.FindAsync(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodigoPlano = new SelectList(db.PlanoModels, "CodigoPlano", "Nome", empresa.CodigoPlano);
            return View(empresa);
        }

        // POST: Empresa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CodigoEmpresa,Nome,RazaoSocial,CNPJ,Email,Telefone,Ativo,DataCadastro,CodigoPlano")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresa).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoPlano = new SelectList(db.PlanoModels, "CodigoPlano", "Nome", empresa.CodigoPlano);
            return View(empresa);
        }

        // GET: Empresa/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			if (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0"))
				RedirectToAction("NotAuthorized", "Erro");

			Empresa empresa = await db.EmpresaModels.FindAsync(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // POST: Empresa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Empresa empresa = await db.EmpresaModels.FindAsync(id);
            db.EmpresaModels.Remove(empresa);
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
