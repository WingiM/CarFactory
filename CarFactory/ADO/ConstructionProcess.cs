//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarFactory.ADO
{
    using System;
    using System.Collections.Generic;
    
    public partial class ConstructionProcess
    {
        public int Id { get; set; }
        public int ConstructionId { get; set; }
        public int CompletedStep { get; set; }
        public System.DateTime DateCompleted { get; set; }
        public int CompletedBy { get; set; }
    
        public virtual Construction Construction { get; set; }
        public virtual ConstructionStep ConstructionStep { get; set; }
        public virtual User User { get; set; }
    }
}