using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CompanyProjectRepository : ICompanyProjects
    {
        public void DeleteCustomer(CompanyProject Customer) => CompanyProjectDAO.DeleteCustomer(Customer);

        public int FindCustomerMaxId() => CompanyProjectDAO.FindCustomerMaxId();


        public List<CompanyProject> GetCompanyProject() => CompanyProjectDAO.GetCompanyProject();

        public CompanyProject GetCompanyProjectID(int ID) => CompanyProjectDAO.GetCompanyProjectID(ID);

      


        public void SaveCustomer(CompanyProject CompanyProjects) => CompanyProjectDAO.SaveCustomer(CompanyProjects);

        public List<CompanyProject> SearchCompanyProject(string companyProject) => CompanyProjectDAO.SearchCompanyProject(companyProject);


        public void UpdateCustomer(CompanyProject Customer) => CompanyProjectDAO.UpdateCustomer(Customer);

    }
}
