using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DepartmentDAO
    {
        public static List<Departmennt> GetDepartmennt()
        {
            var list = new List<Departmennt>();
            try
            {
                using (var context = new DataEmployeeDBcontext())
                {
                    list = context.Departments.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static List<Departmennt> SearchDepartmennt(string Departmennt)
        {
            var list = new List<Departmennt>();
            try
            {
                using (var context = new DataEmployeeDBcontext())
                {
                    list = context.Departments.Where(x => x.DepartmentDescription.Contains(Departmennt) || x.DepartmentName.Contains(Departmennt)).ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }


        public static Departmennt GetDepartmenntID(int ID)
        {
            var list = new Departmennt();
            try
            {
                using (var context = new DataEmployeeDBcontext())
                {
                    list = context.Departments.FirstOrDefault(x => x.DepartmentID == ID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

     




        public static void SaveCustomer(Departmennt Departmennts)
        {

            try
            {
                using var context = new DataEmployeeDBcontext();

                context.Departments.Add(Departmennts);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateCustomer(Departmennt Customer)
        {

            try
            {
                using var context = new DataEmployeeDBcontext();

                context.Departments.Update(Customer);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public static void DeleteCustomer(Departmennt Customer)
        {

            try
            {
                using var context = new DataEmployeeDBcontext();

                context.Departments.Remove(Customer);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }









    }
}
