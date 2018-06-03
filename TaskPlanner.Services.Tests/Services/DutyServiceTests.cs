using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TaskPlanner.Data.Models;
using TaskPlanner.Data.Repositories;
using TaskPlanner.Services.Services;
using TaskPlanner.Shared.Contracts;
using TaskPlanner.Shared.DTO;

namespace TaskPlanner.Services.Tests.Services
{
    [TestClass]
    public class DutyServiceTests
    {
        List<Duty> duties = new List<Duty>
        {
            new Duty() { Id = 1, StartDate = new DateTime(2018, 1, 12), Enabled=true, FamilyMember = new FamilyMember { Id=555} },
            new Duty() { Id = 2, StartDate = new DateTime(2018, 1, 13), Enabled=true, FamilyMember = new FamilyMember { Id=554} },
            new Duty() { Id = 3, StartDate = new DateTime(2018, 1, 14), Enabled=true, FamilyMember = new FamilyMember { Id=555} }
        };

        [TestMethod]
        public void GetForDate_Returns_Duties()
        {
            //Arrange
           
            var dateToCheck = new DateTime(2018, 1, 13);
            int familyMemberId = 554;
            var dutyRepo = new Mock<IDutyRepository>();
          
            dutyRepo.Setup(c => c.Get(It.IsAny<Expression<Func<Duty, bool>>>()))
                .Returns((Expression<Func<Duty, bool>> expression) =>
                {
                    var data = duties.Where(expression.Compile()).AsQueryable();
                    return data;
                });

            var service = new DutyService(dutyRepo.Object);
            
            //Act
            var result = service.GetForDate(familyMemberId, dateToCheck);

            //Assert.
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Id, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetForDate_ThrowsException_When_FamilyMemberId_Is_Not_Positive_Number()
        {
            //Arrange
            var dutyRepo = new Mock<IDutyRepository>();

            dutyRepo.Setup(c => c.Get(It.IsAny<Expression<Func<Duty, bool>>>()))
                .Returns((Expression<Func<Duty, bool>> expression) =>
                {
                    var data = duties.Where(expression.Compile()).AsQueryable();
                    return data;
                });

            var service = new DutyService(dutyRepo.Object);

            //Act
            var result = service.GetForDate(-2, DateTime.Now);

            //Assert.
        }

        [TestMethod]
        public void OperatorEqualEqualVerification()
        {
            Expression<Func<Duty, bool>> expr1 = p => p.Title == "Test";
            Expression<Func<Duty, bool>> expr2 = p => 
            p.Title == "Test";
            Assert.IsTrue(expr1.ToString() == expr2.ToString());
            Assert.IsFalse(expr1.Equals(expr2));
            Assert.IsFalse(expr1 == expr2);
            Assert.IsFalse(expr1.Body == expr2.Body);
            Assert.IsFalse(expr1.Body.Equals(expr2.Body));
        }
    }
}
