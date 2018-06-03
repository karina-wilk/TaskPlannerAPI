using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPlanner.Contracts;
using TaskPlanner.Data.Models;
using TaskPlanner.Data.Repositories;
using TaskPlanner.Services.Services;

namespace TaskPlanner.Data.Winsdor
{
    public class RepositoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //container.Register(Classes.FromThisAssembly()
            //                    .Where(Component.IsInSameNamespaceAs<Repository<Family>>())
            //                    .WithService.DefaultInterfaces()
            //                    .LifestyleTransient());

            container.Register(
               Component.For<IRepository<Family>, Repository<Family>>(),

                Component.For<IFamilyManager, FamilyManager>()
            );
        }
    }
}
