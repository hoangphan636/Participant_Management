using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IDepartment
    {
        List<Departmennt> GetDepartmennt();
        List<Departmennt> SearchDepartmennt(string Departmennt);
        Departmennt GetDepartmenntID(int ID);
        void SaveCustomer(Departmennt Departmennts);
        void UpdateCustomer(Departmennt Customer);
        void DeleteCustomer(Departmennt Customer);





    }
}
