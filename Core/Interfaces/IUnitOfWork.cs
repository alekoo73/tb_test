using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        public Dictionary<Type, object> Repositories { get; set; }
        public Task<int> Commit();
        public void Rollback();
        public IGenericRepository<T> Repository<TRepo, T>() where T : class where TRepo : IGenericRepository<T>;
        public IGenericRepository<T> Repository<T>() where T : class;


    }
}
