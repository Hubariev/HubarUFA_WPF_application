using GalaSoft.MvvmLight.Command;
using MagisterkaApp.Domain;
using MagisterkaApp.Domain.Enums;
using MagisterkaApp.Domain.SeedWork;
using MagisterkaApp.Repo.Abstractions;
using MagisterkaApp.UI.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MagisterkaApp.UI.ViewModel
{
    public class ApplicationViewModel : ViewModelBase
    {
        private readonly IMeasureRepository measureRepository;
        private readonly IFrequenceStepsRepository frequenceStepsRepository;
        public ObservableCollection<Measure> Measures { get; set; }
        public ObservableCollection<Measure> FiltredMeasures { get; set; }
        public Measure selectedMeasure { get; set; }

        private Measure newMeasure;
        public MeasureDto measureDto { get; set; } = new MeasureDto();
        public FilterMeasureDto filterMeasureDto { get; set; } = new FilterMeasureDto();

        public FilePathForMeasure filePathForMeasure { get; set; } = new FilePathForMeasure()
        {
            MonitoringPath = "Dodaj punkty.",
            CalibrationPath = "Dodaj punkty."
        };

        PointsFilePathes pointsPathes = new PointsFilePathes();

        private Boolean iSAddFileEnabled = true;

        private List<string> monitoringPathes;
        private List<string> calibrationPathes;

        private List<string> monitoringPathesFiltred;
        private List<string> calibrationPathesFiltred;

        private int numberOfPoint = 0;

        public string monitoringFilePath = "";
        public string calibrationFilePath = "";

        public string filtredMonitoringFilePath = "";
        public string filtredCalibratingPath = "";

        private string pathForImage = "/Images/noimage.png";

        //--------------------------------------------------------------------------------------------//

        public ApplicationViewModel(IMeasureRepository measureRepository, IFrequenceStepsRepository frequenceStepsRepository)
        {
            this.measureRepository = measureRepository;
            this.frequenceStepsRepository = frequenceStepsRepository;
            Measures = new ObservableCollection<Measure>();
            FiltredMeasures = new ObservableCollection<Measure>();
            GetMeasures();


        }



        public List<string> MonitoringPathesFiltred
        {
            get { return monitoringPathesFiltred; }
            set
            {
                monitoringPathesFiltred = value;
                OnPropertyChanged("MonitoringPathesFiltred");
            }
        }

        public FilterMeasureDto FilterMeasureDto
        {
            get { return filterMeasureDto; }
            set
            {
                filterMeasureDto = value;
                OnPropertyChanged("FilterMeasureDto");
            }
        }

        public MeasureDto MeasureDto
        {
            get { return measureDto; }
            set
            {
                measureDto = value;
                OnPropertyChanged("MeasureDto");
            }
        }

        public List<string> CalibrationPathesFiltred
        {
            get { return calibrationPathesFiltred; }
            set
            {
                calibrationPathesFiltred = value;
                OnPropertyChanged("CalibrationPathesFiltred");
            }
        }

        public List<string> MonitoringPathes
        {
            get { return monitoringPathes; }
            set
            {
                monitoringPathes = value;
                OnPropertyChanged("MonitoringPathes");
            }
        }

        public List<string> CalibrationPathes
        {
            get { return calibrationPathes; }
            set
            {
                calibrationPathes = value;
                OnPropertyChanged("CalibrationPathes");
            }
        }

        public string PathForImage
        {
            get { return pathForImage; }
            set
            {
                pathForImage = value;
                OnPropertyChanged("PathForImage");
            }
        }

        public int NumberOfPoint
        {
            get { return numberOfPoint; }
            set
            {
                numberOfPoint = value;
                OnPropertyChanged("NumberOfPoint");
            }
        }

        public Boolean ISAddFileEnabled
        {
            get { return iSAddFileEnabled; }
            set
            {
                iSAddFileEnabled = value;
                OnPropertyChanged("ISAddFileEnabled");
            }
        }


        private async Task GetMeasures()
        {
            var measures = await this.measureRepository.GetAllAsync();
            this.Measures = new ObservableCollection<Measure>(measures);
            this.FiltredMeasures.AddRange(this.Measures);
        }

        public FilePathForMeasure FilePathForMeasure
        {
            get { return filePathForMeasure; }
            set
            {
                filePathForMeasure = value;
                OnPropertyChanged("FilePathForMeasure");
            }
        }


        #region Commands

        private RelayCommand addMeasureCommand;
        public ICommand AddMeasureCommand =>
            addMeasureCommand ??
            (addMeasureCommand = new RelayCommand(
                () =>
                {
                    var measure = new Measure(
                        MeasureDto.NameOfMeasure,
                        MeasureDto.NameOfOperator,
                        Convert.ToDouble(MeasureDto.ResearchfieldStrength),
                        Convert.ToDouble(MeasureDto.VerificationfieldStrength),
                        MeasureDto.HSeptum);

                    this.measureRepository.AddMeasure(measure);
                    this.Measures.Add(measure);
                    this.FiltredMeasures.Add(measure);


                    AutoClosingMessageBox.Show("Pomiar został dodany", "", 1200);


                    selectedMeasure = measure;
                })
        );

        private RelayCommand gTEMChangeCommand;

        public ICommand GTEMChangeCommand =>
            gTEMChangeCommand ??
            (gTEMChangeCommand = new RelayCommand(
                () =>
                {
                    PathForImage = MeasureDto.GetImagePath();
                    NumberOfPoint = Convert.ToInt32(MeasureExtensions.GetNumberOfPoints(MeasureDto.HSeptum));
                }
                )
            );

        private RelayCommand addFilePathCommand;
        public ICommand AddFilePathCommand =>
            addFilePathCommand ??
            (addFilePathCommand = new RelayCommand(
                () =>
                {
                    AutoClosingMessageBox.Show($"Wszystkie punkty pomiarowe zostali dodane.", "", 1200);
                })
        );

        private RelayCommand browseMonFileCommand;
        public ICommand BrowseMonFileCommand =>
            browseMonFileCommand ??
            (browseMonFileCommand = new RelayCommand(
                () =>
                {
                    if (MeasureDto.HSeptum == TypeOfGTEM.None)
                    {
                        MessageBoxResult result = MessageBox.Show($"Proszę wybrać rodzaj komory.",
                                                   "Niepoprawna ilość punktów",
                                                   MessageBoxButton.OK,
                                                   MessageBoxImage.Warning);
                    }
                    else
                    {
                        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                        dlg.Multiselect = true;
                        dlg.DefaultExt = ".MSD";
                        dlg.Filter = "MSD Files (*.MSD)|*.MSD";
                        dlg.ShowDialog();
                        string[] pathesMonFiles = dlg.FileNames;

                        var countOfPoint = MeasureDto.HSeptum == TypeOfGTEM.GTEM_0_5 ? 5 : 6;


                        this.MonitoringPathes = new List<string>(countOfPoint);
                        this.MonitoringPathesFiltred = new List<string>(countOfPoint);

                        foreach (var path in pathesMonFiles)
                        {
                            var filtredPath = path.Remove(0, path.LastIndexOf('\\') + 1);
                            this.MonitoringPathesFiltred.Add(filtredPath);
                        }

                        if (pathesMonFiles.Length == countOfPoint)
                        {
                            this.MonitoringPathes.AddRange(pathesMonFiles);
                            var calMessage = CalibrationPathes != null ? "Dodano" : "Dodaj punkty";
                            this.FilePathForMeasure = new FilePathForMeasure() { MonitoringPath = "Dodano", CalibrationPath = calMessage };
                            AutoClosingMessageBox.Show($"Wszystkie punkty Monitorujące zostali dodane.", "", 1200);
                        }
                        else
                        {
                            MessageBoxResult result = MessageBox.Show($"Nieodpowiednia ilość punktów.Dodaj: {NumberOfPoint} punktów!",
                                               "Niepoprawna ilość punktów",
                                               MessageBoxButton.OK,
                                               MessageBoxImage.Error);
                        }
                    }
                })
        );


        private RelayCommand browseCalFileCommand;
        public ICommand BrowseCalFileCommand =>
            browseCalFileCommand ??
            (browseCalFileCommand = new RelayCommand(
                () =>
                {
                    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                    dlg.Multiselect = true;
                    dlg.DefaultExt = ".DAT";
                    dlg.Filter = "DAT Files (*.DAT)|*.DAT";
                    dlg.ShowDialog();
                    string[] pathesCalFiles = dlg.FileNames;

                    var countOfPoint = MeasureDto.HSeptum == TypeOfGTEM.GTEM_0_5 ? 5 : 6;


                    this.CalibrationPathes = new List<string>(countOfPoint);
                    this.CalibrationPathesFiltred = new List<string>(countOfPoint);

                    foreach (var path in pathesCalFiles)
                    {
                        var filtredPath = path.Remove(0, path.LastIndexOf('\\') + 1);
                        this.CalibrationPathesFiltred.Add(filtredPath);
                    }

                    if (pathesCalFiles.Length == countOfPoint)
                    {
                        this.CalibrationPathes.AddRange(pathesCalFiles);
                        var monMessage = MonitoringPathes != null ? "Dodano" : "Dodaj punkty";
                        this.FilePathForMeasure = new FilePathForMeasure() { MonitoringPath = monMessage, CalibrationPath = "Dodano" };
                        AutoClosingMessageBox.Show($"Wszystkie punkty Calibracyjne zostali dodane.", "", 1200);
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show($"Nieodpowiednia ilość punktów.Dodaj: {(int)(selectedMeasure.NumberOfPoints)}.",
                                           "Confirmation",
                                           MessageBoxButton.OK,
                                           MessageBoxImage.Information);
                    }
                })
        );


        private RelayCommand<Measure> deleteMeasureCommand;
        public ICommand DeleteMeasureCommand =>
            deleteMeasureCommand ??
            (deleteMeasureCommand = new RelayCommand<Measure>(
                (measure) =>
                {
                    if(measure != null)
                    {
                        this.measureRepository.DeleteMeasure(measure.Id);
                        this.Measures.Remove(measure);
                        this.FiltredMeasures.Remove(measure);
                        this.frequenceStepsRepository.DeleteByMeasureId(measure.Id);
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show($"Wybierz pomiar do usunięcia.",
                                           "Confirmation",
                                           MessageBoxButton.OK,
                                           MessageBoxImage.Warning);
                    }
                }));

        private RelayCommand frequencyStepsOpenCommand;
        public ICommand FrequencyStepsOpenCommand =>
            frequencyStepsOpenCommand ??
            (frequencyStepsOpenCommand = new RelayCommand(
                () =>
                {
                    if(selectedMeasure != null)
                    {
                        var catOpts = new Views.FrequenceStepsWindow(selectedMeasure, this.frequenceStepsRepository,
                                                          this.MonitoringPathes, this.CalibrationPathes);
                        catOpts.ShowDialog();
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show($"Wybierz pomiar.",
                                          "Confirmation",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Warning);
                    }
                }
                ));

        private RelayCommand resetFilterMeasureCommand;
        public ICommand ResetFilterMeasureCommand =>
            resetFilterMeasureCommand ??
            (resetFilterMeasureCommand = new RelayCommand(
                () =>
                {
                    this.FilterMeasureDto = new FilterMeasureDto();
                    this.FiltredMeasures.Clear();
                    this.FiltredMeasures.AddRange(this.Measures);

                })
        );



        private RelayCommand cleareMeasureCommand;
        public ICommand CleareMeasureCommand =>
            cleareMeasureCommand ??
            (cleareMeasureCommand = new RelayCommand(
                () =>
                {
                    this.MeasureDto = new MeasureDto();
                    this.CalibrationPathesFiltred = new List<string>();
                    this.MonitoringPathesFiltred = new List<string>();
                    this.FilePathForMeasure = new FilePathForMeasure()
                    {
                        MonitoringPath = "Dodaj punkty",
                        CalibrationPath = "Dodaj punkty"
                    };

                })
        );

        private RelayCommand searchMeasuresCommand;
        public ICommand SearchMeasuresCommand =>
            searchMeasuresCommand ??
            (searchMeasuresCommand = new RelayCommand(
                () =>
                {
                    var filtredMeasure = FilterMeasureDto;
                    var filtredMeasures = this.Measures.GetFiltredMeasures(filtredMeasure);
                    this.FiltredMeasures.Clear();
                    this.FiltredMeasures.AddRange(filtredMeasures);
                })
        );

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

    public class FilterMeasureDto
    {
        public string FilterNameOfMeasure { get; set; }
        public string FilterSurname { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public FilterMeasureDto()
        {
            this.DateFrom = DateTime.MinValue.AddYears(DateTime.Now.Year - 1).AddMonths(DateTime.Now.Month - 1);
            this.DateTo = DateTime.MinValue.AddYears(DateTime.Now.Year - 1).AddMonths(DateTime.Now.Month - 1);
        }
    }

    public class MeasureDto
    {
        public string NameOfMeasure { get; set; }
        public string NameOfOperator { get; set; }
        public string ResearchfieldStrength { get; set; }
        public string VerificationfieldStrength { get; set; }
        public TypeOfGTEM HSeptum { get; set; }
        //public int numberOfPoints;


        //public int NumberOfPoints
        //{
        //    get { return numberOfPoints; }
        //    set
        //    {
        //        filePathForMeasure = value;
        //        OnPropertyChanged("FilePathForMeasure");
        //    }
        //}
    }

    public class FilePathForMeasure
    {
        public string MonitoringPath { get; set; }
        public string CalibrationPath { get; set; }

        public void AddMonFile(string monPath)
        {
            this.MonitoringPath = monPath;
        }
    }
}