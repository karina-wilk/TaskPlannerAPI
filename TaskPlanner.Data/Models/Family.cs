using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanner.Data.Models
{
    public class Family
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual bool IsActive { get; set; }

        [NonSerialized]
        private IList<FamilyMember> mbers = new List<FamilyMember>();

        //For WCF & NHibernate:
        [DataMember]
        public virtual IList<FamilyMember> Members
        {
            get { return mbers; }
            set { mbers = new List<FamilyMember>(value); }
        }

        public Family()
        {
            Members = new List<FamilyMember>();
        }

        public virtual void AddMember(FamilyMember familyMember)
        {
            familyMember.Family = this;
            Members.Add(familyMember);
        }
    }
}
