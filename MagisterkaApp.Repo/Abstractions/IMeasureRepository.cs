using MagisterkaApp.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagisterkaApp.Repo.Abstractions
{
    public interface IMeasureRepository
    {
        Task<List<Measure>> GetAllAsync();
        Task AddMeasure(Measure entity);
        Task Update(Measure entity);
        Task DeleteMeasure(Guid type);
        Task<Measure> GetById(Guid type);
        Task<bool> CheckIsMeasureNameExist(string name);
    }
}
