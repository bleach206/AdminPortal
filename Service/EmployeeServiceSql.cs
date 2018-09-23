using Model.Interface;
using Repository.Interface;
using Service.Interface;

namespace Service
{
    public class EmployeeServiceSql : IEmpoyeeServiceSql<IEmployee>
    {
        #region MyRegion

        private readonly IEmployeeRepositorySql<IEmployee> _repository;
        #endregion

        #region Constructor

        public EmployeeServiceSql(IEmployeeRepositorySql<IEmployee> repository) => _repository = repository;
        #endregion

        #region Methods

        public bool CreateEmployee(IEmployee employee) => _repository.CreateEmployee(employee);        
        #endregion
    }
}
