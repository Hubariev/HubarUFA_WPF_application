using LiteDB;
using MagisterkaApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagisterkaApp.Repo.Database
{
    public class MeasureLiteDbContext : IMeasureLiteDbContext
    {
        public LiteDatabase LiteDatabase { get; set; }

        public MeasureLiteDbContext()
        {
            LiteDatabase = new LiteDatabase(@"F:\_My_ProgaLibrary\Own-Projects\WPF_UniformityFieldApp\measureDatabase.db");
        }

        public async Task<List<Measure>> GetMeasures()
        {
            return this.LiteDatabase.GetCollection<Measure>("Measure").FindAll().ToList();
        }

        public async Task AddMeasure(Measure measure)
        {
            this.LiteDatabase.GetCollection<Measure>("Measure").Insert(measure);
        }

        public async Task DeleteMeasure(Guid id)
        {
            this.LiteDatabase.GetCollection<Measure>("Measure").Delete(id);
        }

        public async Task<Measure> GetMeasureById(Guid id)
        {
            return this.LiteDatabase.GetCollection<Measure>("Measure").Find(x => x.Id == id).FirstOrDefault();
        }

        public async Task UpdateMeasure(Measure measure)
        {
            this.LiteDatabase.GetCollection<Measure>("Measure").Update(measure);
        }
    }
}
