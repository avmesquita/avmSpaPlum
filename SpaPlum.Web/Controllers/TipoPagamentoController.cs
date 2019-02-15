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
using SpaPlum.Web.Helpers;
using PagedList;

namespace SpaPlum.Web.Controllers
{
    public class TipoPagamentoController : SpaPlumBaseController
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        /*
        // GET: TipoPagamento
        [Authorize]
        public async Task<ActionResult> Index()
        {
            return View(await db.TipoPagamentoModels.Include(t => t.Filial).ToListAsync());
        }
        */

        [Authorize]
        public ActionResult Index(int? page)
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			var tipopagamentoModels = db.TipoPagamentoModels.Include(t => t.Filial)
                                                            .OrderBy(x => x.Filial.Nome)
                                                            .ThenBy(a => a.Descricao)
                                                            .ToPagedList(page ?? 1, 10);
            return View(tipopagamentoModels);
        }

		[HttpPost]
		public ActionResult ObterTiposPagamentosPorFilial(string codigoFilial)
		{
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			if (codigoFilial == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Int64 idInteiroFilial = 0;
			if (codigoFilial != null)
			{
				Int64.TryParse(codigoFilial, out idInteiroFilial);



				var tipopagamentoModels = db.TipoPagamentoModels.Include(t => t.Filial)
																.OrderBy(x => x.Filial.Nome)
																.ThenBy(a => a.Descricao)
																.ToList();

				var retorno = (from s in tipopagamentoModels//.Include(e => e.Filial)
							   where s.CodigoFilial == idInteiroFilial
							   select new
							   {
								   CodigoServico = s.CodigoTipoPagamento,
								   Descricao = s.Descricao
							   }).ToArray();



				//var retorno = tipopagamentoModels.Where(x => x.CodigoFilial == idInteiroFilial).ToList(); ;

				return Json(retorno);
			}			
			else return Json("");
		}

		// GET: TipoPagamento/Details/5
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

			TipoPagamento tipoPagamento = await db.TipoPagamentoModels.Include(t => t.Filial).Where(x => x.CodigoTipoPagamento == id).FirstOrDefaultAsync();
            if (tipoPagamento == null)
            {
                return HttpNotFound();
            }

            ViewBag.CodigoFilial = new SelectList(db.FilialModels, "CodigoFilial", "Nome", tipoPagamento.CodigoFilial);
            return View(tipoPagamento);
        }

        // GET: TipoPagamento/Create
        [Authorize]
        public ActionResult Create()
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			ViewBag.ListaGatewayPagamento = new ListaGatewayPagamento().GatewayPagamentoListItem;
            ViewBag.ListaFilial = new SelectList(db.FilialModels, "CodigoFilial", "Nome");

            return View();
        }

        // POST: TipoPagamento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodigoTipoPagamento,Descricao,Taxa,Mora,TipoTaxa,TipoMora,Ativo,GatewayAtivo,CodigoGatewayPagamento,CodigoFilial")] TipoPagamento tipoPagamento)
        {
            if (ModelState.IsValid)
            {
                db.TipoPagamentoModels.Add(tipoPagamento);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tipoPagamento);
        }

        // GET: TipoPagamento/Edit/5
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

			TipoPagamento tipoPagamento = await db.TipoPagamentoModels.Include(t => t.Filial).Where(x => x.CodigoTipoPagamento == id).FirstOrDefaultAsync();
            if (tipoPagamento == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CodigoFilial = new SelectList(db.FilialModels, "CodigoFilial", "Nome", tipoPagamento.CodigoFilial);
            ViewBag.ListaFilial = new SelectList(db.FilialModels, "CodigoFilial", "Nome", tipoPagamento.CodigoFilial);
            return View(tipoPagamento);
        }

        // POST: TipoPagamento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CodigoTipoPagamento,Descricao,Taxa,Mora,TipoTaxa,TipoMora,Ativo,GatewayAtivo,CodigoGatewayPagamento,CodigoFilial")] TipoPagamento tipoPagamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoPagamento).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ListaFilial = new SelectList(db.FilialModels, "CodigoFilial", "Nome", tipoPagamento.CodigoFilial);
            return View(tipoPagamento);
        }

        // GET: TipoPagamento/Delete/5
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

			TipoPagamento tipoPagamento = await db.TipoPagamentoModels.Include(t => t.Filial).Where(x => x.CodigoTipoPagamento == id).FirstOrDefaultAsync();
            if (tipoPagamento == null)
            {
                return HttpNotFound();
            }            
            ViewBag.ListaFilial = new SelectList(db.FilialModels, "CodigoFilial", "Nome", tipoPagamento.CodigoFilial);
            return View(tipoPagamento);
        }

        // POST: TipoPagamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TipoPagamento tipoPagamento = await db.TipoPagamentoModels.FindAsync(id);
            db.TipoPagamentoModels.Remove(tipoPagamento);
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
