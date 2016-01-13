using ManahostManager.App_Start;
using ManahostManager.Domain.DAL;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Domain.Tools;
using ManahostManager.LogTools;
using ManahostManager.Model.DTO.Account;
using ManahostManager.Model.Factory;
using ManahostManager.Services;
using ManahostManager.Utils;
using ManahostManager.Utils.Attributs;
using ManahostManager.Utils.ExternalLoginTools;
using ManahostManager.Utils.Results;
using ManahostManager.Validation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;

namespace ManahostManager.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private ClientUserManager Manager { get; set; }

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IPhoneRepository _repoPhone;

        [Dependency]
        protected HomeService _HomeService { get; set; }

        public AccountController(IPhoneRepository repo, ClientUserManager manager)
        {
            _repoPhone = repo;
            Manager = manager;
        }

        /// <summary>
        /// Has Captcha By Email
        /// </summary>
        /// <param name="email">L'email de l'utilisateur</param>
        /// <remarks>permet de savoir si pour un utilisateur donnée si il doit spécifié un captcha ou pas,<br/> à utiliser de manière dynamique lorsque une adresse email est valide</remarks>
        /// <returns>
        /// {
        ///     "captcha": Boolean
        /// }
        /// </returns>
        /// <response code="404">l'utilisateur n'existe pas</response>
        [HttpGet]
        [Route("Captcha/{email}")]
        public async Task<IHttpActionResult> IsCaptchaNeeded(string email)
        {
            var attempt = Convert.ToInt32(ConfigurationManager.AppSettings[GenericNames.CAPTCHA_FAILED_COUNT]);
            var client = await Manager.FindByEmailAsync(email);
            if (client == null)
                return NotFound();
            return Ok(new
            {
                captcha = client.AccessFailedCount > attempt ? true : false
            });
        }

        /// <summary>
        /// Get information account
        /// </summary>
        /// <remarks>Recupere les informations utilisateur</remarks>
        /// <returns>le model d'exposion de l'account</returns>
        [HttpGet]
        [ManahostAuthorize]
        [Route("")]
        [ResponseType(typeof(ExposeAccountModel))]
        public async Task<IHttpActionResult> GetAccount()
        {
            int idUser = User.Identity.GetUserId<int>();
            Client ClientSelected = await Manager.FindUserByIdWithPhoneNumberAsync(idUser);
            HomeDTO DefaultHome = null;
            try
            {
                _HomeService.SetModelState(ModelState);
                DefaultHome = _HomeService.DoGetDefaultHome(ClientSelected);
            }
            catch (ManahostValidationException)
            {

            }
            return Ok(Factory.Create(ClientSelected, await Manager.GetClaimsAsync(idUser), DefaultHome));
        }

        /// <summary>
        /// Modification de l'account
        /// </summary>
        /// <param name="model">Put Account Model</param>
        /// <returns></returns>
        /// <remarks>Modification des informations du client courrant</remarks>
        /// <response code="400">Une erreur est survenu, look ModelState</response>
        [HttpPut]
        [ManahostAuthorize]
        [Route("")]
        [NullValidation]
        public async Task<IHttpActionResult> PutAccount(PutAccountModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            int idUser = User.Identity.GetUserId<int>();
            var client = await Manager.FindByIdAsync(idUser);

            client = Factory.Create(client, model);
            var result = await Manager.UpdateAsync(client);
            if (result.Succeeded)
                return Ok();
            ModelState.AddModelError("Client", GenericError.INVALID_GIVEN_PARAMETER);
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Création d'un nouveau client
        /// </summary>
        /// <remarks>Permet la création d'un nouveau client au sein de manahost<br/>
        /// Le compte est désactivé par defaut, il recoit un mail de confirmation<br/><br/>
        /// Password rules :<br/>
        /// RequiredLength = 6,<br/>
        /// RequireNonLetterOrDigit = true,<br/>
        /// RequireDigit = true,<br/>
        /// RequireLowercase = true,<br/>
        /// RequireUppercase = true,
        /// </remarks>
        /// <param name="client"></param>
        /// <returns></returns>
        /// <response code="400">See ModelState</response>
        [NullValidation]
        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> PostNewAccount(CreateAccountModel client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newClient = Factory.Create(client);
            if (await Manager.FindByEmailAsync(client.Email) != null)
            {
                ModelState.AddModelError("Email", GenericError.ALREADY_EXISTS);
                return BadRequest(ModelState);
            }
            if (!(await Manager.PasswordValidator.ValidateAsync(client.Password)).Succeeded)
            {
                ModelState.AddModelError("Password", GenericError.INVALID_GIVEN_PARAMETER);
                return BadRequest(ModelState);
            }
            var result = await Manager.CreateAsync(newClient, client.Password);
            if (result.Succeeded)
            {
                var resultGroup = await Manager.AddToRoleAsync(newClient.Id, GenericNames.DISABLED);
                if (!resultGroup.Succeeded)
                {
                    if (Log.ExceptionLogger.IsErrorEnabled)
                    {
                        foreach (var item in resultGroup.Errors)
                            Log.ExceptionLogger.Error(string.Format("CREATE ACCOUNT GROUP INSERT ERROR : {0}", item));
                    }
                    throw new Exception("CREATE ACCOUNT GROUP INSERT ERROR END");
                }
                string code = await Manager.GenerateEmailConfirmationTokenAsync(newClient.Id);

                //TODO I18N
                var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", newClient.Id, code)));
                var CallbackURLConfirmEmail = new Uri(string.Format("{0}account-activation/{1}", WebApiApplication.MANAGER_URL, token));
                //await Manager.SendEmailAsync(newClient.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + CallbackURLConfirmEmail + "\">here</a>");
                await generateTemplateEmailConfirmation(CallbackURLConfirmEmail, newClient);
                // END TODO

                return Ok();
            }
            ModelState.AddModelError("Client", GenericError.INVALID_GIVEN_PARAMETER);
            return BadRequest(ModelState);
        }

        private async Task generateTemplateEmailConfirmation(Uri url, Client newClient)
        {
            var templateFolderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "CreateAccount");
            var templateFilePath = System.IO.Path.Combine(templateFolderPath, "EmailConfirmation.cshtml");
            var templateService = new TemplateService();//TODO CHANGE FOR RazorEngineService
            var templateString = templateService.Parse(File.ReadAllText(templateFilePath), new { Link = url, FirstName = newClient.FirstName, LastName = newClient.LastName, Email = newClient.Email }, null, null);
            await Manager.SendEmailAsync(newClient.Id, "Confirmation Email", templateString);
        }

        /// <summary>
        /// Change Password
        /// </summary>
        /// <remarks>Permet le changement de mot de passe à partir de son mot de passe</remarks>
        /// <param name="model">Account Change Password Model</param>
        /// <returns></returns>
        /// <Response code="400">See ModelState</Response>
        [NullValidation]
        [ManahostAuthorize]
        [Route("Password")]
        [HttpPost]
        public async Task<IHttpActionResult> PostChangePassword(ChangePasswordAccountModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var client = await Manager.FindByIdAsync(User.Identity.GetUserId<int>());
            if (!await Manager.CheckPasswordAsync(client, model.CurrentPassword))
            {
                ModelState.AddModelError("CurrentPassword", GenericError.INVALID_GIVEN_PARAMETER);
                return BadRequest(ModelState);
            }
            var result = await Manager.ChangePasswordAsync(client.Id, model.CurrentPassword, model.Password);
            if (result.Succeeded)
                return Ok();
            ModelState.AddModelError("Password", GenericError.INVALID_GIVEN_PARAMETER);
            return BadRequest();
        }

        /// <summary>
        /// Confirmation email
        /// </summary>
        /// <remarks>Permet la confirmation de son email à partir d'un code.<br />
        /// Il change de groupe ce qui permet l'authentification</remarks>
        /// <param name="code"></param>
        /// <returns></returns>
        /// <response code="500">See ModelState</response>
        [HttpGet]
        [Route("Email/{code}", Name = "ConfirmEmailRoute")]
        public async Task<IHttpActionResult> GetConfirmEmail(string code)
        {
            if (String.IsNullOrEmpty(code))
            {
                ModelState.AddModelError("ConfirmEmail", GenericError.INVALID_GIVEN_PARAMETER);
                return BadRequest(ModelState);
            }
            try
            {
                string IdToken = Encoding.UTF8.GetString(Convert.FromBase64String(code));
                string[] IdAndToken = IdToken.Split(':');
                if (IdAndToken.Count() != 2)
                    throw new FormatException();
                if (await Manager.FindByIdAsync(int.Parse(IdAndToken[0])) == null)
                {
                    ModelState.AddModelError("id", GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
                    return BadRequest(ModelState);
                }
                IdentityResult result = await Manager.ConfirmEmailAsync(int.Parse(IdAndToken[0]), IdAndToken[1]);
                if (result.Succeeded)
                {
                    await Manager.RemoveFromRoleAsync(int.Parse(IdAndToken[0]), GenericNames.DISABLED);
                    return Ok();
                }
            }
            catch (FormatException)
            {
                ModelState.AddModelError("ConfirmEmail", GenericError.INVALID_GIVEN_PARAMETER);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Post Phone
        /// </summary>
        /// <remarks>Permet l'ajout d'un nouveau numero de téléphone ou modification si principal<br />
        /// Si le principal_phone exists un error badRequest est levé</remarks>
        /// <param name="phone">Phone Model</param>
        /// <returns></returns>
        /// <response code="400">See ModelState</response>
        [HttpPost]
        [NullValidation]
        [Route("Phone")]
        [ManahostAuthorize]
        [ResponseType(typeof(PhoneNumber))]
        public async Task<IHttpActionResult> PostNewPhone(PhoneModel phone)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int idClient = User.Identity.GetUserId<int>();
            var client = await Manager.FindUserByIdWithPhoneNumberAsync(idClient);

            var NewPhoneAdd = new PhoneNumber()
            {
                Phone = phone.Phone,
                PhoneType = phone.Type
            };
            if (phone.IsPrimary)
            {
                if (client.PrincipalPhone != null)
                {
                    ModelState.AddModelError("principal_phone", GenericError.ALREADY_EXISTS);
                    return BadRequest(ModelState);
                }
                NewPhoneAdd = _repoPhone.Add(NewPhoneAdd);
                client.PrincipalPhone = NewPhoneAdd;
                await Manager.UpdateAsync(client);
            }
            else
            {
                NewPhoneAdd = _repoPhone.Add(NewPhoneAdd);
                client.SecondaryPhone.Add(NewPhoneAdd);
                await Manager.UpdateAsync(client);
            }
            return Ok(NewPhoneAdd);
        }

        /// <summary>
        /// Put phone
        /// </summary>
        /// <remarks>Permet la modification d'un numéro de téléphone</remarks>
        /// <param name="id">Id du phone</param>
        /// <param name="model">PhoneModel</param>
        /// <returns></returns>
        /// <response code="400">See ModelState</response>
        [HttpPut]
        [NullValidation]
        [Route("Phone/{id:int}")]
        [ManahostAuthorize]
        [ResponseType(typeof(PhoneNumber))]
        public async Task<IHttpActionResult> PutPhone(int id, PhoneModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int idClient = User.Identity.GetUserId<int>();
            var client = await Manager.FindUserByIdWithPhoneNumberAsync(idClient);
            var phoneNumber = _repoPhone.GetUniq(x => x.Id == id);

            int countSecondary = client.SecondaryPhone.Where(x => x.Id == id).Count();

            if ((client.PrincipalPhone != null && client.PrincipalPhone.Id != id) && countSecondary == 0)
            {
                ModelState.AddModelError("Id", GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
                return BadRequest(ModelState);
            }
            if (model.IsPrimary && client.PrincipalPhone.Id != phoneNumber.Id && countSecondary == 1)
            {
                if (client.PrincipalPhone != null)
                {
                    ModelState.AddModelError("principal_phone", GenericError.ALREADY_EXISTS);
                    return BadRequest(ModelState);
                }
                client.SecondaryPhone.Remove(phoneNumber);
                client.PrincipalPhone = phoneNumber;
                await Manager.UpdateAsync(client);
            }
            else if (!model.IsPrimary && client.PrincipalPhone.Id == phoneNumber.Id && countSecondary == 0)
            {
                client.PrincipalPhone = null;
                client.SecondaryPhone.Add(phoneNumber);
                await Manager.UpdateAsync(client);
            }
            phoneNumber.Phone = model.Phone;
            phoneNumber.PhoneType = model.Type;
            _repoPhone.Update(phoneNumber);
            await _repoPhone.SaveAsync();
            return Ok(phoneNumber);
        }

        /// <summary>
        /// Delete phone
        /// </summary>
        /// <remarks>Supression d'un phone par l'id, renvoie FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST si il n'est pas contenu dans l'user </remarks>
        /// <param name="id">Id du phone</param>
        /// <returns></returns>
        /// <response code="400">See ModelState</response>
        [HttpDelete]
        [Route("Phone/{id:int}")]
        [ManahostAuthorize]
        public async Task<IHttpActionResult> DeletePhoneNumber(int id)
        {
            int idClient = User.Identity.GetUserId<int>();
            var client = await Manager.FindUserByIdWithPhoneNumberAsync(idClient);
            var phoneNumber = _repoPhone.GetUniq(x => x.Id == id);

            int countSecondary = client.SecondaryPhone.Where(x => x.Id == id).Count();

            if ((client.PrincipalPhone != null && client.PrincipalPhone.Id != id) && countSecondary == 0)
            {
                ModelState.AddModelError("Id", GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
                return BadRequest(ModelState);
            }
            if (client.PrincipalPhone != null && client.PrincipalPhone.Id == id)
            {
                client.PrincipalPhone = null;
                await Manager.UpdateAsync(client);
            }
            else
            {
                client.SecondaryPhone.Remove(phoneNumber);
                await Manager.UpdateAsync(client);
            }
            _repoPhone.Delete(phoneNumber);
            await _repoPhone.SaveAsync();
            return Ok();
        }

        /// <summary>
        /// Forget Password send email
        /// </summary>
        /// <remarks>Permet l'envoie d'un email pour l'oublie de mot de passe</remarks>
        /// <param name="email">Email user</param>
        /// <returns></returns>
        /// <response code="400">See ModelState</response>
        [HttpGet]
        [Route("Password/Forget/{email}")]
        [NullValidation]
        public async Task<IHttpActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
            {
                ModelState.AddModelError("Email", GenericError.INVALID_GIVEN_PARAMETER);
                return BadRequest(ModelState);
            }
            var mailUnescape = Uri.UnescapeDataString(email);
            if (!MailValid.IsValid(mailUnescape))
            {
                ModelState.AddModelError("Email", GenericError.INVALID_GIVEN_PARAMETER);
                return BadRequest(ModelState);
            }
            var client = await Manager.FindByEmailAsync(mailUnescape);
            if (client == null)
            {
                ModelState.AddModelError("Email", GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
                return BadRequest(ModelState);
            }
            var TokenForgetPassword = await Manager.GeneratePasswordResetTokenAsync(client.Id);
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", client.Id, TokenForgetPassword)));
            var CallbackURLForgetPassword = new Uri(string.Format("{0}forgot-password/{1}", WebApiApplication.MANAGER_URL, token));

            await sendForgetPasswordWithTemplate(CallbackURLForgetPassword, client);
            return Ok();
        }

        private async Task sendForgetPasswordWithTemplate(Uri url, Client client)
        {
            var templateFolderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "ForgotPassword");
            var templateFilePath = System.IO.Path.Combine(templateFolderPath, "ForgotPassword.cshtml");
            var templateService = new TemplateService();//TODO CHANGE FOR RazorEngineService
            var templateString = templateService.Parse(File.ReadAllText(templateFilePath), new { Link = url, FirstName = client.FirstName, LastName = client.LastName, Email = client.Email }, null, null);
            await Manager.SendEmailAsync(client.Id, "Forgot Password", templateString);
        }

        /// <summary>
        /// Forget password confirmation
        /// </summary>
        /// <remarks>permet la confirmation du nouveau mot de passe à partir d'un token</remarks>
        /// <param name="model">Confirm Forget password Model</param>
        /// <returns></returns>
        /// <response code="400">See ModelState</response>
        [HttpPost]
        [Route("Password/Forget")]
        [NullValidation]
        public async Task<IHttpActionResult> ChangePasswordWithTokenInForgetPassword(ConfirmForgetPassword model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                string IdToken = Encoding.UTF8.GetString(Convert.FromBase64String(model.Token));
                string[] IdAndToken = IdToken.Split(':');
                if (IdAndToken.Count() != 2)
                    throw new FormatException();
                if (!(await Manager.PasswordValidator.ValidateAsync(model.Password)).Succeeded)
                {
                    var result = await Manager.ResetPasswordAsync(int.Parse(IdAndToken[0]), IdAndToken[1], model.Password);
                    if (result.Succeeded)
                        return Ok();
                }

                ModelState.AddModelError("Password", GenericError.INVALID_GIVEN_PARAMETER);
            }
            catch (FormatException)
            {
                ModelState.AddModelError("Token", GenericError.INVALID_GIVEN_PARAMETER);
            }
            return BadRequest(ModelState);
        }

        [ManahostAuthorize]
        [HttpGet]
        [Route("Manager")]
        public async Task<IHttpActionResult> GetInitAccountManager()
        {
            var client = await Manager.FindByIdAsync(User.Identity.GetUserId<int>());
            if (client.InitManager == true)
            {
                ModelState.AddModelError("Client", GenericError.ALREADY_INIT);
                return BadRequest(ModelState);
            }
            client.IsManager = true;
            client.InitManager = true;
            await Manager.UpdateAsync(client);
            await Manager.AddToRoleAsync(User.Identity.GetUserId<int>(), GenericNames.MANAGER);
            await Manager.AddToRoleAsync(User.Identity.GetUserId<int>(), GenericNames.REGISTERED_VIP);
            await ManahostInitData.Seed(Request.GetOwinContext().Get<ManahostManagerDAL>(), client);
            return Ok();
        }

        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("External/Provider", Name = "ExternalLogin")]
        [HttpGet]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            string redirectUri = string.Empty;

            if (error != null)
            {
                return BadRequest(Uri.EscapeDataString(error));
            }
            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }
            if (!ValidateClientAndRedirectUri(this.Request, ref redirectUri))
            {
                return BadRequest(ModelState);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            Client user = await Manager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider, externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            redirectUri = string.Format("{0}#external_access_token={1}&provider={2}&haslocalaccount={3}&external_user_name={4}",
                                            redirectUri,
                                            externalLogin.ExternalAccessToken,
                                            externalLogin.LoginProvider,
                                            hasRegistered.ToString(),
                                            externalLogin.UserName);

            return Redirect(redirectUri);
        }

        [NullValidation]
        [AllowAnonymous]
        [HttpPost]
        [Route("External")]
        public async Task<IHttpActionResult> PostRegisterExternel(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            KeyValuePair<string, Func<ITokenInfo>> searchExternalProvider;
            try
            {
                searchExternalProvider = WebApiApplication.TokenInfoList.First(x => x.Key == model.Provider);
            }
            catch (InvalidOperationException)
            {
                ModelState.AddModelError("Provider", GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
                return BadRequest(ModelState);
            }
            var instance = searchExternalProvider.Value.Invoke();
            var parsedToken = await instance.Get(model.ExternalAccessToken);
            if (parsedToken == null)
            {
                ModelState.AddModelError(GenericNames.AUTHENTICATION_EXTERNAL_LOGIN, GenericError.INVALID_GIVEN_PARAMETER);
                return BadRequest(ModelState);
            }
            Client user = await Manager.FindAsync(new UserLoginInfo(model.Provider, parsedToken.user_id));
            if (user != null)
            {
                ModelState.AddModelError("Client", GenericError.ALREADY_EXISTS);
                return BadRequest(ModelState);
            }

            var informationUser = await instance.GetUserInfo(model.ExternalAccessToken);
            var client = Factory.Create(informationUser);
            var resultClient = await Manager.CreateAsync(client);
            if (!resultClient.Succeeded)
            {
                //TODO ERROR
                return BadRequest();
            }
            var ResultLoginClient = await Manager.AddLoginAsync(client.Id, new UserLoginInfo(model.Provider, parsedToken.user_id));
            if (!ResultLoginClient.Succeeded)
            {
                //TODO ERROR
                return BadRequest();
            }
            Service service = null;
            try
            {
                service = await AuthenticationTools.GetServiceActiveAndExists(Request.GetOwinContext().Get<ManahostManagerDAL>(), model.client_id, model.client_secret);
            }
            catch (AuthenticationToolsException e)
            {
                ModelState.AddModelError(e.Key, e.Value);
                return BadRequest(ModelState);
            }
            var ticket = AuthenticationTools.GenerateTicket(OAuthDefaults.AuthenticationType, model.client_id, client);
            var accessTokenExternal = AuthenticationTools.GenerateToken(ticket, client);

            return OkWithHeader(accessTokenExternal, new Dictionary<string, string>() {
                { GenericNames.OWIN_CONTEXT_CORS_HEADER, service.AllowedOrigin }
            });
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("External")]
        public async Task<IHttpActionResult> GetLocalExternalAccessToken(string provider, string externalAccessToken, string client_id, string client_secret = "")
        {
            if (string.IsNullOrWhiteSpace(provider) || string.IsNullOrWhiteSpace(externalAccessToken))
            {
                return BadRequest("Provider or external access token is not sent");
            }
            KeyValuePair<string, Func<ITokenInfo>> searchExternalProvider;
            try
            {
                searchExternalProvider = WebApiApplication.TokenInfoList.First(x => x.Key == provider);
            }
            catch (InvalidOperationException)
            {
                ModelState.AddModelError("Provider", GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
                return BadRequest(ModelState);
            }
            var instance = searchExternalProvider.Value.Invoke();
            var verifiedTokenExternal = await instance.Get(externalAccessToken);
            if (verifiedTokenExternal == null)
            {
                ModelState.AddModelError(GenericNames.AUTHENTICATION_EXTERNAL_LOGIN, GenericError.INVALID_GIVEN_PARAMETER);
                return BadRequest(ModelState);
            }
            Client userClient = await Manager.FindAsync(new UserLoginInfo(provider, verifiedTokenExternal.user_id));
            if (userClient == null)
            {
                ModelState.AddModelError("Client", GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
                return BadRequest(ModelState);
            }
            Service service = null;
            try
            {
                service = await AuthenticationTools.GetServiceActiveAndExists(Request.GetOwinContext().Get<ManahostManagerDAL>(), client_id, client_secret);
            }
            catch (AuthenticationToolsException e)
            {
                ModelState.AddModelError(e.Key, e.Value);
                return BadRequest(ModelState);
            }
            var ticket = AuthenticationTools.GenerateTicket(OAuthDefaults.AuthenticationType, client_id, userClient);
            var accessTokenExternal = AuthenticationTools.GenerateToken(ticket, userClient);

            return OkWithHeader(accessTokenExternal, new Dictionary<string, string>() {
                { GenericNames.OWIN_CONTEXT_CORS_HEADER, service.AllowedOrigin }
            });
        }

        private OkNegotiatedContentResult<T> OkWithHeader<T>(T Item, IDictionary<string, string> headers) where T : class
        {
            var result = new OkNegotiatedContentResult<T>(Item, this);
            foreach (var item in headers)
            {
                result.Request.Headers.Remove(item.Key);
                result.Request.Headers.Add(item.Key, item.Value);
            }
            return result;
        }

        private bool ValidateClientAndRedirectUri(HttpRequestMessage request, ref string redirectUriOutput)
        {
            Uri redirectUri;
            var redirectUriString = GetQueryString(Request, "redirect_uri");

            if (string.IsNullOrWhiteSpace(redirectUriString))
            {
                ModelState.AddModelError("rediret_uri", GenericError.DOES_NOT_MEET_REQUIREMENTS);
                return false;
            }

            bool validUri = Uri.TryCreate(redirectUriString, UriKind.Absolute, out redirectUri);
            if (!validUri)
            {
                ModelState.AddModelError("rediret_uri", GenericError.INVALID_GIVEN_PARAMETER);
                return false;
            }

            var clientId = GetQueryString(Request, "client_id");
            if (string.IsNullOrWhiteSpace(clientId))
            {
                ModelState.AddModelError("client_id", GenericError.INVALID_GIVEN_PARAMETER);
                return false;
            }

            var client = (new ServiceRepository(_repoPhone.RetrieveContext())).GetUniq(x => x.Id == clientId);
            if (client == null)
            {
                ModelState.AddModelError("client_id", GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
                return false;
            }

            if (!string.Equals(client.AllowedOrigin, redirectUri.GetLeftPart(UriPartial.Authority), StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("client_id", GenericError.NOT_AUTHORIZED);
                return false;
            }

            redirectUriOutput = redirectUri.AbsoluteUri;
            return true;
        }

        private string GetQueryString(HttpRequestMessage request, string key)
        {
            var queryStrings = request.GetQueryNameValuePairs();
            if (queryStrings == null) return null;

            var match = queryStrings.FirstOrDefault(keyValue => string.Compare(keyValue.Key, key, true) == 0);

            if (string.IsNullOrEmpty(match.Value)) return null;
            return match.Value;
        }
    }
}