using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(Testapp2Context dbContext)
            : base(dbContext)
        {

        }

        public async Task<bool> FindDuplicate(string idnumber)
        {
            return await _context.Persons.AnyAsync(a => a.IdNumber == idnumber);
        }

        public async Task<bool> FindDuplicate(string idnumber, int id)
        {
            return await _context.Persons.AnyAsync(a => a.IdNumber == idnumber && a.Id != id);
        }

        public override void Delete(Person t)
        {
            _context.Phones.RemoveRange(_context.Phones.Where(p => p.PersonId == t.Id));
            _context.Relations.RemoveRange(_context.Relations.Where(p => p.RelatedPerson1Id == t.Id || p.RelatedPerson2Id == t.Id));
            _context.Persons.Remove(t);
            _unitOfWork.Commit();
        }

        public override async Task<int> DeleteAsync(Person t)
        {
            _context.Phones.RemoveRange(_context.Phones.Where(p => p.PersonId == t.Id));
            _context.Relations.RemoveRange(_context.Relations.Where(p => p.RelatedPerson1Id == t.Id || p.RelatedPerson2Id == t.Id));
            _context.Persons.Remove(t);
            return await _unitOfWork.Commit();
        }

        public async Task<int> UpdatePicture(string filename, int id)
        {
            _context.Persons.Single(s => s.Id == id).PictureAddress = filename;
            return await _unitOfWork.Commit();
        }


        public async Task<Person> GetPerson(int id)
        {
            var person = await _context.Persons.Include("Phones").SingleOrDefaultAsync(s => s.Id == id);
            person.Relations = _context.Relations.Where(w => w.RelatedPerson1Id == id || w.RelatedPerson2Id == id).AsEnumerable();
            return person;
        }


        public dynamic GetReportData()
        {
            var query =
       from p in _context.Persons
       from r in _context.Relations
       where (p.Id == r.RelatedPerson1Id || p.Id == r.RelatedPerson1Id)
       select new { Name = p.FirstName + " " + p.LastName, RelationType = r.RelationType };

            return query.ToList().GroupBy(e => e.Name).Select(d => new
            {
                Name = d.Key,
                Types = d.GroupBy(h => h.RelationType).Select(h
                => new
                {
                    RelationType = h.Key,
                    count = h.Count()
                }).ToList()
            }).ToList();
        }
    }
}
