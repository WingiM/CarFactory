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
    
    public partial class Automobile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Automobile()
        {
            this.Constructions = new HashSet<Construction>();
        }
    
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Generation { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Construction> Constructions { get; set; }
    }
}
