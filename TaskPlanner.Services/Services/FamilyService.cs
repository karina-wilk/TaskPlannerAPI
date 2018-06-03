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
using System.Threading;
using System.ServiceModel;
using System.Security.Permissions;

namespace TaskPlanner.Services.Services
{
    public class FamilyService : IFamilyService, IFamilyAdminService
    {
        private readonly IFamilyRepository familyRepository;

        //TODO: Delete this constructor after implementing DI.
        public FamilyService()
        {
            familyRepository = new FamilyRepository();
        }

        public FamilyService(IFamilyRepository fRepo)
        {
            familyRepository = fRepo;
        }

        #region GetNotActiveMembersStoredProcedure()
        public List<FamilyMember> GetNotActiveMembersStoredProcedure(int familyId)
        {
            if (familyId <= 0)
            {
                throw new ArgumentException("FamilyId has to be a number greater than zero!");
            }
            return familyRepository.GetNotActiveMembersStoredProcedure(familyId);
        } 
        #endregion

        #region AddMemberToFamily()
        [OperationBehavior(TransactionScopeRequired =true)]
        [PrincipalPermission(SecurityAction.Demand, Role ="admin")]
        public bool AddMemberToFamily(int familyId, FamilyMember member)
        {
            var name = Thread.CurrentPrincipal.Identity.Name;
            var isAdmin = Thread.CurrentPrincipal.IsInRole("admin");

            if (familyId < 0)
            {
                throw new ArgumentException();
            }
            if (member == null)
            {
                throw new ArgumentNullException();
            }
            if (member.FirstName != null && member.FirstName.Length > 32 || member.LastName != null && member.LastName.Length > 128)
            {
                throw new ArgumentOutOfRangeException();
            }

            familyRepository.AddMember(familyId, member);

            return true;
        }
        #endregion

        #region GetAll()
        public List<FamilyDto> GetAll(bool getOnlyActive)
        {
            List<FamilyDto> result = new List<FamilyDto>();
            try
            {
                if (!getOnlyActive)
                {
                    result = familyRepository.GetAll().Select(c => new FamilyDto(c)).ToList();
                }
                else
                {
                    result = familyRepository.GetAll().Where(c => c.IsActive).Select(c => new FamilyDto(c)).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                return new List<FamilyDto>();
            }
        }
        #endregion

        #region Add()
        public Family Add(Family family)
        {
            if (family == null)
            {
                throw new ArgumentNullException();
            }
            return familyRepository.SaveOrUpdate(family);
        } 
        #endregion
    }
}
