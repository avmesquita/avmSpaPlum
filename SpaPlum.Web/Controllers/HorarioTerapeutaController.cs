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

namespace SpaPlum.Web.Controllers
{
    public class HorarioTerapeutaController : Controller
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        // GET: HorarioTerapeuta
		[Authorize]
        public ActionResult Index()
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			ViewBag.ListaFilial = new SelectList(db.FilialModels.Where(x => x.Ativo == true).OrderBy(x => x.Nome), "CodigoFilial", "Nome");
            ViewBag.ListaTerapeuta = new SelectList(db.TerapeutaModels.Where(x => x.Ativa == true).OrderBy(x => x.Nome), "CodigoTerapeuta", "Nome");

            //var horarioTerapeutas = db.HorarioTerapeutaModels.Include(h => h.Terapeuta);
            //return View(await horarioTerapeutas.ToListAsync());
            return View();
        }

        [Authorize]
        //[HttpPost]        
        public async Task<ActionResult> Lista(int CodigoTerapeuta)
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			ViewBag.CodigoTerapeuta = CodigoTerapeuta;

            var horarioTerapeutas = db.HorarioTerapeutaModels.Where(x => x.CodigoTerapeuta == CodigoTerapeuta).OrderBy(x => x.CodigoDiaDaSemana).ThenBy(x => x.HorarioInicio).Include(h => h.Terapeuta);
            return PartialView(await horarioTerapeutas.ToListAsync());
        }

        public bool TerapeutaIsDisponivel(int CodigoTerapeuta, DateTime horario)
        {
            var horarioTerapeutas = db.HorarioTerapeutaModels
                                      .Where(x => x.CodigoTerapeuta == CodigoTerapeuta 
                                           && new DateTime(horario.Year,horario.Month,horario.Day,Convert.ToInt16(x.HorarioInicio.Substring(0,2)),Convert.ToInt16(x.HorarioInicio.Substring(4,2)),0) >= horario 
                                           && new DateTime(horario.Year, horario.Month, horario.Day, Convert.ToInt16(x.HorarioFim.Substring(0, 2)), Convert.ToInt16(x.HorarioFim.Substring(4, 2)), 0) <= horario)
                                      .Include(t => t.Terapeuta);

            return horarioTerapeutas.Count() > 0;
        }



        // GET: HorarioTerapeuta/Details/5
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

			HorarioTerapeuta horarioTerapeuta = await db.HorarioTerapeutaModels.FindAsync(id);
            if (horarioTerapeuta == null)
            {
                return HttpNotFound();
            }
            return View(horarioTerapeuta);
        }

        // GET: HorarioTerapeuta/Create
		[Authorize]
        public ActionResult Create(int CodigoTerapeuta)
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			ViewBag.CodigoTerapeuta = CodigoTerapeuta;

            var nome = db.TerapeutaModels.Where(x => x.CodigoTerapeuta == CodigoTerapeuta).FirstOrDefault().Nome;
            ViewBag.NomeTerapeuta = nome != null ? nome : "";

            //ViewBag.ListaDiaDaSemana = new ListaDiaDaSemana().DiaDaSemanaSelectList;
            ViewBag.ListaDiaDaSemana = new ListaDiaDaSemana().DiaDaSemanaListItem;
            return PartialView();
        }

        // POST: HorarioTerapeuta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodigoTerapeuta,chkDomingo,txtDomingoHoraEntrada,txtDomingoHoraSaida,chkSegunda,txtSegundaHoraEntrada,txtSegundaHoraSaida,chkTerca,txtTercaHoraEntrada,txtTercaHoraSaida,chkQuarta,txtQuartaHoraEntrada,txtQuartaHoraSaida,chkQuinta,txtQuintaHoraEntrada,txtQuintaHoraSaida,chkSexta,txtSextaHoraEntrada,txtSextaHoraSaida,chkSabado,txtSabadoHoraEntrada,txtSabadoHoraSaida")] HorarioTerapeuta horarioTerapeuta)
        {
            if (ModelState.IsValid)
            {
                if (horarioTerapeuta.chkDomingo)
                {
                    var newHorarioTerapeuta = new HorarioTerapeuta();
                    newHorarioTerapeuta.CodigoDiaDaSemana = 0;
                    newHorarioTerapeuta.CodigoTerapeuta = horarioTerapeuta.CodigoTerapeuta;
                    newHorarioTerapeuta.HorarioInicio = horarioTerapeuta.txtDomingoHoraEntrada;
                    newHorarioTerapeuta.HorarioFim = horarioTerapeuta.txtDomingoHoraSaida;
                    db.HorarioTerapeutaModels.Add(newHorarioTerapeuta);
                }
                if (horarioTerapeuta.chkSegunda)
                {
                    var newHorarioTerapeuta = new HorarioTerapeuta();
                    newHorarioTerapeuta.CodigoDiaDaSemana = 1;
                    newHorarioTerapeuta.CodigoTerapeuta = horarioTerapeuta.CodigoTerapeuta;
                    newHorarioTerapeuta.HorarioInicio = horarioTerapeuta.txtSegundaHoraEntrada;
                    newHorarioTerapeuta.HorarioFim = horarioTerapeuta.txtSegundaHoraSaida;
                    db.HorarioTerapeutaModels.Add(newHorarioTerapeuta);
                }
                if (horarioTerapeuta.chkTerca)
                {
                    var newHorarioTerapeuta = new HorarioTerapeuta();
                    newHorarioTerapeuta.CodigoDiaDaSemana = 2;
                    newHorarioTerapeuta.CodigoTerapeuta = horarioTerapeuta.CodigoTerapeuta;
                    newHorarioTerapeuta.HorarioInicio = horarioTerapeuta.txtTercaHoraEntrada;
                    newHorarioTerapeuta.HorarioFim = horarioTerapeuta.txtTercaHoraSaida;
                    db.HorarioTerapeutaModels.Add(newHorarioTerapeuta);
                }
                if (horarioTerapeuta.chkQuarta)
                {
                    var newHorarioTerapeuta = new HorarioTerapeuta();
                    newHorarioTerapeuta.CodigoDiaDaSemana = 3;
                    newHorarioTerapeuta.CodigoTerapeuta = horarioTerapeuta.CodigoTerapeuta;
                    newHorarioTerapeuta.HorarioInicio = horarioTerapeuta.txtQuartaHoraEntrada;
                    newHorarioTerapeuta.HorarioFim = horarioTerapeuta.txtQuartaHoraSaida;
                    db.HorarioTerapeutaModels.Add(newHorarioTerapeuta);
                }
                if (horarioTerapeuta.chkQuinta)
                {
                    var newHorarioTerapeuta = new HorarioTerapeuta();
                    newHorarioTerapeuta.CodigoDiaDaSemana = 4;
                    newHorarioTerapeuta.CodigoTerapeuta = horarioTerapeuta.CodigoTerapeuta;
                    newHorarioTerapeuta.HorarioInicio = horarioTerapeuta.txtQuintaHoraEntrada;
                    newHorarioTerapeuta.HorarioFim = horarioTerapeuta.txtQuintaHoraSaida;
                    db.HorarioTerapeutaModels.Add(newHorarioTerapeuta);
                }
                if (horarioTerapeuta.chkSexta)
                {
                    var newHorarioTerapeuta = new HorarioTerapeuta();
                    newHorarioTerapeuta.CodigoDiaDaSemana = 5;
                    newHorarioTerapeuta.CodigoTerapeuta = horarioTerapeuta.CodigoTerapeuta;
                    newHorarioTerapeuta.HorarioInicio = horarioTerapeuta.txtSextaHoraEntrada;
                    newHorarioTerapeuta.HorarioFim = horarioTerapeuta.txtSextaHoraSaida;
                    db.HorarioTerapeutaModels.Add(newHorarioTerapeuta);
                }
                if (horarioTerapeuta.chkSabado)
                {
                    var newHorarioTerapeuta = new HorarioTerapeuta();
                    newHorarioTerapeuta.CodigoDiaDaSemana = 6;
                    newHorarioTerapeuta.CodigoTerapeuta = horarioTerapeuta.CodigoTerapeuta;
                    newHorarioTerapeuta.HorarioInicio = horarioTerapeuta.txtSabadoHoraEntrada;
                    newHorarioTerapeuta.HorarioFim = horarioTerapeuta.txtSabadoHoraSaida;
                    db.HorarioTerapeutaModels.Add(newHorarioTerapeuta);
                }
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoTerapeuta = new SelectList(db.HorarioTerapeutaModels, "CodigoTerapeuta", "Nome", horarioTerapeuta.CodigoTerapeuta);
            ViewBag.CodigoDiaDaSemana = new ListaDiaDaSemana().DiaDaSemanaSelectList;

            return View(horarioTerapeuta);
        }

        // GET: HorarioTerapeuta/Edit/5
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

			HorarioTerapeuta horarioTerapeuta = await db.HorarioTerapeutaModels.FindAsync(id);
            if (horarioTerapeuta == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodigoTerapeuta = new SelectList(db.HorarioTerapeutaModels, "CodigoTerapeuta", "Nome", horarioTerapeuta.CodigoTerapeuta);
            ViewBag.ListaDiaDaSemana = new ListaDiaDaSemana().DiaDaSemanaSelectList;

            return View(horarioTerapeuta);
        }

        // POST: HorarioTerapeuta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CodigoHorarioTerapeuta,CodigoTerapeuta,CodigoDiaDaSemana,HorarioInicio,HorarioFim")] HorarioTerapeuta horarioTerapeuta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(horarioTerapeuta).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoTerapeuta = new SelectList(db.HorarioTerapeutaModels, "CodigoTerapeuta", "Nome", horarioTerapeuta.CodigoTerapeuta);
            ViewBag.CodigoDiaDaSemana = new ListaDiaDaSemana().DiaDaSemanaSelectList;

            return View(horarioTerapeuta);
        }

        // GET: HorarioTerapeuta/Delete/5
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

			HorarioTerapeuta horarioTerapeuta = await db.HorarioTerapeutaModels.Include(s => s.Terapeuta).AsNoTracking().Where(x => x.CodigoHorarioTerapeuta == id).FirstOrDefaultAsync();
            if (horarioTerapeuta == null)
            {
                return HttpNotFound();
            }
            return View(horarioTerapeuta);
        }

        // POST: HorarioTerapeuta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HorarioTerapeuta horarioTerapeuta = await db.HorarioTerapeutaModels.FindAsync(id);
            db.HorarioTerapeutaModels.Remove(horarioTerapeuta);
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
