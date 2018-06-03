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
    public interface IDutyService
    {
        [OperationContract]
        List<DutyDto> GetAll(int familyMemberId);

        [OperationContract]
        List<DutyDto> GetForDate(int familyMemberId, DateTime dateTime);
    }
}