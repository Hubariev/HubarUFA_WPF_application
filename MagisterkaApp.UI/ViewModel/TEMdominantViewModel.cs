using GalaSoft.MvvmLight.Command;
using MagisterkaApp.Domain;
using MagisterkaApp.Domain.Enums;
using MagisterkaApp.UI.Miscellaneous;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MagisterkaApp.UI.ViewModel
{
    public class TEMdominantViewModel: ViewModelBase
    {
        public ObservableCollection<FrequencyStep> FrequencySteps { get; set; }
        public ObservableCollection<FrequencyStep> FiltredFrequencySteps { get; set; }//will be on View
        public FrequencyStep selectedFrequencyStep { get; set; }
        public FrequencyStep frequencyStepInfo { get; set; } = new FrequencyStep();
        public string Result5proc { get; set; }

        public Boolean isCheckedTEMiationSmaller { get; set; }
        public Boolean isCheckedTEMiationSmaller75Proc { get; set; }
        public Boolean isCheckedTEMiationBetween { get; set; }
        public Boolean isCheckedTEMiationBigger { get; set; }

        
       
        //command
        public RelayCommand<FrequencyStep> SelectionChangedCommand { get; set; }


        public TEMdominantViewModel(ObservableCollection<FrequencyStep> frequencySteps)
        {
            this.FrequencySteps = frequencySteps;
            this.FiltredFrequencySteps = new ObservableCollection<FrequencyStep>(this.FrequencySteps);
            SelectionChangedCommand = new RelayCommand<FrequencyStep>(SelectionChanged);
            Check5proc();
        }

        private void SelectionChanged(FrequencyStep selectedFrequencyStep)
        {
            FrequencyStepInfo = selectedFrequencyStep;
        }

        public void Check5proc()
        {
            var countOfOrange = Math.Round(0.05 * this.FrequencySteps.Count) ;

            var counter = 0;

            for (int t = 0; t < this.FrequencySteps.Count; t++)
            {
                if(this.FrequencySteps[t].TEMNotification.backgroundColor.ToString() == "#FFFF8C00")
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

        public FrequencyStep FrequencyStepInfo
        {
            get { return frequencyStepInfo; }
            set
            {
                frequencyStepInfo = value;
                OnPropertyChanged("FrequencyStepInfo");
            }
        }

        #region checkBoxes
        public Boolean IsCheckedTEMiationSmaller
        {
            get { return isCheckedTEMiationSmaller; }
            set
            {
                isCheckedTEMiationSmaller = value;
                OnPropertyChanged("IsCheckedTEMiationSmaller");
            }
        }

        public Boolean IsCheckedTEMiationSmaller75Proc
        {
            get { return isCheckedTEMiationSmaller75Proc; }
            set
            {
                isCheckedTEMiationSmaller75Proc = value;
                OnPropertyChanged("IsCheckedTEMiationSmaller75Proc");
            }
        }

        public Boolean IsCheckedTEMiationBetween
        {
            get { return isCheckedTEMiationBetween; }
            set
            {
                isCheckedTEMiationBetween = value;
                OnPropertyChanged("IsCheckedTEMiationBetween");
            }
        }

        public Boolean IsCheckedTEMiationBigger
        {
            get { return isCheckedTEMiationBigger; }
            set
            {
                isCheckedTEMiationBigger = value;
                OnPropertyChanged("IsCheckedTEMiationBigger");
            }
        }
        #endregion

        public RelayCommand checkBoxChangedCommand;
        public ICommand CheckBoxChangedCommand =>
            checkBoxChangedCommand ??
            (checkBoxChangedCommand =
                new RelayCommand(
                    () =>
                    {

                        this.FiltredFrequencySteps.Clear();
                        this.FiltredFrequencySteps.AddRange(
                            this.FrequencySteps.GetFiltredTEMFrequencySteps(IsCheckedTEMiationSmaller,
                                                                         IsCheckedTEMiationSmaller75Proc,
                                                                         IsCheckedTEMiationBetween,
                                                                         IsCheckedTEMiationBigger));


                    }
                    ));
    }
}
