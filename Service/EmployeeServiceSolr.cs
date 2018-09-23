using System.Collections.Generic;
using System.Threading.Tasks;

using Model.Interface;
using Repository.Interface;
using Service.Interface;

namespace Service
{
    public class EmployeeServiceSolr : IEmployeeServiceSolr<IEmployee>
    {
        #region Fields

        private readonly IEmployeeRepositorySolr<IEmployee> _repository;
        #endregion

        #region Constructor

        public EmployeeServiceSolr(IEmployeeRepositorySolr<IEmployee> repository) => _repository = repository;
        #endregion

        #region Method

        async Task<IEnumerable<IEmployee>> IEmployeeServiceSolr<IEmployee>.GetEmployee() => await _repository.GetEmployee();

        IEnumerable<IEmployee> IEmployeeServiceSolr<IEmployee>.GetEmployeeByName(string name) => _repository.GetEmployeeByName(name);        
        #endregion
    }
}
