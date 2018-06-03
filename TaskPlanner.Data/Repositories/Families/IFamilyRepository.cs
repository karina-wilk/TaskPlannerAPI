using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPlanner.Data.Models;

namespace TaskPlanner.Data.Repositories
{
    public interface IFamilyRepository : IRepository<Family>
    {
        List<FamilyMember> GetNotActiveMembersStoredProcedure(int familyId);
        void AddMember(int familyId, FamilyMember member);
    }
}
