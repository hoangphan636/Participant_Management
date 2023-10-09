using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ParticipatingProjectRepository : IParticipatingProject
    {
        public List<ParticipatingProject> CheckParticipatingProjectid(string Email) => ParticipatingProjectDAO.CheckParticipatingProjectid(Email);


        public void DeleteCustomer(ParticipatingProject Customer) => ParticipatingProjectDAO.DeleteCustomer(Customer);



        public List<ParticipatingProject> GetParticipatingProject() => ParticipatingProjectDAO.GetParticipatingProject();

        public ParticipatingProject GetParticipatingProjectID(int ID) => ParticipatingProjectDAO.GetParticipatingProjectID(ID);


        public void SaveCustomer(ParticipatingProject ParticipatingProjects) => ParticipatingProjectDAO.SaveCustomer(ParticipatingProjects);


        public List<ParticipatingProject> SearchParticipatingProject(DateTime searchDate) => ParticipatingProjectDAO.SearchParticipatingProject(searchDate);


        public void UpdateCustomer(ParticipatingProject Customer) => ParticipatingProjectDAO.UpdateCustomer(Customer);

    }
}
