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
    public interface IFamilyService
    {
        [OperationContract]
        List<FamilyDto> GetAll(bool getOnlyActive);

        [OperationContract]
        Family Add(Family family);

        [OperationContract]
        List<FamilyMember> GetNotActiveMembersStoredProcedure(int familyId);
    }
}