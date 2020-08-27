﻿using LiteDB;
using MagisterkaApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisterkaApp.Repo.Database
{
    public class FrequenceStepLiteDbContext : IFrequenceStepLiteDbContext
    {
        public LiteDatabase LiteDatabase { get; set; }

        public FrequenceStepLiteDbContext()
        {
            LiteDatabase = new LiteDatabase(@"F:\_My_ProgaLibrary\Own-Projects\WPF_UniformityFieldApp\frequenceStepsDatabase.db");
        }
        public async Task<List<FrequencyStep>> GetFrequencyStepsByMeasureId(Guid MeasureId)
        {
            return this.LiteDatabase.GetCollection<FrequencyStep>("FrequencyStep").FindAll().Where(x => x.MeasureId == MeasureId).ToList();
        }

        public async Task AddFrequencySteps(List<FrequencyStep> frequencySteps)
        {
            this.LiteDatabase.GetCollection<FrequencyStep>("FrequencyStep").InsertBulk(frequencySteps);
        }

        public async Task DeleteByMeasureId(Guid MeasureId)
        {
            this.LiteDatabase.GetCollection<FrequencyStep>("FrequencyStep").DeleteMany(x => x.MeasureId == MeasureId);
        }
    }
}
