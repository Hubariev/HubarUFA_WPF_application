using LiteDB;
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

            LiteDatabase = new LiteDatabase("");
        }
        public async Task<List<FrequencyStep>> GetFrequencyStepsByMeasureId(Guid MeasureId)
        {
            return this.LiteDatabase.GetCollection<FrequencyStep>("FrequencyStep").FindAll().Where(x => x.MeasureId == MeasureId).ToList();
        }
    }
}
