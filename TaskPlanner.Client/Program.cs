using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPlanner.Client.Proxy;
using TaskPlanner.Data.Models;

namespace TaskPlanner.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayWithFamilyAdminService();

            //PlayingWithFamilyService();
            //PlayingWithDutyService();

            Console.WriteLine("END OF CLIENT APP!");
            Console.ReadLine();
        }

        #region PlayingWithDutyService()
        public static void PlayingWithDutyService()
        {
            DutyClient dutyProxy = new DutyClient("BasicHttpBinding_IDutyService");

            dutyProxy.Endpoint.Binding.SendTimeout = TimeSpan.FromMinutes(15);
            dutyProxy.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromMinutes(15);

            dutyProxy.Open();

            var duties = dutyProxy.GetAll(2);
            foreach (var d in duties)
            {
                Console.WriteLine($"Duty {d.Id}: {d.Description}");
            }
            dutyProxy.Close();

            Console.WriteLine(">> End of DutyService CLIENT!");
            Console.ReadLine();
        } 
        #endregion

        #region PlayWithFamilyAdminService()
        private static void PlayWithFamilyAdminService()
        {
            FamilyAdminClient proxy = new FamilyAdminClient("httpAdminEP");

            proxy.Endpoint.Binding.SendTimeout = TimeSpan.FromMinutes(15);
            proxy.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromMinutes(15);

            proxy.ClientCredentials.UserName.UserName = "karina";
            proxy.ClientCredentials.UserName.Password = "abcABC";

            PlayingWithFamilyAdminService(proxy);

            proxy.Close();

            Console.WriteLine(">> End of FamilyAdminService CLIENT!");
            Console.ReadLine();
        } 
        #endregion

        #region PlayWithFamilyService()
        private static void PlayWithFamilyService()
        {
            FamilyClient proxy = new FamilyClient("httpEP");
            proxy.Endpoint.Binding.SendTimeout = TimeSpan.FromMinutes(15);
            proxy.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromMinutes(15);

            PlayingWithFamilyService(proxy);

            proxy.Close();

            Console.WriteLine(">> End of FamilyService CLIENT!");
            Console.ReadLine();
        } 
        #endregion

        public static void PlayingWithFamilyService(FamilyClient proxy)
        {
            // proxy.Add(new Data.Models.Family() { IsActive = true, Name = "Kowalscy" });
            //proxy.Add(new Data.Models.Family() { IsActive = true, Name = "Nowaki" });

            System.Threading.Thread.Sleep(1500);
            var result = proxy.GetAll(true);
            Console.WriteLine($"Downloaded {result.Count} results");


            Console.WriteLine("Not active members (stored procedure call):");
            var notActiveMembers = proxy.GetNotActiveMembersStoredProcedure(2);// result[1].Id);
            foreach (var r in notActiveMembers)
            {
                Console.WriteLine(r.FirstName);
            }
        }

        #region PlayingWithFamilyAdminService()
        public static void PlayingWithFamilyAdminService(FamilyAdminClient proxy)
        {
            System.Threading.Thread.Sleep(1500);

            var member = new FamilyMember()
            {
                IsActive = false,
                FirstName = "Johny",
                LastName = "Braffo"
            };
            proxy.AddMemberToFamily(2, member);
        } 
        #endregion
    }
}
