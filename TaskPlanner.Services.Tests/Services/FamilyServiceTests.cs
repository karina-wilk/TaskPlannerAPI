using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TaskPlanner.Data.Models;
using TaskPlanner.Data.Repositories;
using TaskPlanner.Services.Services;
using TaskPlanner.Shared.Contracts;
using System.Linq;
using System.Collections.Generic;

namespace TaskPlanner.Services.Tests.Services
{
    [TestClass]
    public class FamilyServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetNotActiveMembersStoredProcedure_FamilyId_Negative_ThrowsException()
        {
            //Arrange.
            var familyMock = new Mock<IFamilyService>(MockBehavior.Default);

            familyMock.Setup(x => x.GetNotActiveMembersStoredProcedure(-1)).Throws(new ArgumentException());

            //Act.
            familyMock.Object.GetNotActiveMembersStoredProcedure(-1);

            //Assert - by method's atribute.
        }

        [TestMethod]
        public void AddMemberToFamily_Calls_AddMember_When_FamilyId_Valid()
        {
            //Arrange.
            var mockRepository = new Mock<IFamilyRepository>();

            mockRepository.Setup(c => c.AddMember(It.IsAny<int>(), It.IsAny<FamilyMember>()));

            var familyService = new FamilyService(mockRepository.Object);

            //Act.
            familyService.AddMemberToFamily(1, new FamilyMember());

            //Assert.
            mockRepository.VerifyAll();
        }

        [TestMethod]
        public void AddMemberToFamily_Calls_Repository_Method_AddMember_With_Apropriete_Parameters()
        {
            //Arrange.
            var mockRepository = new Mock<IFamilyRepository>();

            mockRepository.Setup(c => c.AddMember(It.IsAny<int>(), It.IsAny<FamilyMember>()));

            var familyService = new FamilyService(mockRepository.Object);

            var memberToAdd = new FamilyMember();

            //Act.
            familyService.AddMemberToFamily(1, memberToAdd);

            //Assert.
            mockRepository.Verify(x => x.AddMember(
                It.Is<int>(fn => fn.Equals(1)), It.Is<FamilyMember>(fn => fn.Equals(memberToAdd)))
            );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddMemberToFamily_Throws_Exception_When_FamilyId_Is_Negative()
        {
            //Arrange.
            var mockRepository = new Mock<IFamilyRepository>();

            mockRepository.Setup(c => c.AddMember(-1, It.IsAny<FamilyMember>()));//.Throws(new ArgumentException());

            var familyService = new FamilyService(mockRepository.Object);

            //Act.
            familyService.AddMemberToFamily(-1, new FamilyMember { FirstName="KK", LastName= "aa", IsActive=true } );

            //Assert.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddMemberToFamily_Throws_Exception_When_FamilyMember_Is_Null()
        {
            //Arrange.
            var mockRepository = new Mock<IFamilyRepository>();

            mockRepository.Setup(c => c.AddMember(-1, It.IsAny<FamilyMember>()));//.Throws(new ArgumentException());

            var familyService = new FamilyService(mockRepository.Object);

            //Act.
            familyService.AddMemberToFamily(1, null);

            //Assert.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddMemberToFamily_Throws_Exception_When_FamilyMember_FirstName_Longer_Than_32()
        {
            //Arrange.
            var moqRepo = new Mock<IFamilyRepository>();

            moqRepo.Setup(c => c.AddMember(It.IsAny<int>(), It.IsAny<FamilyMember>()));

            var service = new FamilyService(moqRepo.Object);

            //Act.
            service.AddMemberToFamily(1, new FamilyMember { FirstName = "12345678901234567890_12345678901234567890" });
        }

        [TestMethod]
        public void GetAll_Returns_Only_Active_When_Set_To_Do_So()
        {
            //Arrange.
            List<Family> items = new List<Family>()
            {
                new Family{Id = 1, IsActive=true, Name = "Wilks" },
                new Family{Id = 2, IsActive=false, Name = "Novacs" },
                new Family{Id = 3, IsActive=true, Name = "Kovalcyk" }
            };

            var mockRepo = new Mock<IFamilyRepository>();
            mockRepo.Setup(c => c.GetAll()).Returns(items.AsQueryable());

            var sevice = new FamilyService(mockRepo.Object);

            //Act.
            var result = sevice.GetAll(true);

            //Assert.
            var activeCount = result.Where(c => c.IsActive).Count();

            Assert.AreEqual(activeCount, result.Count());
        }

        [TestMethod]
        public void GetAll_Returns_All_When_Set_To_Do_So()
        {
            //Arrange.
            List<Family> items = new List<Family>()
            {
                new Family{Id = 1, IsActive=true, Name = "Wilks" },
                new Family{Id = 2, IsActive=false, Name = "Novacs" },
                new Family{Id = 3, IsActive=true, Name = "Kovalcyk" }
            };

            var mockRepo = new Mock<IFamilyRepository>();
            mockRepo.Setup(c => c.GetAll()).Returns(items.AsQueryable());

            var sevice = new FamilyService(mockRepo.Object);

            //Act.
            var result = sevice.GetAll(false);

            //Assert.
            Assert.AreEqual(result.Count(), items.Count);
        }

        [TestMethod]
        public void Add_Family_Is_SaveOrUpdate_Called()
        {
            //Arrange.
            var mockRepo = new Mock<IFamilyRepository>();

            mockRepo.Setup(c => c.SaveOrUpdate(It.IsAny<Family>()));

            var service = new FamilyService(mockRepo.Object);

            //Act.
            service.Add(new Family());

            //Assert.
            mockRepo.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_Family_ThrowException_When_Family_Is_Null()
        {
            //Arrange.
            var mockRepo = new Mock<IFamilyRepository>();

            mockRepo.Setup(c => c.SaveOrUpdate(It.IsAny<Family>()));

            var service = new FamilyService(mockRepo.Object);

            //Act.
            service.Add(null);
        }
    }
}
