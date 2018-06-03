using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPlanner.Data.ModelResultTransformers;
using TaskPlanner.Data.Models;

namespace TaskPlanner.Data.Repositories
{
    public class FamilyRepository : Repository<Family>, IFamilyRepository
    {
        private readonly ISession session;

        public FamilyRepository(ISession _session) : base(_session)
        {
            session = _session;
        }

        //TODO: Delete after implementing DI.
        public FamilyRepository() :base(FluentNHibernateHelper.OpenSession())
        {
            session = FluentNHibernateHelper.OpenSession();
        }

        #region GetNotActiveMembersStoredProcedure()
        public List<FamilyMember> GetNotActiveMembersStoredProcedure(int familyId)
        {
            var q = session.CreateSQLQuery("EXEC [dbo].[GetNotActiveMembers] @familyId=:familyId ");
            q.SetInt32("familyId", familyId);

            return q.SetResultTransformer(new GetNotActiveMembersTransformer()).List<FamilyMember>().ToList();

            //OR:
            //IQuery query = session.CreateSQLQuery("exec LogData @Time=:time, @Data=:data");
            //query.SetDateTime("time", time);
            //query.SetString("data", data);
            //query.ExecuteUpdate();
        }
        #endregion

        #region AddMember()
        public void AddMember(int familyId, FamilyMember member)
        {
            using (var t = session.BeginTransaction())
            {
                var family = session.Query<Family>().Where(c => c.Id == familyId).FirstOrDefault();
                if (family != null)
                {
                    family.AddMember(member);

                    session.SaveOrUpdate(member);
                    session.SaveOrUpdate(family);
                    t.Commit();
                }
            }
        } 
        #endregion
    }
}
