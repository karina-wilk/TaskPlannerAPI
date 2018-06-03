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
    public class DutyClient : ClientBase<IDutyService>, IDutyService
    {
        public DutyClient(string endpointName)
            : base(endpointName)
        {
        }

        public DutyClient(Binding binding, EndpointAddress address)
            : base(binding, address)
        {
        }

        public List<DutyDto> GetAll(int familyMemberId)
        {
            return Channel.GetAll(familyMemberId);
        }

        public List<DutyDto> GetForDate(int familyMemberId, DateTime dateTime)
        {
            return Channel.GetForDate(familyMemberId, dateTime);
        }
    }
}
