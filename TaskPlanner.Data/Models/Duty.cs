using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanner.Data.Models
{
    public class Duty
    {
        public virtual int Id { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime DateModified { get; set; }

        public virtual DateTime? StartDate { get; set; }
        public virtual string Description { get; set; }//should be nullable!
        public virtual bool Enabled { get; set; }
        public virtual bool TaskNeverEnds { get; set; }
        public virtual int TaskType { get; set; }
        public virtual string Title { get; set; }

        public virtual FamilyMember FamilyMember { get; set; }
    }
}
