using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IEmployee
    {
        List<Employee> CheckEmployeeEmail(string Employee);
        bool CheckEmail(string email);
        Employee checkCustomerLogin(string email, string password);
        List<Employee> GetEmployee();
        bool CheckEmailForUpdate(string email, int id);
        List<Employee> SearchEmployee(string Employee);
     Employee GetEmployeeID(int ID);
        Employee checkAdminLogin(string email, string password);
        void SaveCustomer(Employee Employees);
        void UpdateCustomer(Employee Customer);
        void DeleteCustomer(Employee Customer);
    }
}
