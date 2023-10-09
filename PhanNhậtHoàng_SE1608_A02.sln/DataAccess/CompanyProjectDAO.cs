using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CompanyProjectDAO
    {
        public static List<CompanyProject> GetCompanyProject()
        {
            var list = new List<CompanyProject>();
            try
            {
                using (var context = new DataEmployeeDBcontext())
                {
                    list = context.CompanyProjects.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static int FindCustomerMaxId()
        {
            try
            {
                using (var context = new DataEmployeeDBcontext())
                {
                    int maxId = context.CompanyProjects.Max(customer => customer.CompanyProjectID.Value);
                    return maxId;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<CompanyProject> CheckParticipatingProjectid(string Email)
        {
            var Com = new List<CompanyProject>();
            var list = new ParticipatingProject();
            var emp = new Employee();
            try
            {
                using (var context = new DataEmployeeDBcontext())
                {

                    emp = context.Employees.FirstOrDefault(x => x.EmailAddress == Email);
                    list = context.ParticipatingProjects.FirstOrDefault(x => x.EmployeeID == emp.EmployeeID);
                    Com =context.CompanyProjects.Where(x => x.CompanyProjectID == list.CompanyProjectID).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Com;
        }

        public static List<CompanyProject> SearchCompanyProject(string companyProject)
        {
            var list = new List<CompanyProject>();
            try
            {
                using (var context = new DataEmployeeDBcontext())
                {
                    list = context.CompanyProjects.Where(x => x.ProjectDescription.Contains(companyProject)).ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }


        public static CompanyProject GetCompanyProjectID(int ID)
        {
            var list = new CompanyProject();
            try
            {
                using (var context = new DataEmployeeDBcontext())
                {
                    list = context.CompanyProjects.FirstOrDefault(x=> x.CompanyProjectID== ID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        public static void SaveCustomer(CompanyProject CompanyProjects)
        {

            try
            {
                using var context = new DataEmployeeDBcontext();

                context.CompanyProjects.Add(CompanyProjects);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateCustomer(CompanyProject Customer)
        {

            try
            {
                using var context = new DataEmployeeDBcontext();

                context.CompanyProjects.Update(Customer);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public static void DeleteCustomer(CompanyProject Customer)
        {

            try
            {
                using var context = new DataEmployeeDBcontext();

                context.CompanyProjects.Remove(Customer);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }








    }
}
