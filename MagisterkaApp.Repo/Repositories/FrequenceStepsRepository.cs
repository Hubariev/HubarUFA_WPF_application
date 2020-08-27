using MagisterkaApp.Domain;
using MagisterkaApp.Repo.Abstractions;
using MagisterkaApp.Repo.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MagisterkaApp.Repo.Repositories
{
    public class FrequenceStepsRepository: IFrequenceStepsRepository
    {
        private IFrequenceStepLiteDbContext frequenceStepContext;

        public FrequenceStepsRepository(IFrequenceStepLiteDbContext db)
        {
            this.frequenceStepContext = db;
        }

        public async Task AddFrequencySteps(List<FrequencyStep> frequencySteps)
        {
            await this.frequenceStepContext.AddFrequencySteps(frequencySteps);
        }

        public async Task DeleteByMeasureId(Guid MeasureId)
        {
            await this.frequenceStepContext.DeleteByMeasureId(MeasureId);
        }

        public async Task<List<FrequencyStep>> GetFrequencyStepsByMeasureId(Guid measureId)
        {
            return await this.frequenceStepContext.GetFrequencyStepsByMeasureId(measureId);
        }

 
    }
}
