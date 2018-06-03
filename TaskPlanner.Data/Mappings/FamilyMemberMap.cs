using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPlanner.Data.Models;

namespace TaskPlanner.Data.Mappings
{
    public class FamilyMemberMap : ClassMap<FamilyMember>
    {
        public FamilyMemberMap()
        {
            Id(x => x.Id);
            Map(x => x.IsActive);
            Map(x => x.FirstName).Length(32);
            Map(x => x.LastName).Length(128);

            References(c => c.Family)
                .Column("FamilyId")
                .Cascade.None();

            HasMany(c => c.Duties)
                .Inverse()
                .Cascade.All();

            Table("FamilyMembers");
        }
    }
}
