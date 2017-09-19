using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GabinetIP.Models.DB;
using GabinetIP.Models.ViewModel;
using GabinetIP.Models.EntityManager;

namespace GabinetIP.Models.EntityManager
{
    public class WizytyManager
    {
        public List<EventData> GetAllWizyty()
        {
            List<EventData> wizyty = new List<EventData>();
            using (GabinetDBEntities db = new GabinetDBEntities())
            {
                EventData ED;
                foreach (var w in db.Event)
                {
                    ED = new EventData();
                    ED.Id = w.Id;
                    ED.PacjentID = (int)w.PacjentID;
                    ED.LekarzID = (int)w.LekarzID;
                    ED.Start = w.Start;
                    ED.End= w.End;
                    ED.Text= w.Text;

                    var pacjent = db.SYSUserProfiles.Where(i => i.SYSUserID == w.PacjentID).Single();
                    if (pacjent != null)
                    {
                        ED.Pacjent = pacjent.FirstName + " " + pacjent.LastName;
                    }

                    var lekarz = db.SYSUserProfiles.Where(i => i.SYSUserID ==w.LekarzID).FirstOrDefault();
                    if (lekarz != null)
                    {
                        ED.Lekarz = lekarz.FirstName + " " + lekarz.LastName;
                    }

                    wizyty.Add(ED);
                }
                
            }
                return wizyty;
        }

        public EventDataView HistoriaWizytDataView(string userName)
        {
            EventDataView EDV = new EventDataView();
            List<EventData> wizyty = GetAllWizyty();
            UserManager UM = new UserManager();
            var userID = UM.GetUserID(userName);
            var wizyty1 = wizyty.Where(i => i.Start <= DateTime.Now && i.PacjentID.Equals(userID));
            EDV.Wizyty = wizyty1;

            return EDV;
        }

        public EventDataView ZaplanowaneWizytyDataView(string userName)
        {
            EventDataView EDV = new EventDataView();
            List<EventData> wizyty = GetAllWizyty();
            UserManager UM = new UserManager();
            var userID = UM.GetUserID(userName);
            EDV.Wizyty = wizyty.Where(i => i.Start > DateTime.Now && i.PacjentID.Equals(userID));
            return EDV;
        }
        public EventDataView ZaplanowaneWizytyLekarzDataView(string userName)
        {
            EventDataView EDV = new EventDataView();
            List<EventData> wizyty = GetAllWizyty();
            UserManager UM = new UserManager();
            var userID = UM.GetUserID(userName);
            EDV.Wizyty = wizyty.Where(i => i.Start > DateTime.Now && i.LekarzID.Equals(userID));
            return EDV;
        }

        public EventDataView ZapisywanieNaWizyteDataView()
        {
            EventDataView EDV = new EventDataView();
            List<EventData> wizyty = GetAllWizyty();
            return EDV;
        }
        
    }
}