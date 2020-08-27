using GalaSoft.MvvmLight;
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
        public FrequencyStep frequencyStepInfo { get; set; } = new FrequencyStep() {Frequency = 80, PowerLevelResult =50 };



        public ObservableCollection<FrequencyStep> FrequencySteps { get; set; }

        public FrequenceStepsViewModel(Measure measure, IFrequenceStepsRepository frequenceStepsRepository)
        {
            this.frequenceStepsRepository = frequenceStepsRepository;
            this.FrequencySteps = new ObservableCollection<FrequencyStep>();
            GetData();
            this.measure = measure;

            var readFrequencies = GetFrequencyStepsMonitoring(this.monitoringPathes, measure.Id);
            readFrequencies = GetFrequencyStepsCalibrating(this.calibrationPathes, readFrequencies, measure.Id);
            this.FrequencySteps = CalculateFrequencySteps(readFrequencies, measure.FieldStrength);

            SelectionChangedCommand = new RelayCommand<FrequencyStep>(SelectionChanged);
        }

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
                RaisePropertyChanged("SelectedFrequencyStep");
            }
        }

        public FrequencyStep FrequencyStepInfo
        {
            get { return frequencyStepInfo; }
            set
            {
                frequencyStepInfo = value;
                RaisePropertyChanged("FrequencyStepInfo");
            }
        }

        public ObservableCollection<FrequencyStep> GetFrequencyStepsMonitoring(List<string> pathMeasuredPoints, Guid measureId)
        {
            var readFrequencySteps = new ObservableCollection<FrequencyStep>();
            var frequenceSteps = this.frequenceStepsRepository.GetFrequencyStepsByMeasureId(measureId);

            if (frequenceSteps.Status == System.Threading.Tasks.TaskStatus.Faulted)
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
            if (frequenceSteps.Status == System.Threading.Tasks.TaskStatus.Faulted)
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
                        FrequencyStepInfo.Notification = SelectedFrequencyStep.Notification;
                        FrequencyStepInfo.IsHidden = SelectedFrequencyStep.IsHidden;
                    }                 
                    ));

        #endregion

        public ObservableCollection<FrequencyStep> CalculateFrequencySteps(ObservableCollection<FrequencyStep> frequencySteps, double fieldstrength)
        {
            var calucatedFrequencySteps = CalculateResult.GetResult(frequencySteps, fieldstrength);
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
