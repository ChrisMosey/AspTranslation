//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CMSailing.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class member
    {
        public member()
        {
            this.boats = new HashSet<boat>();
            this.memberships = new HashSet<membership>();
            this.memberTasks = new HashSet<memberTask>();
        }
    
        public int memberId { get; set; }
        public string fullName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string spouseFirstName { get; set; }
        public string spouseLastName { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string provinceCode { get; set; }
        public string postalCode { get; set; }
        public string homePhone { get; set; }
        public string email { get; set; }
        public Nullable<int> yearJoined { get; set; }
        public string comment { get; set; }
        public Nullable<bool> taskExempt { get; set; }
        public Nullable<bool> useCanadaPost { get; set; }
    
        public virtual ICollection<boat> boats { get; set; }
        public virtual province province { get; set; }
        public virtual ICollection<membership> memberships { get; set; }
        public virtual ICollection<memberTask> memberTasks { get; set; }
    }
}
