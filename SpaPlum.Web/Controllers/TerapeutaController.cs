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
using System.Data.Entity.Validation;
using PagedList;

namespace SpaPlum.Web.Controllers
{
    public class TerapeutaController : SpaPlumBaseController
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        /*
        // GET: Terapeuta
        [Authorize]
        public async Task<ActionResult> Index()
        {
            return View(await db.TerapeutaModels.OrderBy(x => x.CodigoFilial).ThenBy(x => x.Nome).ToListAsync());
        }
        */

        [Authorize]
        public ActionResult Index(int? page)
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			var terapeutaModels = db.TerapeutaModels.Include(t => t.Filial)
                                                    .Include(t => t.Horarios)
                                                    .Include(t => t.Servicos)
                                                    .OrderBy(x => x.Filial.Nome)
                                                    .ThenBy(a => a.Nome)
                                                    .ToPagedList(page ?? 1, 10);
            return View(terapeutaModels);
        }



        // GET: Terapeuta/Details/5
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

			Terapeuta terapeuta = await db.TerapeutaModels.FindAsync(id);
            if (terapeuta == null)
            {
                return HttpNotFound();
            }
            return View(terapeuta);
        }

        // GET: Terapeuta/Create
        [Authorize]
        public ActionResult Create()
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			ViewBag.ListaFilial = new SelectList(db.FilialModels, "CodigoFilial", "Nome");

            return View();
        }

        // POST: Terapeuta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodigoTerapeuta,Nome,Ativa,DataCadastro,CodigoFilial,EMail,Telefone")] Terapeuta terapeuta)
        {            
            terapeuta.DataCadastro = DateTime.Now;
            
            if (ModelState.IsValid)
            {
                terapeuta.Filial = db.FilialModels.Find(terapeuta.CodigoFilial);            

                db.TerapeutaModels.Add(terapeuta);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ListaFilial = new SelectList(db.FilialModels, "CodigoFilial", "Nome");

            return View(terapeuta);
        }

        // GET: Terapeuta/Edit/5
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

			Terapeuta terapeuta = await db.TerapeutaModels.FindAsync(id);
            if (terapeuta == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListaFilial = new SelectList(db.FilialModels, "CodigoFilial", "Nome", terapeuta.CodigoFilial);
            return View(terapeuta);
        }

        // POST: Terapeuta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CodigoTerapeuta,Nome,Ativa,DataCadastro,CodigoFilial,EMail,Telefone")] Terapeuta terapeuta)
        {            
            if (ModelState.IsValid)
            {
                terapeuta.Filial = db.FilialModels.Find(terapeuta.CodigoFilial);

                db.Entry(terapeuta).State = System.Data.Entity.EntityState.Modified;               
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ListaFilial = new SelectList(db.FilialModels, "CodigoFilial", "Nome", terapeuta.CodigoFilial);
            return View(terapeuta);
        }

        // GET: Terapeuta/Delete/5
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

			Terapeuta terapeuta = await db.TerapeutaModels.FindAsync(id);
            if (terapeuta == null)
            {
                return HttpNotFound();
            }
            return View(terapeuta);
        }

        // POST: Terapeuta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Terapeuta terapeuta = await db.TerapeutaModels.FindAsync(id);
            terapeuta.Filial = await db.FilialModels.AsNoTracking().Where(x => x.CodigoFilial == terapeuta.CodigoFilial).FirstOrDefaultAsync();
            try
            {

                db.TerapeutaModels.Remove(terapeuta);                
                await db.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                string msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    msg += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);

                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw new Exception("Falha na verificação de dados." + Environment.NewLine + msg);
            }

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
