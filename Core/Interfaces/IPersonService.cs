using Core.Classes;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPersonService
    {
        public Task<bool> UpdatePerson(Person person);
        public Task<bool> DeletePerson(int personid);
        public Task<bool> FindDuplicate(string idnumber);
        public Task<bool> FindDuplicate(string idnumber,int id);
        public Task AddPerson(Person person);
        public Task<Person> GetPersonAsync(int personid);
        public Task<(IEnumerable<Person> records, int cnt)> GetRecordsAsync(QueryParameters queryParameters);
        public Task UpdatePicture(string filename, int id);
        public dynamic GetReportData();
    }
}
