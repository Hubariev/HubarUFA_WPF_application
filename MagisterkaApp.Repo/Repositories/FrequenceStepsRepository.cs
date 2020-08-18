using MagisterkaApp.Domain;
using MagisterkaApp.Repo.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MagisterkaApp.Repo.Repositories
{
    public class FrequenceStepsRepository: IFrequenceStepLiteDbContext
    {
        private IFrequenceStepLiteDbContext frequenceStepContext;

        public FrequenceStepsRepository(IFrequenceStepLiteDbContext db)
        {
            this.frequenceStepContext = db;
        }

        public Task<List<FrequencyStep>> GetFrequencyStepsByMeasureId(Guid measureId)
        {
            return this.frequenceStepContext.GetFrequencyStepsByMeasureId(measureId);
        }
    }
}
