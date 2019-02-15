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
    public class FilialConfiguracaoController : Controller
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        // GET: FilialConfiguracao
        public async Task<ActionResult> Index()
        {
            var filialConfiguracaos = db.FilialConfiguracaoModels.Include(f => f.Filial);
            return View(await filialConfiguracaos.ToListAsync());
        }

        // GET: FilialConfiguracao/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FilialConfiguracao filialConfiguracao = await db.FilialConfiguracaoModels.Include(f => f.Filial).Where(x => x.CodigoConfiguracao== id).FirstOrDefaultAsync();
            if (filialConfiguracao == null)
            {
                return HttpNotFound();
            }
            return View(filialConfiguracao);
        }

        // GET: FilialConfiguracao/Create
        public ActionResult Create()
        {
            ViewBag.CodigoFilial = new SelectList(db.FilialModels, "CodigoFilial", "Nome");
            return View();
        }

        // POST: FilialConfiguracao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodigoConfiguracao,CodigoFilial,EmailSmtpHost,EmailSmtpPort,EmailSmtpSSL,EmailSmtpAccount,EmailSmtpPassword")] FilialConfiguracao filialConfiguracao)
        {
            if (ModelState.IsValid)
            {
                db.FilialConfiguracaoModels.Add(filialConfiguracao);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoFilial = new SelectList(db.FilialModels, "CodigoFilial", "Nome", filialConfiguracao.CodigoFilial);
            return View(filialConfiguracao);
        }

        // GET: FilialConfiguracao/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FilialConfiguracao filialConfiguracao = await db.FilialConfiguracaoModels.FindAsync(id);
            if (filialConfiguracao == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodigoFilial = new SelectList(db.FilialModels, "CodigoFilial", "Nome", filialConfiguracao.CodigoFilial);
            return View(filialConfiguracao);
        }

        // POST: FilialConfiguracao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CodigoConfiguracao,CodigoFilial,EmailSmtpHost,EmailSmtpPort,EmailSmtpSSL,EmailSmtpAccount,EmailSmtpPassword")] FilialConfiguracao filialConfiguracao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(filialConfiguracao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoFilial = new SelectList(db.FilialModels, "CodigoFilial", "Nome", filialConfiguracao.CodigoFilial);
            return View(filialConfiguracao);
        }

        // GET: FilialConfiguracao/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FilialConfiguracao filialConfiguracao = await db.FilialConfiguracaoModels.FindAsync(id);
            if (filialConfiguracao == null)
            {
                return HttpNotFound();
            }
            return View(filialConfiguracao);
        }

        // POST: FilialConfiguracao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FilialConfiguracao filialConfiguracao = await db.FilialConfiguracaoModels.FindAsync(id);
            db.FilialConfiguracaoModels.Remove(filialConfiguracao);
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
