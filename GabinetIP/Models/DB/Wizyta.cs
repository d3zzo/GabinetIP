//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GabinetIP.Models.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Wizyta
    {
        public int WizytaID { get; set; }
        public int PacjentID { get; set; }
        public int LekarzID { get; set; }
        public Nullable<System.DateTime> DataUtworzeniaWizyty { get; set; }
        public Nullable<System.DateTime> DataWizyty { get; set; }
        public string OpisWizyty { get; set; }
    
        public virtual SYSUser SYSUser { get; set; }
        public virtual SYSUser SYSUser1 { get; set; }
    }
}