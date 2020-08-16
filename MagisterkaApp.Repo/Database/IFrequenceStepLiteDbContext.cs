using MagisterkaApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MagisterkaApp.Repo.Database
{
    public interface IFrequenceStepLiteDbContext
    {
        Task<List<FrequencyStep>> GetFrequencyStepsByMeasureId(Guid MeasureId);
    }
}
