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
using System.Data.Entity.Infrastructure;
using PagedList;

namespace SpaPlum.Web.Controllers
{
    public class ServicoController : SpaPlumBaseController
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        /*
        // GET: Servico
        [Authorize]
        public async Task<ActionResult> Index()
        {            
            return View(await db.ServicoModels.OrderBy(x => x.CodigoFilial).ThenBy(x => x.Nome).ToListAsync());
        }
        */

        [Authorize]
        public ActionResult Index(int? page)
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			var servicoModels = db.ServicoModels.Include(t => t.Filial)
                                                .OrderBy(x => x.Filial.Nome)
                                                .ThenBy(a => a.Nome)
                                                .ToPagedList(page ?? 1, 10);
            return View(servicoModels);
        }


        // GET: Servico/Details/5
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

			Servico servico = await db.ServicoModels.FindAsync(id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }

        // GET: Servico/Create
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

        // POST: Servico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodigoServico,Nome,Valor,ValorComissaoEmpresa,ValorComissaoTerapeuta,EmPromocao,ValorPromocao,ValorPromocaoComissaoEmpresa,ValorPromocaoComissaoTerapeuta,CodigoFilial,TempoAproximadoDoServico,Ativo")] Servico servico)
        {
            if (ModelState.IsValid)
            {
                servico.Filial = db.FilialModels.Find(servico.CodigoFilial);

                db.ServicoModels.Add(servico);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ListaFilial = new SelectList(db.FilialModels, "CodigoFilial", "Nome");

            return View(servico);
        }

        // GET: Servico/Edit/5
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

			Servico servico = await db.ServicoModels.FindAsync(id);
            if (servico == null)
            {
                return HttpNotFound();
            }

            ViewBag.ListaFilial = new SelectList(db.FilialModels, "CodigoFilial", "Nome", servico.CodigoFilial);

            return View(servico);


        }

        // POST: Servico/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CodigoServico,Nome,Valor,ValorComissaoEmpresa,ValorComissaoTerapeuta,EmPromocao,ValorPromocao,ValorPromocaoComissaoEmpresa,ValorPromocaoComissaoTerapeuta,CodigoFilial,TempoAproximadoDoServico,Ativo")] Servico servico)
        {
            if (ModelState.IsValid)
            {
                servico.Filial = db.FilialModels.Find(servico.CodigoFilial);

                db.Entry(servico).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            ViewBag.ListaFilial = new SelectList(db.FilialModels, "CodigoFilial", "Nome", servico.CodigoServico);
            return View(servico);
        }

        // GET: Servico/Delete/5
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

			Servico servico = await db.ServicoModels.FindAsync(id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }

        // POST: Servico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Servico servico = await db.ServicoModels.FindAsync(id);
            db.ServicoModels.Remove(servico);
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
