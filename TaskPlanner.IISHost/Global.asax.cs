using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using TaskPlanner.Contracts;
using TaskPlanner.Services.Services;

namespace TaskPlanner.IISHost
{
    public class Global : System.Web.HttpApplication
    {

        private static IWindsorContainer container;

        protected void Application_Start(object sender, EventArgs e)
        {

            //SPRóbować jeszcze może to: https://github.com/gusrodriguez/CastleAndWcf


            container = new WindsorContainer();

            container.Register(Component.For<IFamilyManager>()
           .ImplementedBy<FamilyManager>()
           .Named("FamilyManager")
           .AsWcfService(new DefaultServiceModel()
           .AddEndpoints(WcfEndpoint
                  .BoundTo(new BasicHttpBinding
                  {
                      MaxReceivedMessageSize = 2147483647,
                      MaxBufferSize = 2147483647,
                      MaxBufferPoolSize = 2147483647,
                      TransferMode = TransferMode.Streamed,
                      MessageEncoding = WSMessageEncoding.Mtom//,
                      //ReaderQuotas = new XmlDictionaryReaderQuotas
                      //{
                      //    MaxDepth = 2147483647,
                      //    MaxArrayLength = 2147483647,
                      //    MaxStringContentLength = 2147483647,
                      //    MaxNameTableCharCount = 2147483647,
                      //    MaxBytesPerRead = 2147483647
                      //}
                  }))
                  .Hosted()
                  .PublishMetadata())
          .LifeStyle.PerWcfOperation());

            container.AddFacility<WcfFacility>();


            ////STARE PODEJSCIE:
            ////container = new WindsorContainer()
            ////   .Install(FromAssembly.InThisApplication()); //TODO Spr. czy to działa?

            //container = new WindsorContainer();
            //container.AddFacility<WcfFacility>();
            //container.Install(FromAssembly.Named("TaskPlanner.Data")); //Działa :). Wchodzi w PersistenceInstaller.Init().


            ////container.Register(Component.For<IFamilyManager, FamilyManager>());

            ////var host = new ServiceHost().New(c =>
            ////{
            ////    c.Service<WcfServiceWrapper<SampleService, ISampleService>>(s =>
            ////    {
            ////        s.SetServiceName("CalculatorService");
            ////        s.ConstructUsing(x => new WcfServiceWrapper<SampleService, ISampleService>("SampleService"));
            ////        s.WhenStarted(service => service.Start());
            ////        s.WhenStopped(service => service.Stop());
            ////    });
            ////    c.RunAsLocalSystem();

            ////    c.SetDescription("Runs SampleService.");
            ////    c.SetDisplayName("SampleService");
            ////    c.SetServiceName("SampleService");
            ////});






            ////https://dzimchuk.net/dependency-injection-with-wcf/
            ////http://scotthannen.org/blog/2016/04/13/wcf-dependency-injection-in-5-minutes.html

            ////try
            ////{
            ////    var controller = container.Resolve<FamilyManager>();
            ////}
            ////catch(Exception ex)
            ////{

            ////}

            ////ServiceHost hostGeoManager = new ServiceHost(typeof(FamilyManager));

            ////hostGeoManager.Open();

            ////Console.WriteLine("Services started. Press [Enter] to exit.");
            ////Console.ReadLine();

            ////hostGeoManager.Close();
            ////Console.Read();
        }
        //


       

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}