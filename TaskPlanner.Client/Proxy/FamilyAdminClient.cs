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
    public class FamilyAdminClient : ClientBase<IFamilyAdminService>, IFamilyAdminService
    {
        public FamilyAdminClient(string endpointName)
            : base(endpointName)
        {
        }

        public FamilyAdminClient(Binding binding, EndpointAddress address)
            : base(binding, address)
        {
        }

        public bool AddMemberToFamily(int familyId, FamilyMember member)
        {
            return Channel.AddMemberToFamily(familyId, member);
        }
    }
}
