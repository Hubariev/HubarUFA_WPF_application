using GalaSoft.MvvmLight.Command;
using MagisterkaApp.Domain;
using MagisterkaApp.Domain.Enums;
using System;
using System.Collections.ObjectModel;

namespace MagisterkaApp.UI.ViewModel
{
    public class TEMdominantViewModel: ViewModelBase
    {
        public ObservableCollection<FrequencyStep> FrequencySteps { get; set; }
        public FrequencyStep selectedFrequencyStep { get; set; }
        public FrequencyStep frequencyStepInfo { get; set; } = new FrequencyStep();
        public string Result5proc { get; set; }

        //command
        public RelayCommand<FrequencyStep> SelectionChangedCommand { get; set; }


        public TEMdominantViewModel(ObservableCollection<FrequencyStep> frequencySteps)
        {
            this.FrequencySteps =frequencySteps;
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
    }
}
