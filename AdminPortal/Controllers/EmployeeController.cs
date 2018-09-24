using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

using Model;
using Model.Interface;
using Service.Interface;
using System.Threading.Tasks;

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

        public EmployeeController(IEmployeeServiceSolr<IEmployee> serviceSolr, IEmpoyeeServiceSql<IEmployee> serviceSql, ILogger<EmployeeController> logger) => (_servicesSolr, _servicesSql, _logger) = (serviceSolr, serviceSql, logger);
        #endregion

        #region Methods
        // GET: api/Employee
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Task<IEnumerable<Employee>>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var empoyeeList = await _servicesSolr.GetEmployee();
                return Ok((IEnumerable<Employee>)empoyeeList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get All Employee Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/Employee/phillip
        [HttpGet("{name}", Name = "Name")]
        [ProducesResponseType(200, Type = typeof(Task<IEnumerable<Employee>>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get(string name)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    var empoyeeList = await _servicesSolr.GetEmployeeByName(name);

                    return Ok((IEnumerable<Employee>)empoyeeList);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get employ by name error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
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