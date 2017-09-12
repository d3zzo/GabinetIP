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
        public List<WizytaView> GetAllWizyty()
        {
            List<WizytaView> wizyty = new List<WizytaView>();
            using (GabinetDBEntities db = new GabinetDBEntities())
            {
                WizytaView WV;
                foreach (var w in db.Wizyta)
                {
                    WV = new WizytaView();
                    WV.WizytaID = (int)w.WizytaID;
                    WV.PacjentID = (int)w.PacjentID;
                    WV.LekarzID = (int)w.LekarzID;
                    //WV.DataWizyty = w.DataWizyty;
                    WV.DataWizyty = (DateTime)w.Start;
                    WV.KoniecWizyty = (DateTime)w.Koniec;
                    WV.OpisWizyty = w.OpisWizyty;

                    var pacjent = db.SYSUserProfiles.Where(i => i.SYSUserID.Equals(w.PacjentID)).FirstOrDefault();
                    if (pacjent != null)
                    {
                        WV.Pacjent = pacjent.FirstName + " " + pacjent.LastName;
                    }

                    var lekarz = db.SYSUserProfiles.Where(i => i.SYSUserID.Equals(w.LekarzID)).FirstOrDefault();
                    if (lekarz != null)
                    {
                        WV.Lekarz= lekarz.FirstName + " " + lekarz.LastName;
                    }

                    wizyty.Add(WV);
                }
                
            }
                return wizyty;
        }

        public WizytyDataView HistoriaWizytDataView(string userName)
        {
            WizytyDataView WDV = new WizytyDataView();
            List<WizytaView> wizyty = GetAllWizyty();
            UserManager UM = new UserManager();
            var userID = UM.GetUserID(userName);
            var wizyty1 = wizyty.Where(i => i.DataWizyty <= DateTime.Now && i.PacjentID.Equals(userID));
            WDV.Wizyty = wizyty1;

            return WDV;
        }

        public WizytyDataView ZaplanowaneWizytyDataView(string userName)
        {
            WizytyDataView WDV = new WizytyDataView();
            List<WizytaView> wizyty = GetAllWizyty();
            UserManager UM = new UserManager();
            var userID = UM.GetUserID(userName);
            var wizyty1 = wizyty.Where(i => i.DataWizyty > DateTime.Now && i.PacjentID.Equals(userID));
            WDV.Wizyty = wizyty1;

            return WDV;
        }

        public WizytyDataView ZapisywanieNaWizyteDataView()
        {
            WizytyDataView WDV = new WizytyDataView();
            List<WizytaView> wizyty = GetAllWizyty();
            return WDV;
        }
        
    }
}