using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IParticipatingProject
    {

        List<ParticipatingProject> GetParticipatingProject();
        List<ParticipatingProject> SearchParticipatingProject(DateTime searchDate);
        void SaveCustomer(ParticipatingProject ParticipatingProjects);
void UpdateCustomer(ParticipatingProject Customer);
        void DeleteCustomer(ParticipatingProject Customer);

        ParticipatingProject GetParticipatingProjectID(int ID);

        List<ParticipatingProject> CheckParticipatingProjectid(string Email);





    }
}
