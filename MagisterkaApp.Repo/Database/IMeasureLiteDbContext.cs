using MagisterkaApp.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagisterkaApp.Repo.Database
{
    public interface IMeasureLiteDbContext
    {
        Task<List<Measure>> GetMeasures();
        Task AddMeasure(Measure measure);
        Task UpdateMeasure(Measure measure);
        Task<Measure> GetMeasureById(Guid id);
        Task DeleteMeasure(Guid id);
    }
}
