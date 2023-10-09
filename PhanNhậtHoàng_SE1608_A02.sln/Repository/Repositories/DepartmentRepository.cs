using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class DepartmentRepository : IDepartment
    {
        public void DeleteCustomer(Departmennt Customer)=> DepartmentDAO.DeleteCustomer(Customer);


        public List<Departmennt> GetDepartmennt() => DepartmentDAO.GetDepartmennt();


        public Departmennt GetDepartmenntID(int ID) => DepartmentDAO.GetDepartmenntID(ID);


        public void SaveCustomer(Departmennt Departmennts) => DepartmentDAO.SaveCustomer(Departmennts);


        public List<Departmennt> SearchDepartmennt(string Departmennt) => DepartmentDAO.SearchDepartmennt(Departmennt);


        public void UpdateCustomer(Departmennt Customer) => DepartmentDAO.UpdateCustomer(Customer);

    }
}
