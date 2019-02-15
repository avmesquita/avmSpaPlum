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
using SpaPlum.Web.Models;

namespace SpaPlum.Web.Controllers
{
    public class ClienteController : Controller
    {
        private AgendamentoContexto db = new AgendamentoContexto();

		// GET: Cliente
		[Authorize]
		public ActionResult Index(int? page)
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			var clientes = db.ClienteModels.OrderBy(t => t.Nome).ToPagedList(page ?? 1, 10);

            return View(clientes);
        }

		// GET: Cliente/Details/5
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

			Cliente cliente = await db.ClienteModels.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Cliente/Create
		[Authorize]
        public ActionResult Create()
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			return View();
        }

        // POST: Cliente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodigoCliente,Descricao,Ativo,Email,Telefone,DataCadastro,TokenVIP,PontosVIP,Senha,ConfirmarSenha")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                if (cliente.Senha.Equals(cliente.ConfirmarSenha))
                {
                    db.ClienteModels.Add(cliente);
                    await db.SaveChangesAsync();

                    // Cadastra o cliente como usuário no sistema
                    RegisterViewModel registro = new RegisterViewModel();
                    registro.Nome = cliente.Nome;
                    registro.Email = cliente.Email;
                    registro.Password = cliente.Senha;
                    registro.ConfirmPassword = cliente.ConfirmarSenha;
                    registro.Sobrenome = cliente.Nome.Split(' ').Count() > 1 ? cliente.Nome.Split(' ')[1] : "";
                    registro.CodigoCliente = cliente.CodigoCliente;
                    registro.CodigoPerfil = 1;
                    new AccountController().SimpleRegisterNoSignin(registro);
                }
                else
                {
                    return View(cliente);
                }

                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Cliente/Edit/5
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

			Cliente cliente = await db.ClienteModels.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CodigoCliente,Descricao,Ativo")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Cliente/Delete/5
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

				Cliente cliente = await db.ClienteModels.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cliente cliente = await db.ClienteModels.FindAsync(id);
            db.ClienteModels.Remove(cliente);
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
