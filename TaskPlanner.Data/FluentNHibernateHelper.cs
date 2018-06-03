using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPlanner.Data.Models;

namespace TaskPlanner.Data
{
    public static class FluentNHibernateHelper
    {
        public static ISession OpenSession()
        {
            ISessionFactory sessionFactory = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(c => c.FromConnectionStringWithKey("DefaultConnection"))
                    .ShowSql()
                ).Mappings(m =>
                    m.FluentMappings
                    .AddFromAssemblyOf<Family>()
                    .AddFromAssemblyOf<Duty>()
                ).ExposeConfiguration(cfg => new SchemaExport(cfg)
                 .Create(false, false))
                .BuildSessionFactory();

            return sessionFactory.OpenSession();
        }
    }
}
