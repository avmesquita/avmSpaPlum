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
    public class AgendamentoItemServicoController : Controller
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        // GET: AgendamentoItemServico
        public async Task<ActionResult> Index()
        {
            var agendamentoItemServicoModels = db.AgendamentoItemServicoModels.Include(a => a.Agendamento).Include(a => a.Terapeuta).Include(a => a.Servico);
            return View(await agendamentoItemServicoModels.ToListAsync());
        }

        // GET: AgendamentoItemServico/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgendamentoItemServico agendamentoItemServico = await db.AgendamentoItemServicoModels.FindAsync(id);
            if (agendamentoItemServico == null)
            {
                return HttpNotFound();
            }
            return View(agendamentoItemServico);
        }

        // GET: AgendamentoItemServico/Create
        public ActionResult Create()
        {
            ViewBag.CodigoAgendamento = new SelectList(db.AgendamentoModels, "CodigoAgendamento", "NomeCliente");
            return View();
        }

        // POST: AgendamentoItemServico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodigoAgendamentoItemServico,CodigoAgendamento,CodigoServico,Nome,Valor,ValorComissaoEmpresa,ValorComissaoTerapeuta,EmPromocao,TempoAproximadoDoServico")] AgendamentoItemServico agendamentoItemServico)
        {
            if (ModelState.IsValid)
            {
                db.AgendamentoItemServicoModels.Add(agendamentoItemServico);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoAgendamento = new SelectList(db.AgendamentoModels, "CodigoAgendamento", "NomeCliente", agendamentoItemServico.CodigoAgendamento);
            return View(agendamentoItemServico);
        }

        // GET: AgendamentoItemServico/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgendamentoItemServico agendamentoItemServico = await db.AgendamentoItemServicoModels.FindAsync(id);
            if (agendamentoItemServico == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodigoAgendamento = new SelectList(db.AgendamentoModels, "CodigoAgendamento", "NomeCliente", agendamentoItemServico.CodigoAgendamento);
            return View(agendamentoItemServico);
        }

        // POST: AgendamentoItemServico/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CodigoAgendamentoItemServico,CodigoAgendamento,CodigoServico,Nome,Valor,ValorComissaoEmpresa,ValorComissaoTerapeuta,EmPromocao,TempoAproximadoDoServico")] AgendamentoItemServico agendamentoItemServico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agendamentoItemServico).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoAgendamento = new SelectList(db.AgendamentoModels, "CodigoAgendamento", "NomeCliente", agendamentoItemServico.CodigoAgendamento);
            return View(agendamentoItemServico);
        }

        // GET: AgendamentoItemServico/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgendamentoItemServico agendamentoItemServico = await db.AgendamentoItemServicoModels.FindAsync(id);
            if (agendamentoItemServico == null)
            {
                return HttpNotFound();
            }
            return View(agendamentoItemServico);
        }

        // POST: AgendamentoItemServico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AgendamentoItemServico agendamentoItemServico = await db.AgendamentoItemServicoModels.FindAsync(id);
            db.AgendamentoItemServicoModels.Remove(agendamentoItemServico);
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
