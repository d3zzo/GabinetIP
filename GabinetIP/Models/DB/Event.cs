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
    
    public partial class Event
    {
        public int Id { get; set; }
        public System.DateTime Start { get; set; }
        public System.DateTime End { get; set; }
        public string Text { get; set; }
        public Nullable<int> PacjentID { get; set; }
        public Nullable<int> LekarzID { get; set; }
    
        public virtual SYSUser SYSUser { get; set; }
        public virtual SYSUser SYSUser1 { get; set; }
    }
}
