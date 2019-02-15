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
using Newtonsoft.Json;

namespace SpaPlum.Web.Controllers
{
    public class TerapeutaServicosController : Controller
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        /*
        // GET: TerapeutaServicos
        public async Task<ActionResult> Index()
        {
            var terapeutaServicoModels = db.TerapeutaServicoModels.Include(t => t.Servico).Include(t => t.Terapeuta);
            return View(await terapeutaServicoModels.ToListAsync());
        }
        */
		[Authorize]
        public ActionResult Index(int? page)
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			var terapeutaServicoModels = db.TerapeutaServicoModels.Include(t => t.Servico)
                                                                  .Include(t => t.Terapeuta).Include(t => t.Terapeuta.Filial)																  
                                                                  .OrderBy(t => t.Terapeuta.CodigoFilial)
                                                                  .ThenBy(t => t.Terapeuta.Nome)
                                                                  .ToPagedList(page ?? 1, 10);
            return View(terapeutaServicoModels);
        }

        // GET: TerapeutaServicos/Details/5
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

			TerapeutaServico terapeutaServico = await db.TerapeutaServicoModels.Include(t => t.Servico).Include(t => t.Terapeuta).Where(x => x.CodigoTerapeutaServico == id).FirstOrDefaultAsync();
            if (terapeutaServico == null)
            {
                return HttpNotFound();
            }
            return View(terapeutaServico);
        }

        // GET: TerapeutaServicos/Create
		[Authorize]
        public ActionResult Create()
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			var servicos = db.ServicoModels.Where(x => x.Ativo == true).OrderBy(x => x.Nome).ToList();

            List<SelectListItem> servicosSelecao = new List<SelectListItem>();
            foreach (var servico in servicos)
            {
                var item = new SelectListItem
                {
                    Value = servico.CodigoServico.ToString(),
                    Text = servico.Nome
                };
                servicosSelecao.Add(item);
            }

            ViewBag.ListaTerapeuta = new SelectList(db.TerapeutaModels.Where(x => x.CodigoFilial == -1).OrderBy(x => x.Nome), "CodigoTerapeuta", "Nome");
            ViewBag.ListaServico = new SelectList(servicos, "CodigoServico", "Nome");
            ViewBag.ListaFilial = new SelectList(db.FilialModels.OrderBy(x => x.Nome), "CodigoFilial", "Nome");

			//ViewBag.ServicoSelecao = servicosSelecao.OrderBy(x => x.Text);
			//ViewBag.lstServicosMulti = new MultiSelectList(servicosSelecao.OrderBy(x => x.Text), "CodigoServico", "Nome");

			// Make sure the selected list only contains the Ids.
			//var selected = servicos.Select(x => x.);
			// Make sure the ViewBag property is the same name as the Model property.
			//ViewBag.ServicosSelecionados = new MultiSelectList(servicosSelecao, "Value", "Text", selected);


			List<SelectListItem> listaNula = new List<SelectListItem>();

			var model = new TerapeutaServico();
            model.ServicosIds = new List<string>();
            model.ServicosSelecionados = new MultiSelectList(listaNula, "Value", "Text", null);

            return View(model);
        }

        [HttpPost]
        public ActionResult ListServicos()
        {
            var servicos = db.ServicoModels.Where(x => x.CodigoFilial == -1).OrderBy(x => x.Nome).ToList();
            return Json(servicos);
        }

        // POST: TerapeutaServicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodigoTerapeutaServico,CodigoTerapeuta,CodigoServico,ServicosSelecionados,ServicosIds")] TerapeutaServico terapeutaServico)
        {
            if (ModelState.IsValid)
            {
                if (terapeutaServico.ServicosIds.Count > 0)
                {
                    foreach (var item in terapeutaServico.ServicosIds)
                    {
                        var newModel = new TerapeutaServico();
                        newModel.CodigoTerapeuta = terapeutaServico.CodigoTerapeuta;
                        newModel.CodigoServico = int.Parse(item);

                        db.TerapeutaServicoModels.Add(newModel);
                    }
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }

            //ViewBag.ListaTerapeuta = new SelectList(db.TerapeutaModels.OrderBy(x => x.Nome), "CodigoTerapeuta", "Nome");
            //ViewBag.ListaServico = new SelectList(db.ServicoModels.OrderBy(x => x.Nome), "CodigoServico", "Nome");
            ViewBag.ListaFilial = new SelectList(db.FilialModels.OrderBy(x => x.Nome), "CodigoFilial", "Nome");
            return View(terapeutaServico);
        }

        // GET: TerapeutaServicos/Edit/5
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

			TerapeutaServico terapeutaServico = await db.TerapeutaServicoModels.Include(t => t.Servico).Include(t => t.Terapeuta).Where(x => x.CodigoTerapeutaServico == id).FirstOrDefaultAsync();
            if (terapeutaServico == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ListaTerapeuta = new SelectList(db.TerapeutaModels.OrderBy(x => x.Nome), "CodigoTerapeuta", "Nome");
            //ViewBag.ListaServico = new SelectList(db.ServicoModels.OrderBy(x => x.Nome), "CodigoServico", "Nome");
            ViewBag.ListaFilial = new SelectList(db.FilialModels.OrderBy(x => x.Nome), "CodigoFilial", "Nome");

            return View(terapeutaServico);
        }

        // POST: TerapeutaServicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CodigoTerapeutaServico,CodigoTerapeuta,CodigoServico")] TerapeutaServico terapeutaServico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(terapeutaServico).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ListaTerapeuta = new SelectList(db.TerapeutaModels.OrderBy(x => x.Nome), "CodigoTerapeuta", "Nome");
            ViewBag.ListaServico = new SelectList(db.ServicoModels.OrderBy(x => x.Nome), "CodigoServico", "Nome");
            ViewBag.ListaFilial = new SelectList(db.FilialModels.OrderBy(x => x.Nome), "CodigoFilial", "Nome");
            return View(terapeutaServico);
        }

        // GET: TerapeutaServicos/Delete/5
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

			TerapeutaServico terapeutaServico = await db.TerapeutaServicoModels.Include(t => t.Servico).Include(t => t.Terapeuta).Where(x => x.CodigoTerapeutaServico == id).FirstOrDefaultAsync();
            if (terapeutaServico == null)
            {
                return HttpNotFound();
            }
            return View(terapeutaServico);
        }

        // POST: TerapeutaServicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TerapeutaServico terapeutaServico = await db.TerapeutaServicoModels.FindAsync(id);
            db.TerapeutaServicoModels.Remove(terapeutaServico);
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
