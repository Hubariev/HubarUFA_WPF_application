using LiteDB;
using MagisterkaApp.Domain;
using MagisterkaApp.Repo.Abstractions;
using MagisterkaApp.Repo.Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagisterkaApp.Repo.Repositories
{
    public class MeasureRepository : IRepository<Measure, Guid>
    {
        private IMeasureLiteDbContext measureContext;

        public MeasureRepository(IMeasureLiteDbContext db)
        {
            this.measureContext = db;
        }
        public Task Add(Measure entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid type)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Measure>> GetAllAsync()
        {
            return await this.measureContext.GetMeasures();
        }

        public Task<Measure> GetById(Guid type)
        {
            throw new NotImplementedException();
        }

        public Task Update(Measure entity)
        {
            throw new NotImplementedException();
        }
    }
}
