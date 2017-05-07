using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GabinetIP.Models.ViewModel;
using GabinetIP.Models.EntityManager;
using System.Web.Security;

namespace GabinetIP.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(UserSignUpView USV)
        {
            if (ModelState.IsValid)
            {
                UserManager UM = new UserManager();
                if (!UM.IsLoginNameExist(USV.LoginName))
                {
                    UM.AddUserAccount(USV);
                    FormsAuthentication.SetAuthCookie(USV.LoginName, false);
                    return RedirectToAction("Welcome", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login name already taken");
                }
            }
            return View();
        }
    }
}