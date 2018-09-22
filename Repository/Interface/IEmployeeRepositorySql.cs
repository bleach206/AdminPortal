using Model.Interface;

namespace Repository.Interface
{
    public interface IEmployeeRepositorySql<T> where T : IEmployee
    {
        bool CreateEmployee(IEmployee employee);
    }
}
