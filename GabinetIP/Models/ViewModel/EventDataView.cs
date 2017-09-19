using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GabinetIP.Models.ViewModel
{
    public class EventData
    {
        public int Id { get; set; }
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public SelectList Resource { get; set; }
        public string Text { get; set; }
        public Lekarze lekarze { get; set; }
        public int PacjentID { get; set; }
        public int LekarzID { get; set; }
        public string Pacjent { get; set; }
        public string Lekarz { get; set; }
    }

    public class Lekarze
    {
        public int? SelectedDoctorID { get; set; }
        public IEnumerable<UserProfileView> ListaLekarzy { get; set; }
    }
    public class EventDataView
    {
        public IEnumerable<EventData> Wizyty { get; set; }
    }
}