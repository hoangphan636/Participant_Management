using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EmployeeDAO
    {

        public static List<Employee> GetEmployee()
        {
            var list = new List<Employee>();
            try
            {
                using (var context = new DataEmployeeDBcontext())
                {
                    list = context.Employees.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static List<Employee> SearchEmployee(string Employee)
        {
            var list = new List<Employee>();
            try
            {
                using (var context = new DataEmployeeDBcontext())
                {
                    list = context.Employees.Where(x => x.FullName.Contains(Employee) || x.Skills.Contains(Employee) || x.Telephone.Contains(Employee) || x.Address.Contains(Employee) || x.EmailAddress.Contains(Employee) ).ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        public static List<Employee> CheckEmployeeEmail(string Employee)
        {
            var list = new List<Employee>();
            try
            {
                using (var context = new DataEmployeeDBcontext())
                {
                    list = context.Employees.Where(x => x.EmailAddress==Employee).ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        public static bool CheckEmailForUpdate(string email, int id)
        {
            var c = new DataEmployeeDBcontext();
           var adminss = GetEmployee();
            var mail = new Employee();
            var another = new Employee();
            foreach (var item in adminss)
            {
                 mail = c.Employees.FirstOrDefault(c => c.EmailAddress == email && c.EmployeeID ==id);
                 another = c.Employees.FirstOrDefault(c => c.EmailAddress == email && c.EmployeeID != id);
            }
            Employee admins = c.getDefaultAccounts();
            if (email != admins.EmailAddress && mail != null && another == null)
            {
                return true;
            }
            return false;
        }

        public static Employee GetEmployeeID(int ID)
        {
            var list = new Employee();
            try
            {
                using (var context = new DataEmployeeDBcontext())
                {
                    list = context.Employees.FirstOrDefault(x => x.EmployeeID == ID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static Employee checkAdminLogin(string email, string password)
        {
            var c = new DataEmployeeDBcontext();
            Employee admin = c.getDefaultAccounts();
            if (email == admin.EmailAddress && password == admin.Password)
            {
                return admin;
            }
            return null;
        }

        public static Employee checkCustomerLogin(string email, string password)
        {
            var c = new DataEmployeeDBcontext();
            Employee admin = c.Employees.FirstOrDefault(c=> c.EmailAddress == email && c.Password == password);
           
                return admin;
          
        }

        public static bool CheckEmail(string email)
        {
            var c = new DataEmployeeDBcontext();
            Employee admin = c.Employees.FirstOrDefault(c => c.EmailAddress == email);
            Employee admins = c.getDefaultAccounts();
            if (email != admins.EmailAddress && admin == null)
            {
                return true;
            }
            return false;
        }


        public static void SaveCustomer(Employee Employees)
        {

            try
            {
                using var context = new DataEmployeeDBcontext();

                context.Employees.Add(Employees);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateCustomer(Employee Customer)
        {

            try
            {
                using var context = new DataEmployeeDBcontext();

                context.Employees.Update(Customer);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public static void DeleteCustomer(Employee Customer)
        {

            try
            {
                using var context = new DataEmployeeDBcontext();

                context.Employees.Remove(Customer);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }












    }
}
