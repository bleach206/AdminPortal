using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

using Model;
using Model.Interface;
using Service.Interface;

namespace AdminPortal.Controllers
{
    [Produces("application/json")]
    [Route("api/Employee")]
    public class EmployeeController : Controller
    {
        #region Fields

        private readonly IEmployeeServiceSolr<IEmployee> _servicesSolr;
        private readonly IEmpoyeeServiceSql<IEmployee> _servicesSql;
        private readonly ILogger _logger;
        #endregion

        #region Constructor

        public EmployeeController(IEmployeeServiceSolr<IEmployee> serviceSolr, IEmpoyeeServiceSql<IEmployee> serviceSql, ILogger<EmployeeController> logger)
        {
            _servicesSolr = serviceSolr;
            _servicesSql = serviceSql;
            _logger = logger;
        }
        #endregion

        #region Methods
        // GET: api/Employee
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            try
            {
                var empoyeeList = _servicesSolr.GetEmployee();
                return (IEnumerable<Employee>)empoyeeList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get All Employee Error");
            }
            return null;
        }

        // GET: api/Employee/5
        [HttpGet("{name}", Name = "Name")]
        public IEnumerable<Employee> Get(string name)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    var empoyeeList = _servicesSolr.GetEmployeeByName(name);
                    return (IEnumerable<Employee>)empoyeeList;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get employ by name error");
            }
            return null;
        }

        // POST: api/Employee
        [HttpPost]
        public bool Post([FromBody]Employee employee)
        {
            try
            {
                if (employee != null && 0 < employee.ManagerId && !string.IsNullOrWhiteSpace(employee.FullName))
                {
                    return _servicesSql.CreateEmployee(employee);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error create eployee");
            }
            return false;
        }

        #endregion
    }
}