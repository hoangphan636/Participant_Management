using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICompanyProjects
    {
        List<CompanyProject> GetCompanyProject();
     
        CompanyProject GetCompanyProjectID(int ID);
        void SaveCustomer(CompanyProject CompanyProjects);
        void UpdateCustomer(CompanyProject Customer);
        void DeleteCustomer(CompanyProject Customer);
        List<CompanyProject> SearchCompanyProject(string companyProject);
        int FindCustomerMaxId();
    }
}
