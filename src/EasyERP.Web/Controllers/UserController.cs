namespace EasyERP.Web.Controllers
{
    using System.Web.Mvc;
    using Doamin.Service.Authentication;
    using Doamin.Service.Users;
    using EasyERP.Web.Framework.Security.Captcha;
    using EasyERP.Web.Models.Users;

    public class UserController : BasePublicController
    {
        private readonly IAuthenticationService authenticationService;

        private readonly CaptchaSettings captchaSettings = new CaptchaSettings();

        private readonly IUserService userService;

        public UserController(IUserService userService, IAuthenticationService authenticationService)
        {
            this.userService = userService;
            this.authenticationService = authenticationService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            var model = new LoginModel();
            model.DisplayCaptcha = captchaSettings.Enabled;
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            //if (captchaSettings.Enabled && !captchaValid)
            //{
            //    ModelState.AddModelError("", "验证码错误");
            //}

            if (ModelState.IsValid)
            {
                var loginResult = userService.ValidateUser(model.Username.Trim(), model.Password);
                switch (loginResult)
                {
                    case UserLoginResults.Successful:
                    {
                        var user = userService.GetUserByName(model.Username);
                        authenticationService.SignIn(user, false);


                        if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                        {
                            // TODO:redirection to page according user typer
                            return RedirectToAction("List", "Product");
                        }

                        return Redirect(returnUrl);
                    }
                    case UserLoginResults.UserNotExist:
                        ModelState.AddModelError("", "用户不存在");
                        break;

                    case UserLoginResults.Deleted:
                        ModelState.AddModelError("", "Deleted");
                        break;

                    case UserLoginResults.NotActive:
                        ModelState.AddModelError("", "未激活");
                        break;

                    default:
                        ModelState.AddModelError("", "密码错误");
                        break;
                }
            }

            // something wrong, display form again
            model.DisplayCaptcha = captchaSettings.Enabled;
            return View(model);
        }

        public ActionResult Logout()
        {
            authenticationService.SignOut();
            return RedirectToAction("Login");
        }
    }
}