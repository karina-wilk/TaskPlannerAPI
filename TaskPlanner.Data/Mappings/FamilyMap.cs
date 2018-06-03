using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPlanner.Data.Models;

namespace TaskPlanner.Data.Mappings
{
    public class FamilyMap : ClassMap<Family>
    {
        public FamilyMap()
        {
            Id(x => x.Id);
            Map(x => x.IsActive);
            Map(x => x.Name);
            HasMany(c => c.Members)
                .Inverse()
                .Cascade.All();

            Table("Families");
        }
    }
}
