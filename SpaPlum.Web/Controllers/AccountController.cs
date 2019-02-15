using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SpaPlum.Web.Contexto;
using SpaPlum.Web.Entity;
using SpaPlum.Web.Models;

namespace SpaPlum.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Tentativa de login inválida.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Código inválido.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        //[Authorize]
        public ActionResult Register()
        {
            return View();
        }

		//
		// POST: /Account/Register
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Register(RegisterViewModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					if (!model.Password.Equals(model.ConfirmPassword))
					{
						return View(model);
					}

					Contexto.AgendamentoContexto db = new Contexto.AgendamentoContexto();

					/* Isso aqui é código de inicialização. Não precisa estar aqui né?! */
					/*
					if (db.PerfilAcessoModels.ToList().Count() == 0)
					{
						PerfilAcesso perfilAcessoDev = new PerfilAcesso
						{
							CodigoPerfilAcesso = 0,
							Descricao = "Desenvolvimento",
							Ativo = true
						};
						PerfilAcesso perfilAcessoAdmin = new PerfilAcesso
						{
							CodigoPerfilAcesso = 1,
							Descricao = "Administrador",
							Ativo = true
						};
						PerfilAcesso perfilAcessoCliente = new PerfilAcesso
						{
							CodigoPerfilAcesso = 2,
							Descricao = "Cliente",
							Ativo = true
						};
						PerfilAcesso perfilAcessoProfissional = new PerfilAcesso
						{
							CodigoPerfilAcesso = 3,
							Descricao = "Profissional",
							Ativo = true
						};
						PerfilAcesso perfilAcessoGerente = new PerfilAcesso
						{
							CodigoPerfilAcesso = 4,
							Descricao = "Gerente",
							Ativo = true
						};
						db.PerfilAcessoModels.Add(perfilAcessoDev);
						db.PerfilAcessoModels.Add(perfilAcessoAdmin);
						db.PerfilAcessoModels.Add(perfilAcessoCliente);
						db.PerfilAcessoModels.Add(perfilAcessoProfissional);
						db.PerfilAcessoModels.Add(perfilAcessoGerente);
						db.SaveChanges();
					}
					*/

					// Carrega a lista de e-mails principais FROM ARQUIVO DE CONFIGURACAO
					// Importante para a implantação
					int codigoPerfilSelecionado = 2;
					// 0 - Development | 1 - Administrador | 2 - Cliente | 3 - Profissional (Ponto) | 4 - Gerente
					var mainEmails = new ApplicationContext().MainEmails.Split(',');
					var emailsAdministrador = new ApplicationContext().EmailsFuncaoAdministrador.Split(',');
					var emailsGerente = new ApplicationContext().EmailsFuncaoGerente.Split(',');
					var emailsCliente = new ApplicationContext().EmailsFuncaoCliente.Split(',');
					var emailsFuncionario = new ApplicationContext().EmailsFuncaoFuncionario.Split(',');
					foreach (var item in mainEmails)
					{
						if (model.Email.Equals(item)) { codigoPerfilSelecionado = 0; break; }
					}
					foreach (var item in emailsAdministrador)
					{
						if (model.Email.Equals(item)) { codigoPerfilSelecionado = 1; break; }
					}
					foreach (var item in emailsCliente)
					{
						if (model.Email.Equals(item)) { codigoPerfilSelecionado = 2; break; }
					}
					foreach (var item in emailsFuncionario)
					{
						if (model.Email.Equals(item)) { codigoPerfilSelecionado = 3; break; }
					}
					foreach (var item in emailsGerente)
					{
						if (model.Email.Equals(item)) { codigoPerfilSelecionado = 4; break; }
					}
					// VERIFICA SE EXISTE ALGUM EMAIL DE FUNCIONARIO CADASTRADO E ATRIBUI O CODIGO DO PERFIL
					if (db.TerapeutaModels.Where(x => x.EMail == model.Email).Count() > 0)
					{
						codigoPerfilSelecionado = 3;
					}

					// Cadastra o cliente
					var cliente = new Entity.Cliente();
					cliente.Nome = model.Nome + " " + model.Sobrenome;
					cliente.Email = model.Email;
					cliente.PontosVIP = 0;
					cliente.Ativo = true;
					cliente.Senha = model.Password;
					cliente.ConfirmarSenha = model.ConfirmPassword;
					cliente.CodigoPerfilAcesso = codigoPerfilSelecionado; // 1 - Administrador | 2 - Cliente | 3 - Profissional (Ponto) | 4 - Gerente
					cliente.PerfilAcesso = db.PerfilAcessoModels.Where(x => x.CodigoPerfilAcesso == codigoPerfilSelecionado).FirstOrDefault();
					db.ClienteModels.Add(cliente);
					db.SaveChanges();
					// Pega o código do cliente para vincular no usuário
					model.CodigoCliente = cliente.CodigoCliente;

					var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Nome = model.Nome, Sobrenome = model.Sobrenome, CodigoCliente = cliente.CodigoCliente, CodigoPerfil = codigoPerfilSelecionado, CodigoTerapeuta = model.CodigoTerapeuta };

					var result = await UserManager.CreateAsync(user, model.Password);
					if (result.Succeeded)
					{
						// Loga o cara
						await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

						// For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
						// Send an email with this link
						string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
						var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
						await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

						return RedirectToAction("Index", "Home");
					}
					AddErrors(result);

				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

        public bool SimpleRegister(RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Nome = model.Nome, Sobrenome = model.Sobrenome, CodigoCliente = 0, CodigoPerfil = 2 };
            var result = UserManager.Create(user, model.Password);
            if (result.Succeeded)
            {
                // Loga o cara no sistema
                SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                // Envia o e-mail de confirmação de conta
                string code = UserManager.GenerateEmailConfirmationToken(user.Id);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                UserManager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                //return RedirectToAction("Index", "Home");

                return true;
            }
            AddErrors(result);
            return false;
        }

        public bool SimpleRegisterNoSignin(RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Nome = model.Nome, Sobrenome = model.Sobrenome, CodigoCliente = 0, CodigoPerfil = 2 };
            var result = UserManager.Create(user, model.Password);
            if (result.Succeeded)
            {
                // Envia o e-mail de confirmação de conta
                string code = UserManager.GenerateEmailConfirmationToken(user.Id);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                UserManager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");                
                return true;
            }
            AddErrors(result);
            return false;
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Para reiniciar sua senha, clique <a href=\"" + callbackUrl + "\">aqui</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
		{
			ManageController.ManageMessageId? message = null;
			IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
			if (result.Succeeded)
			{
				message = ManageController.ManageMessageId.RemoveLoginSuccess;
			}
			else
			{
				message = ManageController.ManageMessageId.Error;
			}
			return RedirectToAction("Manage", new { Message = message });
		}


		protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        public string getClaimNome()
        {
            int CodigoCliente = 0;
            string Nome = "";
            string Sobrenome = "";
            int CodigoPerfil = 1;
            this.UserClaims(out CodigoCliente, out Nome, out Sobrenome, out CodigoPerfil);

            return Nome;
        }

        public string getClaimSobrenome()
        {
            int CodigoCliente = 0;
            string Nome = "";
            string Sobrenome = "";
            int CodigoPerfil = 1;
            this.UserClaims(out CodigoCliente, out Nome, out Sobrenome, out CodigoPerfil);

            return Sobrenome;
        }

        public int getClaimCodigoCliente()
        {
            int CodigoCliente = 0;
            string Nome = "";
            string Sobrenome = "";
            int CodigoPerfil = 1;
            this.UserClaims(out CodigoCliente, out Nome, out Sobrenome, out CodigoPerfil);

            return CodigoCliente;
        }

        public int getClaimCodigoPerfil()
        {
            int CodigoCliente = 0;
            string Nome = "";
            string Sobrenome = "";
            int CodigoPerfil = 1;
            this.UserClaims(out CodigoCliente, out Nome, out Sobrenome, out CodigoPerfil);

            return CodigoPerfil;
        }



        private ApplicationUser UserClaims(out int CodigoCliente, out string Nome, out string Sobrenome, out int CodigoPerfil )
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            CodigoCliente = user.CodigoCliente;
            Nome = user.Nome;
            Sobrenome = user.Sobrenome;
            CodigoPerfil = user.CodigoPerfil;

            return user;
        }
        #endregion
    }
}