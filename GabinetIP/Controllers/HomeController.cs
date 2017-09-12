using GabinetIP.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GabinetIP.Models.ViewModel;
using GabinetIP.Models.EntityManager;
using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Events.Calendar;
using GabinetIP.Models.DB;

namespace GabinetIP.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Welcome()
        {            
            return View();
        }

        [AuthorizeRoles("Admin")]
        public ActionResult AdminOnly()
        {
            return View();
        }

        [Authorize]
        public ActionResult Zaplanowane()
        {
            if (User.Identity.IsAuthenticated)
            {
                string loginName = User.Identity.Name;
                WizytyManager WM = new WizytyManager();
                WizytyDataView WDV = WM.ZaplanowaneWizytyDataView(loginName);
                return View(WDV);
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Historia()
        {
            if (User.Identity.IsAuthenticated)
            {
                string loginName = User.Identity.Name;
                WizytyManager WM = new WizytyManager();
                WizytyDataView WDV = WM.HistoriaWizytDataView(loginName);
                return View(WDV);
            }
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult Zapisywanie()
        {
            if (User.Identity.IsAuthenticated)
            {
                WizytyManager WM = new WizytyManager();
                WizytyDataView WDV = WM.ZapisywanieNaWizyteDataView();
                return View(WDV);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult UnAuthorized()
        {
            return View();
        }

        [AuthorizeRoles("Admin")]
        public ActionResult ManageUserPartial(string status = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                string loginName = User.Identity.Name;
                UserManager UM = new UserManager();
                UserDataView UDV = UM.GetUserDataView(loginName);

                string message = string.Empty;
                if (status.Equals("update"))
                    message = "Update Successful";
                else if (status.Equals("delete"))
                    message = "Delete Successful";

                ViewBag.Message = message;

                return PartialView(UDV);
            }

            return RedirectToAction("Index", "Home");
        }

        [AuthorizeRoles("Admin")]
        public ActionResult UpdateUserData(int userID, string loginName, string password, string firstName, string lastName, string gender, int roleID = 0)
        {
            UserProfileView UPV = new UserProfileView();
            UPV.SYSUserID = userID;
            UPV.LoginName = loginName;
            UPV.Password = password;
            UPV.FirstName = firstName;
            UPV.LastName = lastName;
            UPV.Gender = gender;

            if (roleID > 0)
                UPV.LOOKUPRoleID = roleID;

            UserManager UM = new UserManager();
            UM.UpdateUserAccount(UPV);

            return Json(new { success = true });
        }

        [AuthorizeRoles("Admin")]
        public ActionResult DeleteUser(int userID)
        {
            UserManager UM = new UserManager();
            UM.DeleteUser(userID);
            return Json(new { success = true });
        }

        [Authorize]
        public ActionResult EditProfile()
        {
            string loginName = User.Identity.Name;
            UserManager UM = new UserManager();
            UserProfileView UPV = UM.GetUserProfile(UM.GetUserID(loginName));
            return View(UPV);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditProfile(UserProfileView profile)
        {
            if (ModelState.IsValid)
            {
                UserManager UM = new UserManager();
                UM.UpdateUserAccount(profile);

                ViewBag.Status = "Update Sucessful!";
            }
            return View(profile);
        }

        public ActionResult Calendar()
        {
            return View();
        }

        public ActionResult Backend()
        {
            return new Dpc().CallBack(this);
        }

        class Dpc : DayPilotCalendar
        {
            GabinetDBEntities db = new GabinetDBEntities();

            protected override void OnInit(InitArgs e)
            {
                Update(CallBackUpdateType.Full);
            }

            protected override void OnEventResize(EventResizeArgs e)
            {
                var toBeResized = (from ev in db.Wizyta where ev.WizytaID == Convert.ToInt32(e.Id) select ev).First();
                toBeResized.Start = e.NewStart;
                toBeResized.Koniec = e.NewEnd;
                db.SaveChanges();
                Update();
            }

            protected override void OnEventMove(EventMoveArgs e)
            {
                var toBeResized = (from ev in db.Wizyta where ev.WizytaID == Convert.ToInt32(e.Id) select ev).First();
                toBeResized.Start = e.NewStart;
                toBeResized.Koniec = e.NewEnd;
                db.SaveChanges();
                Update();
            }

            protected override void OnTimeRangeSelected(TimeRangeSelectedArgs e)
            {
                var toBeCreated = new Wizyta
                {
                    Start = e.Start,
                    Koniec = e.End,
                    OpisWizyty = (string)e.Data["name"]
                };
                db.Wizyta.Add(toBeCreated);
                db.SaveChanges();
                Update();
            }

            protected override void OnFinish()
            {
                if (UpdateType == CallBackUpdateType.None)
                {
                    return;
                }
                Events = from ev in db.Wizyta select ev;

                DataIdField = "WizytaID";
                DataTextField = "OpisWizyty";
                DataStartField = "Start";
                DataEndField = "Koniec";

                Update();
            }
        }

    }
}