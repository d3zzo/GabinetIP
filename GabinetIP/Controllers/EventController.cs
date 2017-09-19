using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GabinetIP.Models.DB;
using DayPilot.Web.Mvc.Json;
using GabinetIP.Models.EntityManager;
using GabinetIP.Models.ViewModel;

namespace GabinetIP.Controllers
{
    public class EventController : Controller
    {
        GabinetDBEntities dc = new GabinetDBEntities();

        [Authorize]
        public ActionResult Edit(string id)
        {
            var ids = Convert.ToInt32(id);
            var t = (from tr in dc.Event where tr.Id == ids select tr).First();
            var ev = new EventData
            {
                Id = t.Id,
                Start = t.Start,
                End = t.End,
                Text = t.Text
                    
            };
            return View(ev);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(FormCollection form)
        {
            int id = Convert.ToInt32(form["Id"]);
            DateTime start = Convert.ToDateTime(form["Start"]);
            DateTime end = Convert.ToDateTime(form["End"]);
            string text = form["Text"];

            var record = (from e in dc.Event where e.Id == id select e).First();
            record.Start = start;
            record.End = end;
            record.Text = text;
            dc.SaveChanges();

            return JavaScript(SimpleJsonSerializer.Serialize("OK"));
        }

        [Authorize]
        public ActionResult Create()
        {
            UserManager UM = new UserManager();
            var DDV = UM.GetDoctorDataView();
            //var ED = new EventData {
            //    Start = Convert.ToDateTime(Request.QueryString["start"]),
            //    End = Convert.ToDateTime(Request.QueryString["end"]),               
            //};
            DDV.Start = Convert.ToDateTime(Request.QueryString["start"]);
            DDV.End = Convert.ToDateTime(Request.QueryString["end"]);
            
            return View(DDV);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection form)
        {
            UserManager UM = new UserManager();
            DateTime start = Convert.ToDateTime(form["Start"]);
            DateTime end = Convert.ToDateTime(form["End"]);
            //string text = form["Text"];
            int resource = Convert.ToInt32(form["Resource"]);
            int lekarz = Convert.ToInt32(form["lekarze.SelectedDoctorID"]);
            //string recurrence = form["Recurrence"];
            var doctorFull = UM.GetUserFullName(lekarz);
            var uzyt = System.Web.HttpContext.Current.User.Identity.Name;

            var toBeCreated = new Event() { Start = start, End = end, Text = doctorFull, PacjentID = UM.GetUserID(uzyt), LekarzID = lekarz };
            dc.Event.Add(toBeCreated);
            dc.SaveChanges();

            return JavaScript(SimpleJsonSerializer.Serialize("OK"));
        }

        //public class MoreEventData
        //{
        //    public EventData ed { get; set; }
        //}

    }
}