using MagisterkaApp.Calculator;
using MagisterkaApp.Domain;
using MagisterkaApp.Repo.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MagisterkaApp.UI.ViewModel
{
    public class FrequenceStepsViewModel: ViewModelBase
    {
        private readonly IFrequenceStepsRepository frequenceStepsRepository;
        public List<string> filePathes = new List<string>();
        public Measure measure { get; set; }

        string fileName1 = $"F:/A_STUDIA/MAGISTERKA/3 semestr/Diplomka_magisterka/Bochanek/Ready_points/pkt_1.MSD";
        string fileName2 = $"F:/A_STUDIA/MAGISTERKA/3 semestr/Diplomka_magisterka/Bochanek/Ready_points/pkt_2.MSD";
        string fileName3 = $"F:/A_STUDIA/MAGISTERKA/3 semestr/Diplomka_magisterka/Bochanek/Ready_points/pkt_3.MSD";
        string fileName4 = $"F:/A_STUDIA/MAGISTERKA/3 semestr/Diplomka_magisterka/Bochanek/Ready_points/pkt_4.MSD";
        string fileName5 = $"F:/A_STUDIA/MAGISTERKA/3 semestr/Diplomka_magisterka/Bochanek/Ready_points/pkt_5.MSD";

        
        public ObservableCollection<FrequencyStep> FrequencySteps { get; set; }

        public FrequenceStepsViewModel(Measure measure)
        {
            this.FrequencySteps = new ObservableCollection<FrequencyStep>();
            GetData();
            this.measure = measure;
            var readFrequencies = GetFrequencySteps(this.filePathes, measure.Id);
            this.FrequencySteps = CalculateFrequencySteps(readFrequencies, measure.FieldStrength);
        }

        public ObservableCollection<FrequencyStep> GetFrequencySteps(List<string> pathMeasuredPoints, Guid measureId)
        {
            return ReadFile.ReadMeasureSteps(pathMeasuredPoints, measureId);
        }

        public ObservableCollection<FrequencyStep> CalculateFrequencySteps(ObservableCollection<FrequencyStep> frequencySteps, double fieldstrength)
        {
            return CalculateResult.GetResult(frequencySteps, fieldstrength);
        }

        public void GetData()
        {
            filePathes.Add(fileName1);
            filePathes.Add(fileName2);
            filePathes.Add(fileName3);
            filePathes.Add(fileName4);
            filePathes.Add(fileName5);
        }
    }
  
}
