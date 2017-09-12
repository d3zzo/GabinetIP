using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GabinetIP.Models.ViewModel
{
    public class WizytaView
    {
        [Key]
        public int WizytaID { get; set; }
        public int? PacjentID { get; set; }
        public int? LekarzID { get; set; }
        public System.DateTime DataUtworzeniaWizyty { get; set; }
        [Display(Name = "Data zaplanowanej wizyty")]
        public System.DateTime DataWizyty { get; set; }
        public System.DateTime KoniecWizyty { get; set; }
        [Display(Name = "Opis wizyty")]
        public string OpisWizyty { get; set; }
        public string Pacjent { get; set; }
        public string Lekarz { get; set; }

    }
    public class WizytyDataView
    {
        public IEnumerable<WizytaView> Wizyty{ get; set; }
    }
}