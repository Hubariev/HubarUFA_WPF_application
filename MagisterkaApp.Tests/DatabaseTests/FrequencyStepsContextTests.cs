using MagisterkaApp.Domain;
using MagisterkaApp.Repo.Database;
using System;
using System.Collections.Generic;
using Xunit;

namespace MagisterkaApp.Tests.DatabaseTests
{
    public class FrequencyStepsContextTests
    {
        [Fact]
        public void GetAllSteps()
        {
            var database = new FrequenceStepLiteDbContext();

            Guid id = new Guid("8d19d983-2a3f-4be8-a8c0-a263ca7533a6");

            var steps = database.GetAllSteps();
            System.Console.WriteLine();
        }

        [Fact]
        public void CheckMethodOfAddingSteps()
        {
            var measureId = Guid.NewGuid();

            var frequencyStep1 = new FrequencyStep(1,measureId, 80);
            var frequencyStep2 = new FrequencyStep(1,measureId, 90);

            List<FrequencyStep> frequencySteps = new List<FrequencyStep>()
            {
                frequencyStep1, frequencyStep2
            };

            var database = new FrequenceStepLiteDbContext();

            var steps = database.GetAllSteps().Result;


            database.AddFrequencySteps(frequencySteps);

            var log = database.LiteDatabase.CheckpointSize;
            


            var steps1 = database.GetAllSteps().Result;

            database.AddFrequencySteps(frequencySteps);

            var steps11 = database.GetAllSteps().Result;

            //var concreteStep = database.GetFrequencyStepsByMeasureId(measureId);
            Console.WriteLine();

        }


        [Fact]
        public void GetAllFrequencySteps()
        {
            var measureId = new Guid("17fca9a0-beb8-44a6-b200-c30eaa8e6bc1");

            var database = new FrequenceStepLiteDbContext();


            var steps = database.GetAllSteps().Result;

            Console.WriteLine();

        }
    }
}
