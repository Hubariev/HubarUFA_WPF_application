
using MagisterkaApp.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MagisterkaApp.Repo.Abstractions
{
    public interface IFrequenceStepsRepository
    {
        Task<List<FrequencyStep>> GetFrequencyStepsByMeasureId(Guid MeasureId);

        Task AddFrequencySteps(List<FrequencyStep> frequencySteps);

        Task DeleteByMeasureId(Guid MeasureId);
    }
}
