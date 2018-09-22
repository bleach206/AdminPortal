using Microsoft.Extensions.Logging;
using Service.Interface;

using Moq;

using Model;
using Model.Interface;
using NUnit.Framework;
using AdminPortal.Controllers;

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
    }
}
