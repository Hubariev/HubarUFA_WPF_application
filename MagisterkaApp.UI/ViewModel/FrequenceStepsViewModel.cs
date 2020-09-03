using GalaSoft.MvvmLight.Command;
using MagisterkaApp.Calculator;
using MagisterkaApp.Domain;
using MagisterkaApp.Repo.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MagisterkaApp.UI.ViewModel
{
    public class FrequenceStepsViewModel : ViewModelBase
    {
        private readonly IFrequenceStepsRepository frequenceStepsRepository;

        List<string> monitoringPathes = new List<string>();
        List<string> calibrationPathes = new List<string>();
        public Measure measure { get; set; }

        public FrequencyStep selectedFrequencyStep { get; set; } 
        public FrequencyStep frequencyStepInfo { get; set; } = new FrequencyStep();

        public string Result5proc { get; set; }



        public ObservableCollection<FrequencyStep> FrequencySteps { get; set; }// will be going to TEmdom
        public ObservableCollection<FrequencyStep> FiltredFrequencySteps { get; set; }//will be on View

        public FrequenceStepsViewModel(Measure measure, IFrequenceStepsRepository frequenceStepsRepository)
        {
            this.frequenceStepsRepository = frequenceStepsRepository;
            this.FrequencySteps = new ObservableCollection<FrequencyStep>();
            GetData();
            this.measure = measure;

            var readFrequencies = GetFrequencyStepsMonitoring(this.monitoringPathes, measure.Id);
            readFrequencies = GetFrequencyStepsCalibrating(this.calibrationPathes, readFrequencies, measure.Id);
            this.FrequencySteps = CalculateFrequencySteps(readFrequencies, measure.ResearchfieldStrength, measure.VerificationfieldStrength);

            SelectionChangedCommand = new RelayCommand<FrequencyStep>(SelectionChanged); 
            Check5proc();
        }


        public void Check5proc()
        {
            var countOfOrange = Math.Round(0.05 * this.FrequencySteps.Count);

            var counter = 0;

            for (int t = 0; t < this.FrequencySteps.Count; t++)
            {
                if (this.FrequencySteps[t].DeviationNotification.backgroundColor.ToString() == "#FFFF8C00")
                {
                    counter++;
                }
            }

            if (counter > countOfOrange)
                this.Result5proc = $"Warunek TEM dominant NIE jest spełniony. Dopuszczalna liczba kroków częstotliwości z warunkiem " +
                    $"-6[dB] do -2[dB]: {countOfOrange}. Faktyczna liczba: {counter}";
            else
                this.Result5proc = $"Warunek TEM dominant JEST spełniony. Dopuszczalna liczba kroków częstotliwości z warunkiem " +
                 $"-6[dB] do -2[dB]: {countOfOrange}. Faktyczna liczba: {counter}";
        }

        private RelayCommand saveResultCommand;
        public ICommand SaveResultCommand =>
           saveResultCommand ??
           (saveResultCommand = new RelayCommand(
               () =>
               {
                   var filtredFrequencySteps = new List<FrequencyStep>();

                   double counter = this.FrequencySteps.Count * 0.05;
                   foreach (var step in this.FrequencySteps)
                   {
                       if(step.DeviationNotification.backgroundColor.ToString() == "#FFFF8C00" && counter > 0)
                       {
                           filtredFrequencySteps.Add(step);
                           counter--;
                       }
                       else if(step.DeviationNotification.backgroundColor.ToString() != "#FFFF8C00" && 
                               step.DeviationNotification.backgroundColor.ToString() != "#FFFF0000")
                       {
                           filtredFrequencySteps.Add(step);
                       }
                   }

                   SaveResult.WriteResult(filtredFrequencySteps, measure.NameOfMeasure,
                       measure.ResearchfieldStrength, nameof(measure.HSeptum));
               })
       );


        private void SelectionChanged(FrequencyStep selectedFrequencyStep)
        {
            FrequencyStepInfo = selectedFrequencyStep;
        }

        public FrequencyStep SelectedFrequencyStep
        {
            get { return selectedFrequencyStep; }
            set
            {
                selectedFrequencyStep = value;
                OnPropertyChanged("SelectedFrequencyStep");
            }
        }

        public FrequencyStep FrequencyStepInfo
        {
            get { return frequencyStepInfo; }
            set
            {
                frequencyStepInfo = value;
                OnPropertyChanged("FrequencyStepInfo");
            }
        }

        public ObservableCollection<FrequencyStep> GetFrequencyStepsMonitoring(List<string> pathMeasuredPoints, Guid measureId)
        {
            var readFrequencySteps = new ObservableCollection<FrequencyStep>();
            var frequenceSteps = this.frequenceStepsRepository.GetFrequencyStepsByMeasureId(measureId);

            if (frequenceSteps.Status == System.Threading.Tasks.TaskStatus.Faulted || frequenceSteps.Result.Count == 0)
            {
                readFrequencySteps = ReadFile.ReadMonitoringFile(pathMeasuredPoints, measureId);
            }
            else
            {
                readFrequencySteps = new ObservableCollection<FrequencyStep>(frequenceSteps.Result);
            }

            return readFrequencySteps;
        }

        public ObservableCollection<FrequencyStep> GetFrequencyStepsCalibrating(List<string> pathMeasuredPoints, ObservableCollection<FrequencyStep> frequencySteps, 
            Guid measureId)
        {
            var readFrequencySteps = new ObservableCollection<FrequencyStep>();
            var frequenceSteps = this.frequenceStepsRepository.GetFrequencyStepsByMeasureId(measureId);
            if (frequenceSteps.Status == System.Threading.Tasks.TaskStatus.Faulted || frequenceSteps.Result.Count == 0)
            {
                readFrequencySteps = ReadFile.ReadCalibrationFile(pathMeasuredPoints, frequencySteps);
                this.frequenceStepsRepository.AddFrequencySteps(new List<FrequencyStep>(readFrequencySteps));
            }
            else
            {
                readFrequencySteps = frequencySteps;
            }
            

            return readFrequencySteps;
        }

        public RelayCommand<FrequencyStep> SelectionChangedCommand { get; set; }



        #region commands
        public RelayCommand stepBeenSelectedCommand;
        public ICommand StepBeenSelectedCommand =>
            stepBeenSelectedCommand ??
            (stepBeenSelectedCommand =
                new RelayCommand(
                    () =>
                    {
                        FrequencyStepInfo.MeasureId = SelectedFrequencyStep.MeasureId;
                        FrequencyStepInfo.Frequency = SelectedFrequencyStep.Frequency;
                        FrequencyStepInfo.PowerLevelResult = SelectedFrequencyStep.PowerLevelResult;
                        FrequencyStepInfo.Points = SelectedFrequencyStep.Points;
                        FrequencyStepInfo.DeviationNotification = SelectedFrequencyStep.DeviationNotification;
                        FrequencyStepInfo.IsHidden = SelectedFrequencyStep.IsHidden;
                    }                 
                    ));

        private RelayCommand temDominantOpenCommand;
        public ICommand TEMdominantOpenCommand =>
            temDominantOpenCommand ??
            (temDominantOpenCommand = new RelayCommand(
                () =>
                {
                    var catOpts = new Views.TEMdominantWindow(this.FrequencySteps);
                    catOpts.ShowDialog();
                }
                ));

        #endregion

        public ObservableCollection<FrequencyStep> CalculateFrequencySteps(ObservableCollection<FrequencyStep> frequencySteps, double researchfieldStrength,
            double verificationfieldStrength)
        {
            var calucatedFrequencySteps = CalculateResult.GetResult(frequencySteps, researchfieldStrength, verificationfieldStrength);
            this.frequenceStepsRepository.AddFrequencySteps(new List<FrequencyStep>(calucatedFrequencySteps));
            return calucatedFrequencySteps;
        }

        public void GetData()
        {
            string monFile1 = $"F:/A_STUDIA/MAGISTERKA/3_semestr/Diplomka_magisterka/Pomiary/Monitoring_GTEM5317_2020_08_17/Punkt_1.MSD";
            string monFile2 = $"F:/A_STUDIA/MAGISTERKA/3_semestr/Diplomka_magisterka/Pomiary/Monitoring_GTEM5317_2020_08_17/Punkt_2.MSD";
            string monFile3 = $"F:/A_STUDIA/MAGISTERKA/3_semestr/Diplomka_magisterka/Pomiary/Monitoring_GTEM5317_2020_08_17/Punkt_3.MSD";
            string monFile4 = $"F:/A_STUDIA/MAGISTERKA/3_semestr/Diplomka_magisterka/Pomiary/Monitoring_GTEM5317_2020_08_17/Punkt_4.MSD";
            string monFile5 = $"F:/A_STUDIA/MAGISTERKA/3_semestr/Diplomka_magisterka/Pomiary/Monitoring_GTEM5317_2020_08_17/Punkt_5.MSD";
            string monFile6 = $"F:/A_STUDIA/MAGISTERKA/3_semestr/Diplomka_magisterka/Pomiary/Monitoring_GTEM5317_2020_08_17/Punkt_6.MSD";

            string calFile1 = $"F:/A_STUDIA/MAGISTERKA/3_semestr/Diplomka_magisterka/Pomiary/Punkt_1_2020_08_17.DAT";
            string calFile2 = $"F:/A_STUDIA/MAGISTERKA/3_semestr/Diplomka_magisterka/Pomiary/Punkt_2_2020_08_17.DAT";
            string calFile3 = $"F:/A_STUDIA/MAGISTERKA/3_semestr/Diplomka_magisterka/Pomiary/Punkt_3_2020_08_18.DAT";
            string calFile4 = $"F:/A_STUDIA/MAGISTERKA/3_semestr/Diplomka_magisterka/Pomiary/Punkt_4_2020_08_18.DAT";
            string calFile5 = $"F:/A_STUDIA/MAGISTERKA/3_semestr/Diplomka_magisterka/Pomiary/Punkt_5_2020_08_18.DAT";
            string calFile6 = $"F:/A_STUDIA/MAGISTERKA/3_semestr/Diplomka_magisterka/Pomiary/Punkt_6_2020_08_18.DAT";

            monitoringPathes.Add(monFile1);
            monitoringPathes.Add(monFile2);
            monitoringPathes.Add(monFile3);
            monitoringPathes.Add(monFile4);
            monitoringPathes.Add(monFile5);
            monitoringPathes.Add(monFile6);

            calibrationPathes.Add(calFile1);
            calibrationPathes.Add(calFile2);
            calibrationPathes.Add(calFile3);
            calibrationPathes.Add(calFile4);
            calibrationPathes.Add(calFile5);
            calibrationPathes.Add(calFile6);

        }
    }
}
