using Microsoft.Extensions.Logging;
using Service.Interface;

using Moq;

using Model;
using Model.Interface;
using NUnit.Framework;
using AdminPortal.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace EmployeeTest
{
    [TestFixture]
    public class EmployeeControlTest
    {
        IEmpoyeeServiceSql<IEmployee> _mockServiceSql;
        IEmployeeServiceSolr<IEmployee> _mockServiceSolr;      
        ILogger<EmployeeController> _mockLogger;
        EmployeeController _employeeController;

        [SetUp]
        public void Setup()
        {
            _mockServiceSql = new Mock<IEmpoyeeServiceSql<IEmployee>>().Object;
            _mockServiceSolr = new Mock<IEmployeeServiceSolr<IEmployee>>().Object;
            _mockLogger = new Mock<ILogger<EmployeeController>>().Object;
            _employeeController = new EmployeeController(_mockServiceSolr, _mockServiceSql, _mockLogger);
        }

        #region Get Test

        [Test]
        public void GetEmployee500ErrorCantCastCorrectly()
        {
            //Arrange
            var expected = StatusCodes.Status500InternalServerError;
            var mockSolr = new Mock<IEmployeeServiceSolr<IEmployee>>();
            var employeeList = new List<IEmployee>
            {
                new Employee{EmployeeId = 12, FullName = "John Snow", DateOfJoining = DateTime.Now, ManagerId = 1}
            };
            mockSolr.Setup(m => m.GetEmployee()).Returns(Task.FromResult((IEnumerable<IEmployee>)employeeList));
            var employeeController = new EmployeeController(mockSolr.Object, _mockServiceSql, _mockLogger);
            //Act
            var actionResult = employeeController.Get().Result as StatusCodeResult;
            //Assert
            Assert.AreEqual(expected, actionResult.StatusCode);
        }

        [Test]
        public void GetEmployee200Response()
        {
            //Arrange        
            var mockSolr = new Mock<IEmployeeServiceSolr<IEmployee>>();
            var employeeList = new List<Employee>
            {
                new Employee{EmployeeId = 12, FullName = "John Snow", DateOfJoining = DateTime.Now, ManagerId = 1}
            };
            mockSolr.Setup(m => m.GetEmployee()).Returns(Task.FromResult((IEnumerable<IEmployee>)employeeList));
            var employeeController = new EmployeeController(mockSolr.Object, _mockServiceSql, _mockLogger);
            //Act
            var actionResult = employeeController.Get().Result;
            //Assert
            Assert.IsInstanceOf(typeof(OkObjectResult), actionResult);
        }
        #endregion

        #region Post Test

        [Test]
        public void PostCreateNullCheck()
        {
            //Arrange         
            var expected = false;
            //Act    
            var actionResult = _employeeController.Post(null);
            //Assert
            Assert.AreEqual(expected, actionResult);
        }

        [Test]
        public void PostCreateEmptyValueCheck()
        {
            //Arrange         
            var expected = false;
            //Act
            var actionResult = _employeeController.Post(new Employee());
            //Assert
            Assert.AreEqual(expected, actionResult);
        }
        #endregion
    }
}
