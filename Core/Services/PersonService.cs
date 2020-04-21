using Core.Classes;
using Core.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Core.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _uow;
        public readonly IAppLogger<PersonService> _appLogger;

        public PersonService(IUnitOfWork uow,IAppLogger<PersonService> appLogger)
        {
            this._uow = uow;
            this._appLogger = appLogger;
        }

        public async Task AddPerson(Person person)
        {
            await _uow.Repository<IPersonRepository,Person>().AddAsync(person);

        }

        public async Task<bool> DeletePerson(int personid)
        {
            var repo = _uow.Repository<IPersonRepository, Person>();
            var person=repo.GetByUniqueIdAsync(personid).Result;
            if (person == null)
                return false;
            await repo.DeleteAsync(person);
            return true;
        }

    

        public async Task UpdatePerson(Person person)
        {
            await _uow.Repository<IPersonRepository, Person>().UpdateAsync(person);
            await _uow.Commit();
        }

        public async Task UpdatePicture(string filename,int id)
        {
            await ((IPersonRepository)_uow.Repository<IPersonRepository, Person>()).UpdatePicture(filename,id);
            await _uow.Commit();
        }

        public async Task<Person> GetPersonAsync(int personid)
        {
             return await ((IPersonRepository)_uow.Repository<IPersonRepository, Person>()).GetPerson(personid);
        }

        public async Task<bool> FindDuplicate(string idnumber)
        {
            _appLogger.LogInformation($"searching for {idnumber}");
            return await ((IPersonRepository)_uow.Repository<IPersonRepository, Person>()).FindDuplicate(idnumber);
        }

        public async Task<bool> FindDuplicate(string idnumber, int id)
        {
            _appLogger.LogInformation($"searching for {idnumber} {id}");
            return await ((IPersonRepository)_uow.Repository<IPersonRepository, Person>()).FindDuplicate(idnumber,id);
        }

        public async Task<(IEnumerable<Person> records,int cnt)> GetRecordsAsync(QueryParameters queryParameters)
        {
            ICollection<Person> records = null;
            IPersonRepository repo = ((IPersonRepository)_uow.Repository<IPersonRepository, Person>());
            switch (queryParameters)
            {
                case QueryParameters q when q.HasQuery():
                    records=await repo.FindAllAsync(queryParameters, f => f.FirstName.Contains(queryParameters.Query) || f.LastName.Contains(queryParameters.Query) || f.IdNumber.Contains(queryParameters.Query));
                    break;
                case QueryParameters q when q.IsDynamic():
                    records= await repo.FindDynamicAsync(queryParameters);
                    break;
                default:
                    records= await repo.GetAllAsync(queryParameters);
                    break;
            }

            return (records.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                .Take(queryParameters.PageCount),records.Count);
        }


        public dynamic GetReportData()
        {
           return ((IPersonRepository)_uow.Repository<IPersonRepository, Person>()).GetReportData();
        }

    }
}
