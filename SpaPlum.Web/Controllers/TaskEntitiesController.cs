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
    public class TaskEntitiesController : SpaPlumBaseController
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        // GET: TaskEntities
        public async Task<ActionResult> Index()
        {
            return View(await db.TaskEntityModels.ToListAsync());
        }

        // GET: TaskEntities/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskEntity taskEntity = await db.TaskEntityModels.FindAsync(id);
            if (taskEntity == null)
            {
                return HttpNotFound();
            }
            return View(taskEntity);
        }

        // GET: TaskEntities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskEntities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Codigo,Nome,Categoria,Date,DataCadastro")] TaskEntity taskEntity)
        {
            if (ModelState.IsValid)
            {
                db.TaskEntityModels.Add(taskEntity);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(taskEntity);
        }

        // GET: TaskEntities/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskEntity taskEntity = await db.TaskEntityModels.FindAsync(id);
            if (taskEntity == null)
            {
                return HttpNotFound();
            }
            return View(taskEntity);
        }

        // POST: TaskEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Codigo,Nome,Categoria,Date,DataCadastro")] TaskEntity taskEntity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskEntity).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(taskEntity);
        }

        // GET: TaskEntities/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskEntity taskEntity = await db.TaskEntityModels.FindAsync(id);
            if (taskEntity == null)
            {
                return HttpNotFound();
            }
            return View(taskEntity);
        }

        // POST: TaskEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TaskEntity taskEntity = await db.TaskEntityModels.FindAsync(id);
            db.TaskEntityModels.Remove(taskEntity);
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
