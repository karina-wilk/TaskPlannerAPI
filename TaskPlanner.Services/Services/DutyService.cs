using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPlanner.Shared;
using TaskPlanner.Shared.DTO;
using TaskPlanner.Data;
using TaskPlanner.Data.Models;
using TaskPlanner.Data.Repositories;
using TaskPlanner.Shared.Contracts;

namespace TaskPlanner.Services.Services
{
    public class DutyService : IDutyService
    {
        private readonly IDutyRepository dutyRepository;

        //TODO: Delete this constructor after implementing DI.
        public DutyService()
        {
            dutyRepository = new DutyRepository();
        }

        public DutyService(IDutyRepository dRepo)
        {
            dutyRepository = dRepo;
        }

        public List<DutyDto> GetAll(int familyMemberId)
        {
            if(familyMemberId <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return dutyRepository.GetAll()
                .Where(c => c.FamilyMember.Id == familyMemberId)
                .Select(c => new DutyDto(c))
                .ToList();
        }

        #region GetForDate()
        public List<DutyDto> GetForDate(int familyMemberId, DateTime dateTime)
        {
            if (familyMemberId <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            var query = dutyRepository.Get(c =>
                        c.FamilyMember.Id == familyMemberId &&
                        c.Enabled &&
                        c.StartDate.HasValue && c.StartDate.Value.Date == dateTime.Date
                    );

            return query.Select(c => new DutyDto(c))
                    .ToList();
        } 
        #endregion

    }
}
