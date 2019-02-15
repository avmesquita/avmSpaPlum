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
    public class CarrinhoController : Controller
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        /*
        // GET: Carrinho
        public async Task<ActionResult> Index()
        {
            return View(await db.CarrinhoModels.ToListAsync());
        }

        // GET: Carrinho/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrinho carrinho = await db.CarrinhoModels.FindAsync(id);
            if (carrinho == null)
            {
                return HttpNotFound();
            }
            return View(carrinho);
        }

        // GET: Carrinho/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carrinho/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodigoCarrinho,SessionID,IPCliente,Data,CodigoAgendamento")] Carrinho carrinho)
        {
            if (ModelState.IsValid)
            {
                db.CarrinhoModels.Add(carrinho);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(carrinho);
        }

        // GET: Carrinho/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrinho carrinho = await db.CarrinhoModels.FindAsync(id);
            if (carrinho == null)
            {
                return HttpNotFound();
            }
            return View(carrinho);
        }

        // POST: Carrinho/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CodigoCarrinho,SessionID,IPCliente,Data,CodigoAgendamento")] Carrinho carrinho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carrinho).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(carrinho);
        }

        // GET: Carrinho/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrinho carrinho = await db.CarrinhoModels.FindAsync(id);
            if (carrinho == null)
            {
                return HttpNotFound();
            }
            return View(carrinho);
        }

        // POST: Carrinho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Carrinho carrinho = await db.CarrinhoModels.FindAsync(id);
            db.CarrinhoModels.Remove(carrinho);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        */

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
