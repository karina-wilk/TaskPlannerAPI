using Castle.MicroKernel;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using Castle.Windsor.Installer;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanner.Data.Winsdor
{
    public class PersistenceFacility : AbstractFacility
    {
        protected override void Init()
        {
            Kernel.Register(
               Component.For<ISessionFactory>()
                   .UsingFactoryMethod(CreateSessionFactory),
               Component.For<ISession>()
                   .UsingFactoryMethod(OpenSession)
                   .LifestylePerWebRequest());
        }

        private static ISession OpenSession(IKernel kernel)
        {
            return kernel.Resolve<ISessionFactory>().OpenSession();
        }

        // Returns our session factory
        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(CreateDbConfig)
                .Mappings(m => m.AutoMappings.Add(CreateMappings()))
                .ExposeConfiguration(UpdateSchema)
                .CurrentSessionContext<WebSessionContext>()
                .BuildSessionFactory();
        }

        // Returns our database configuration
        private static MsSqlConfiguration CreateDbConfig()
        {
            return MsSqlConfiguration
                .MsSql2012
                .ConnectionString(c => c.FromConnectionStringWithKey("DefaultConnection"));
        }

        // Returns our mappings
        private static AutoPersistenceModel CreateMappings()
        {
            return AutoMap
                .Assembly(System.Reflection.Assembly.GetCallingAssembly())
                .Where(t => t.Namespace != null && t.Namespace.EndsWith("Models"))
                .Conventions.Setup(c => c.Add(DefaultCascade.SaveUpdate()));
        }

        // Updates the database schema if there are any changes to the model,
        // or drops and creates it if it doesn't exist
        private static void UpdateSchema(NHibernate.Cfg.Configuration cfg)
        {
            new SchemaUpdate(cfg)
                .Execute(false, true);
        }
    }
}
