using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TaskPlanner.Shared;
using TaskPlanner.Data.Models;
using TaskPlanner.Data.Repositories;
using TaskPlanner.Services;
using TaskPlanner.Services.Services;

namespace TaskPlanner.Host
{
    class Program
    {
        private static IWindsorContainer container;

        static void Main(string[] args)
        {
           // container = new WindsorContainer();
           // //container.Install(FromAssembly.Named("TaskPlanner.Data")); //Działa :). Wchodzi w PersistenceInstaller.Init().
           // container.Register(Component.For<IRepository<Family>, Repository<Family>>());
           //// container.Register(Component.For<IFamilyManager, FamilyManager>());
           // container.AddFacility<WcfFacility>(f => f.CloseTimeout = TimeSpan.Zero)
           //     .Register(Component.For<IFamilyManager>()
           //     .ImplementedBy<FamilyManager>()
           //     .AsWcfService(new DefaultServiceModel()));

           // try
           // {
           //     var controller = container.Resolve<FamilyManager>();
           // }
           // catch (Exception ex)
           // {

           // }
           // //https://dzimchuk.net/dependency-injection-with-wcf/
           // //http://scotthannen.org/blog/2016/04/13/wcf-dependency-injection-in-5-minutes.html


            ServiceHost hostGeoManager = new ServiceHost(typeof(FamilyService));
            
            hostGeoManager.Open();

            ServiceHost hostDutyService = new ServiceHost(typeof(DutyService));
            hostDutyService.Open();

            Console.WriteLine("FamilyService & DutyService Services started. Press [Enter] to exit.");
            Console.ReadLine();

            hostDutyService.Close();
            hostGeoManager.Close();
            Console.Read();
        }
    }
}
