using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{   
    public interface IPersonRepository : IGenericRepository<Person>
    {
        Task<bool> FindDuplicate(string idnumber);
        Task<bool> FindDuplicate(string idnumber, int id);
        Task<int> UpdatePicture(string filename, int id);
        Task<Person> GetPerson(int id);
        dynamic GetReportData();

    }
}
