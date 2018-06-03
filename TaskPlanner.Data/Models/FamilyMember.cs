using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanner.Data.Models
{
    public class FamilyMember
    {
        public virtual int Id { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual Family Family { get; set; }

        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

        [NonSerialized]
        private IList<Duty> duties = new List<Duty>();

        [DataMember]
        public virtual IList<Duty> Duties
        {
            get { return duties; }
            set { duties = new List<Duty>(value); }
        }

        public virtual void AddDuty(Duty duty)
        {
            duty.FamilyMember = this;
            Duties.Add(duty);
        }

        public FamilyMember()
        {
            Duties = new List<Duty>();
        }
    }
}
