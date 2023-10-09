using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class EmployeeRepository : IEmployee
    {
        public Employee checkAdminLogin(string email, string password) => EmployeeDAO.checkAdminLogin(email, password);

        public Employee checkCustomerLogin(string email, string password) => EmployeeDAO.checkCustomerLogin(email, password);


        public bool CheckEmail(string email) => EmployeeDAO.CheckEmail(email);

        public bool CheckEmailForUpdate(string email, int id) => EmployeeDAO.CheckEmailForUpdate(email, id);

        public List<Employee> CheckEmployeeEmail(string Employee) => EmployeeDAO.CheckEmployeeEmail(Employee);
     

        public void DeleteCustomer(Employee Customer) => EmployeeDAO.DeleteCustomer(Customer);
     

        public List<Employee> GetEmployee() => EmployeeDAO.GetEmployee();


     public Employee GetEmployeeID(int ID) => EmployeeDAO.GetEmployeeID(ID);


        public void SaveCustomer(Employee Employees) => EmployeeDAO.SaveCustomer(Employees);


        public List<Employee> SearchEmployee(string Employee) => EmployeeDAO.SearchEmployee(Employee);


        public void UpdateCustomer(Employee Customer) => EmployeeDAO.UpdateCustomer(Customer);

    }
}
