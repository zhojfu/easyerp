using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyERP.Web.Controllers
{
    using Doamin.Service.Authentication;
    using Doamin.Service.Users;
    using EasyERP.Web.Framework.Security.Captcha;
    using EasyERP.Web.Models.Users;
    using System.Web.UI;

    public class UserController : BasePublicController
    {
        private readonly CaptchaSettings captchaSettings = new CaptchaSettings();

        private readonly IUserService userService;
        private readonly IAuthenticationService authenticationService;

        public UserController(IUserService userService, IAuthenticationService authenticationService)
        {
            this.userService = userService;
            this.authenticationService = authenticationService;
        }

        public ActionResult Index()
        {
            return this.RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            var model = new LoginModel();
            model.DisplayCaptcha = captchaSettings.Enabled;
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            //if (captchaSettings.Enabled && !captchaValid)
            //{
            //    ModelState.AddModelError("", "验证码错误");
            //}

            if (ModelState.IsValid)
            {
                var loginResult = this.userService.ValidateUser(model.Username.Trim(), model.Password);
                switch (loginResult)
                {
                    case UserLoginResults.Successful:
                        {
                            var user = this.userService.GetUserByName(model.Username);
                            this.authenticationService.SignIn(user, false);

                            // TODO:redirection to page according user type
                            return RedirectToAction("List", "Product");
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
            this.authenticationService.SignOut();
            return this.RedirectToRoute("Home");
        }
    }
}