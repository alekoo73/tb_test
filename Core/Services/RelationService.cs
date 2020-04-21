using Core.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class RelationService : IRelationService
    {
        private readonly IUnitOfWork _uow;
        public readonly IAppLogger<RelationService> _appLogger;

        public RelationService(IUnitOfWork uow, IAppLogger<RelationService> appLogger)
        {
            this._uow = uow;
            this._appLogger = appLogger;
        }
        public async Task AddRelation(Relation relation)
        {
            await _uow.Repository<Relation>().AddAsync(relation);
        }

        public async Task<bool> DeleteRelation(int id, int id2)
        {
           var record=await _uow.Repository<Relation>().FindAsync(w=>(w.RelatedPerson1Id==id && w.RelatedPerson2Id==id2) || (w.RelatedPerson2Id == id && w.RelatedPerson1Id == id2));
            if (record==null)
            {
                return false;
            }
            await _uow.Repository<Relation>().DeleteAsync(record);
            await _uow.Commit();
            return true;
        }

    }
}
