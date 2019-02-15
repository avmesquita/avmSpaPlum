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

namespace SpaPlum.Web.Controllers
{
    public class EmailController : Controller
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        /*
        // GET: Email
        [Authorize]
        public async Task<ActionResult> Index()
        {
            return View(await );
        }
        */

        [Authorize]
        public ActionResult Index(int? page)
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			var emailsModels = db.EmailModels.OrderByDescending(x => x.DataEnvio)
                                             .ToPagedList(page ?? 1, 10);
            return View(emailsModels);
        }



        // GET: Email/Details/5
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

			Email email = await db.EmailModels.FindAsync(id);
            if (email == null)
            {
                return HttpNotFound();
            }
            return View(email);
        }

        // GET: Email/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }        

        // POST: Email/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodigoEmail,DoNome,DoEmail,ParaNome,ParaEmail,Assunto,Mensagem,DataEnvio,Extra,FoiLido,HouveFalha")] Email email)
        {
            if (ModelState.IsValid)
            {
                db.EmailModels.Add(email);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(email);
        }

        // GET: Email/Edit/5
        /*
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Email email = await db.EmailModels.FindAsync(id);
            if (email == null)
            {
                return HttpNotFound();
            }
            return View(email);
        }
        */
        // POST: Email/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CodigoEmail,DoNome,DoEmail,ParaNome,ParaEmail,Assunto,Mensagem,DataEnvio,Extra,FoiLido,HouveFalha")] Email email)
        {
            if (ModelState.IsValid)
            {
                db.Entry(email).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(email);
        }

        // GET: Email/Delete/5
        /*
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Email email = await db.EmailModels.FindAsync(id);
            if (email == null)
            {
                return HttpNotFound();
            }
            return View(email);
        }
        */

        // POST: Email/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            /*
            Email email = await db.EmailModels.FindAsync(id);
            db.EmailModels.Remove(email);
            await db.SaveChangesAsync();
            */
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
