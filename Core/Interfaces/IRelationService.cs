using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRelationService
    {
    
        public Task AddRelation(Relation relation);
        public Task<bool> DeleteRelation(int id, int id2);
    
    }
}
