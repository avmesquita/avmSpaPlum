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
    public class CarrinhoItemController : Controller
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        /*
         
        // GET: CarrinhoItem
        public async Task<ActionResult> Index()
        {
            var carrinhoItemModels = db.CarrinhoItemModels.Include(c => c.Servico).Include(c => c.Terapeuta);
            return View(await carrinhoItemModels.ToListAsync());
        }

        // GET: CarrinhoItem/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarrinhoItem carrinhoItem = await db.CarrinhoItemModels.FindAsync(id);
            if (carrinhoItem == null)
            {
                return HttpNotFound();
            }
            return View(carrinhoItem);
        }

        // GET: CarrinhoItem/Create
        public ActionResult Create()
        {
            ViewBag.CodigoServico = new SelectList(db.ServicoModels, "CodigoServico", "Nome");
            ViewBag.CodigoTerapeuta = new SelectList(db.TerapeutaModels, "CodigoTerapeuta", "Nome");
            return View();
        }

        // POST: CarrinhoItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodigoCarrinhoItem,CodigoCarrinho,CodigoServico,CodigoTerapeuta,TipoDeMassagem,QtdePeriodos,Preco")] CarrinhoItem carrinhoItem)
        {
            if (ModelState.IsValid)
            {
                db.CarrinhoItemModels.Add(carrinhoItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoServico = new SelectList(db.ServicoModels, "CodigoServico", "Nome", carrinhoItem.CodigoServico);
            ViewBag.CodigoTerapeuta = new SelectList(db.TerapeutaModels, "CodigoTerapeuta", "Nome", carrinhoItem.CodigoTerapeuta);
            return View(carrinhoItem);
        }

        // GET: CarrinhoItem/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarrinhoItem carrinhoItem = await db.CarrinhoItemModels.FindAsync(id);
            if (carrinhoItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodigoServico = new SelectList(db.ServicoModels, "CodigoServico", "Nome", carrinhoItem.CodigoServico);
            ViewBag.CodigoTerapeuta = new SelectList(db.TerapeutaModels, "CodigoTerapeuta", "Nome", carrinhoItem.CodigoTerapeuta);
            return View(carrinhoItem);
        }

        // POST: CarrinhoItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CodigoCarrinhoItem,CodigoCarrinho,CodigoServico,CodigoTerapeuta,TipoDeMassagem,QtdePeriodos,Preco")] CarrinhoItem carrinhoItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carrinhoItem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoServico = new SelectList(db.ServicoModels, "CodigoServico", "Nome", carrinhoItem.CodigoServico);
            ViewBag.CodigoTerapeuta = new SelectList(db.TerapeutaModels, "CodigoTerapeuta", "Nome", carrinhoItem.CodigoTerapeuta);
            return View(carrinhoItem);
        }

        // GET: CarrinhoItem/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarrinhoItem carrinhoItem = await db.CarrinhoItemModels.FindAsync(id);
            if (carrinhoItem == null)
            {
                return HttpNotFound();
            }
            return View(carrinhoItem);
        }

        // POST: CarrinhoItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            CarrinhoItem carrinhoItem = await db.CarrinhoItemModels.FindAsync(id);
            db.CarrinhoItemModels.Remove(carrinhoItem);
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
