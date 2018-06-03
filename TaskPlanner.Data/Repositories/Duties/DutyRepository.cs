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
    public class DutyRepository : Repository<Duty>, IDutyRepository
    {
        private readonly ISession session;

        public DutyRepository(ISession _session) : base(_session)
        {
            session = _session;
        }

        //TODO: Delete after implementing DI.
        public DutyRepository() :base(FluentNHibernateHelper.OpenSession())
        {
            session = FluentNHibernateHelper.OpenSession();
        }
    }
}
