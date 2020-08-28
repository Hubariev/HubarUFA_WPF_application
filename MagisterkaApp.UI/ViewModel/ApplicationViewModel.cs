using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MagisterkaApp.Domain;
using MagisterkaApp.Domain.Enums;
using MagisterkaApp.Repo.Abstractions;
using MagisterkaApp.UI.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MagisterkaApp.UI.ViewModel
{
    public class ApplicationViewModel : ViewModelBase
    {
        private readonly IMeasureRepository measureRepository;
        private readonly IFrequenceStepsRepository frequenceStepsRepository;
        public ObservableCollection<Measure> Measures { get; set; }
        public Measure selectedMeasure { get; set; }

        private Measure newMeasure;
        public MeasureDto MeasureDto { get; set; } = new MeasureDto();



        public ApplicationViewModel(IMeasureRepository measureRepository, IFrequenceStepsRepository frequenceStepsRepository)
        {
            this.measureRepository = measureRepository;
            this.frequenceStepsRepository = frequenceStepsRepository;
            Measures = new ObservableCollection<Measure>();
            GetMeasures();

            SelectionChangedMeasureCommand = new RelayCommand<Measure>(SelectionMeasureChanged);
        }



        private async Task GetMeasures()
        {
            var measures = await this.measureRepository.GetAllAsync();
            this.Measures = new ObservableCollection<Measure>(measures);
        }

        //protected override void RegisterCollections()
        //{
        //    Measures = new ObservableCollection<Measure>();
        //}


        #region Commands

        public RelayCommand<Measure> SelectionChangedMeasureCommand { get; set; }
        private void SelectionMeasureChanged(Measure measure)
        {
            if (measure != null)
            {
                MeasureDto = new MeasureDto()
                {
                    NameOfMeasure = measure.NameOfMeasure,
                    NameOfOperator = measure.NameOfOperator,
                    FieldStrength = measure.FieldStrength.ToString(),
                    HSeptum = measure.HSeptum
                };
            }
            else
                throw new Exception();
        }

        private RelayCommand addMeasureCommand;
        public ICommand AddMeasureCommand =>
            addMeasureCommand ??
            (addMeasureCommand = new RelayCommand(
                () =>
                {
                    var measure = new Measure(
                        MeasureDto.NameOfMeasure,
                        MeasureDto.NameOfOperator,
                        Convert.ToDouble(MeasureDto.FieldStrength),
                        MeasureDto.HSeptum);

                    this.measureRepository.AddMeasure(measure);
                    this.Measures.Add(measure);
                })
        );

        private RelayCommand<Measure> deleteMeasureCommand;
        public ICommand DeleteMeasureCommand =>
            deleteMeasureCommand ??
            (deleteMeasureCommand = new RelayCommand<Measure>(
                (measure) =>
                {
                    this.measureRepository.DeleteMeasure(measure.Id);
                    this.Measures.Remove(measure);
                    this.frequenceStepsRepository.DeleteByMeasureId(measure.Id);
                }));

        private RelayCommand frequencyStepsOpenCommand;
        public ICommand FrequencyStepsOpenCommand =>
            frequencyStepsOpenCommand ??
            (frequencyStepsOpenCommand = new RelayCommand(
                () =>
                {
                    var catOpts = new Views.FrequenceStepsWindow(selectedMeasure, this.frequenceStepsRepository);
                    catOpts.ShowDialog();
                }
                ));
        #endregion

        public Measure NewMeasure
        {
            get { return newMeasure; }
            set
            {
                newMeasure = value;
                OnPropertyChanged("NewMeasure");
            }
        }

        public Measure SelectedMeasure
        {
            get { return selectedMeasure; }
            set
            {
                selectedMeasure = value;
                OnPropertyChanged("SelectedMeasure");
            }
        }
    }
    public class MeasureDto
    {
        public string NameOfMeasure { get; set; }
        public string NameOfOperator { get; set; }
        public string FieldStrength { get; set; }
        public TypeOfGTEM HSeptum { get; set; }
    }
}