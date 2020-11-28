using GalaSoft.MvvmLight.Command;
using MagisterkaApp.Calculator;
using MagisterkaApp.Domain;
using MagisterkaApp.Repo.Abstractions;
using MagisterkaApp.UI.Miscellaneous;
using MagisterkaApp.UI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
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

        public Boolean isCheckedDeviationSmaller { get; set; }
        public Boolean isCheckedDeviationSmaller75Proc { get; set; }
        public Boolean isCheckedDeviationBetween { get; set; }
        public Boolean isCheckedDeviationBigger { get; set; }


        public FrequenceStepsViewModel(Measure measure, IFrequenceStepsRepository frequenceStepsRepository,
                                       List<string> monitoringPaathes, List<string> calibraationPathes)
        {
            this.frequenceStepsRepository = frequenceStepsRepository;
            this.FrequencySteps = new ObservableCollection<FrequencyStep>();
            this.measure = measure;

            this.FrequencySteps = GetFrequencySteps(measure, monitoringPaathes, calibraationPathes);

            if(this.FrequencySteps != null)
            {
            
                this.FiltredFrequencySteps = new ObservableCollection<FrequencyStep>(this.FrequencySteps);
                SelectionChangedCommand = new RelayCommand<FrequencyStep>(SelectionChanged);
                Check5proc();
            }       
        }

        public void Check5proc()
        {
            var countOfOrange = Math.Round(0.05 * this.FrequencySteps.Count);

            var counter = 0;

            for (int t = 0; t < this.FrequencySteps.Count; t++)
            {
                if (this.FrequencySteps[t].DeviationNotification.backgroundColor.ToString() == "#FFFFA500")
                {
                    counter++;
                }
            }

            if (counter > countOfOrange)
            {   
                this.Result5proc = $"Warunek jednorodności pola NIE jest spełniony. Dopuszczalna liczba kroków częstotliwości z warunkiem " +
                    $"od 6 [dB] do 10 [dB] jest do: {countOfOrange} kroków. Faktyczna liczba kroków: {counter}";
            }
            else
            { 
                this.Result5proc = $"Warunek jednorodności pola JEST spełniony. Dopuszczalna liczba kroków częstotliwości z warunkiem " +
                 $"od 6[dB] do 10[dB] jest do: {countOfOrange} kroków. Faktyczna liczba kroków: {counter}";
            }
              
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
                       if (step.DeviationNotification.backgroundColor.ToString() == "#FFFFA500" && counter > 0)
                       {
                           filtredFrequencySteps.Add(step);
                           counter--;
                       }
                       else if (step.DeviationNotification.backgroundColor.ToString() != "#FFFFA500" &&
                               step.DeviationNotification.backgroundColor.ToString() != "#FFFF0000")
                       {
                           filtredFrequencySteps.Add(step);
                       }
                   }

                   var saveWindow = new System.Windows.Forms.SaveFileDialog();

                   saveWindow.Title = "Wybierz folder do zapisania pliku.";
                   saveWindow.FileName = measure.NameOfMeasure;
                   saveWindow.DefaultExt = ".DAT";
                   saveWindow.Filter = "DAT files (*.DAT)|*.dat";

                   if (saveWindow.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                   {
                       string savePath = Path.GetDirectoryName(saveWindow.FileName);
                       var savePathTest = savePath + $"\\{measure.NameOfMeasure}Test.txt";
                       savePath = savePath + $"\\{measure.NameOfMeasure}.DAT";
                    

                       SaveResult.WriteResult(filtredFrequencySteps, savePath, measure);
                       SaveResult.WriteTestValues(new List<FrequencyStep>(this.FrequencySteps), savePathTest);

                       MessageBoxResult result = MessageBox.Show($"{measure.NameOfMeasure}.DAT został zapisany.",
                                              "Confirmation",
                                              MessageBoxButton.OK,
                                              MessageBoxImage.Information);
                   }

               })
       );


        private void SelectionChanged(FrequencyStep selectedFrequencyStep)
        {
            FrequencyStepInfo = selectedFrequencyStep;
        }


        #region checkBoxes
        public Boolean IsCheckedDeviationSmaller
        {
            get { return isCheckedDeviationSmaller; }
            set
            {
                isCheckedDeviationSmaller = value;
                OnPropertyChanged("IsCheckedDeviationSmaller");
            }
        }

        public Boolean IsCheckedDeviationSmaller75Proc
        {
            get { return isCheckedDeviationSmaller75Proc; }
            set
            {
                isCheckedDeviationSmaller75Proc = value;
                OnPropertyChanged("IsCheckedDeviationSmaller75Proc");
            }
        }

        public Boolean IsCheckedDeviationBetween
        {
            get { return isCheckedDeviationBetween; }
            set
            {
                isCheckedDeviationBetween = value;
                OnPropertyChanged("IsCheckedDeviationBetween");
            }
        }

        public Boolean IsCheckedDeviationBigger
        {
            get { return isCheckedDeviationBigger; }
            set
            {
                isCheckedDeviationBigger = value;
                OnPropertyChanged("IsCheckedDeviationBigger");
            }
        }

        #endregion

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

        public ObservableCollection<FrequencyStep> GetFrequencySteps(Measure measure,
               List<string> monitoringPaathes, List<string> calibraationPathes)
        {
            if (this.frequenceStepsRepository.CheckExistenceOfFrequencyStep(measure.Id).Result)
            {
                return new ObservableCollection<FrequencyStep>
                    (this.frequenceStepsRepository.GetFrequencyStepsByMeasureId(measure.Id).Result);
            }
            else
            {
                if(monitoringPaathes == null || calibraationPathes == null)
                {
                    MessageBoxResult result = MessageBox.Show($"Kalibracyjny i monitorujący pliki nie istnieją.",
                                          "Confirmation",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Error);
                    
                    return null;

                }
                else
                {
                    var readFrequencies = GetFrequencyStepsMonitoring(monitoringPaathes, measure.Id);
                    readFrequencies = GetFrequencyStepsCalibrating(calibraationPathes, readFrequencies, measure.Id);
                    return CalculateFrequencySteps(readFrequencies, measure.ResearchfieldStrength, measure.VerificationfieldStrength);
                }
               

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
            }
            else
            {
                readFrequencySteps = frequencySteps;
            }


            return readFrequencySteps;
        }


        public Boolean infoChecked;

        #region commands
        public RelayCommand checkBoxChangedCommand;
        public ICommand CheckBoxChangedCommand =>
            checkBoxChangedCommand ??
            (checkBoxChangedCommand =
                new RelayCommand(
                    () =>
                    {

                        this.FiltredFrequencySteps.Clear();
                        this.FiltredFrequencySteps.AddRange(
                            this.FrequencySteps.GetFiltredFrequencySteps(IsCheckedDeviationSmaller,
                                                                         IsCheckedDeviationSmaller75Proc,
                                                                         IsCheckedDeviationBetween,
                                                                         IsCheckedDeviationBigger));


                    }
                    ));


        public RelayCommand<FrequencyStep> SelectionChangedCommand { get; set; }

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
