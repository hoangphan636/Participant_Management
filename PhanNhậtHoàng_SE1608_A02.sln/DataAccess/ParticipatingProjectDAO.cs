using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ParticipatingProjectDAO
    {

        public static List<ParticipatingProject> GetParticipatingProject()
        {
            var list = new List<ParticipatingProject>();
            try
            {
                using (var context = new DataEmployeeDBcontext())
                {
                    list = context.ParticipatingProjects.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static List<ParticipatingProject> SearchParticipatingProject(DateTime searchDate)
        {
            var list = new List<ParticipatingProject>();
            try
            {
                using (var context = new DataEmployeeDBcontext())
                {
                    list = context.ParticipatingProjects.Where(x => x.StartDate <= searchDate && searchDate <= x.EndDate).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        public static ParticipatingProject GetParticipatingProjectID(int ID)
        {
            var list = new ParticipatingProject();
            try
            {
                using (var context = new DataEmployeeDBcontext())
                {
                    list = context.ParticipatingProjects.FirstOrDefault(x => x.CompanyProjectID == ID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static List<ParticipatingProject> CheckParticipatingProjectid(string Email)
        {
            var list = new List<ParticipatingProject>();
            var emp = new Employee();
            try
            {
                using (var context = new DataEmployeeDBcontext())
                {
                  
                    emp = context.Employees.FirstOrDefault(x => x.EmailAddress == Email);
                    list = context.ParticipatingProjects.Where(x => x.EmployeeID == emp.EmployeeID).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static void SaveCustomer(ParticipatingProject ParticipatingProjects)
        {

            try
            {
                using var context = new DataEmployeeDBcontext();
               
                context.ParticipatingProjects.Add(ParticipatingProjects);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateCustomer(ParticipatingProject Customer)
        {

            try
            {
                using var context = new DataEmployeeDBcontext();

                context.ParticipatingProjects.Update(Customer);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public static void DeleteCustomer(ParticipatingProject Customer)
        {

            try
            {
                using var context = new DataEmployeeDBcontext();

                context.ParticipatingProjects.Remove(Customer);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }






    }
}
