using System;

namespace Model.Interface
{
    public interface IEmployee
    {
        int EmployeeId { get; set; }
        string FullName { get; set; }
        int ManagerId { get; set; }        
        DateTime DateOfJoining { get; set; }
    }
}
