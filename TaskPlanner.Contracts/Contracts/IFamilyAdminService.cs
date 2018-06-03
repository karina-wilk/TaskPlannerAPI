using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TaskPlanner.Shared.DTO;
using TaskPlanner.Data.Models;

namespace TaskPlanner.Shared.Contracts
{
    [ServiceContract]
    public interface IFamilyAdminService
    {
        [OperationContract]
        bool AddMemberToFamily(int familyId, FamilyMember member);
    }
}