using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using TaskPlanner.Shared;
using TaskPlanner.Shared.DTO;
using TaskPlanner.Data.Models;
using TaskPlanner.Shared.Contracts;

namespace TaskPlanner.Client.Proxy
{
    public class FamilyClient : ClientBase<IFamilyService>, IFamilyService
    {
        public FamilyClient(string endpointName)
            : base(endpointName)
        {
        }

        public FamilyClient(Binding binding, EndpointAddress address)
            : base(binding, address)
        {
        }

        public Family Add(Family family)
        {
            return Channel.Add(family);
        }

        public List<FamilyDto> GetAll(bool getOnlyActive)
        {
            return Channel.GetAll(getOnlyActive);
        }

        public List<FamilyMember> GetNotActiveMembersStoredProcedure(int familyId)
        {
            return Channel.GetNotActiveMembersStoredProcedure(familyId);
        }
    }
}
