using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPlanner.Data.Models;

namespace TaskPlanner.Data.Mappings
{
    public class DutyMap : ClassMap<Duty>
    {
        public DutyMap()
        {
            Id(x => x.Id);
            Map(x => x.DateCreated);
            Map(x => x.DateModified);
            Map(x => x.StartDate).Nullable();
            Map(x => x.Description).Nullable();
            Map(x => x.Enabled);
            Map(x => x.TaskNeverEnds);
            Map(x => x.TaskType);
            Map(x => x.Title).Nullable();

            References(c => c.FamilyMember)
                .Column("FamilyMemberId")
                .Cascade.None();

            Table("Duties");
        }
    }
}
