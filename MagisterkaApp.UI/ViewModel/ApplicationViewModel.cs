using GalaSoft.MvvmLight.Command;
using MagisterkaApp.Domain;
using MagisterkaApp.Domain.Enums;
using MagisterkaApp.Domain.SeedWork;
using MagisterkaApp.Repo.Abstractions;
using MagisterkaApp.UI.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public Measure selectedMeasure { get; set; }

        private Measure newMeasure;
        public MeasureDto MeasureDto { get; set; } = new MeasureDto();

        public FilePathForMeasure filePathForMeasure { get; set; } = new FilePathForMeasure()
        {
            MonitoringPath = "No file path",
            CalibrationPath = "No file path"
        };

        PointsFilePathes pointsPathes = new PointsFilePathes();

        private Boolean iSAddFileEnabled = true;

        public List<string> MonitoringPathes;
        public List<string> CalibrationPathes;

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
            GetMeasures();

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


                    //AutoClosingMessageBox.Show("DODANO", "", 1200);

                    //MessageBoxResult result = MessageBox.Show("Dodaj punkty pomiarowe do pomiaru.",
                    //                        "Confirmation",
                    //                        MessageBoxButton.OK,
                    //                        MessageBoxImage.Information);




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
                    //this.MonitoringPathes.Add(FilePathForMeasure.MonitoringPath);
                    //this.CalibrationPathes.Add(FilePathForMeasure.CalibrationPath);

                    //AutoClosingMessageBox.Show($"DODANO: {numberOfPoint} punkt.", "", 1200);
                    //this.FilePathForMeasure = new FilePathForMeasure()
                    //{
                    //    MonitoringPath = "No file path",
                    //    CalibrationPath = "No file path"
                    //};
                    //NumberOfPoint++;

                    //if (numberOfPoint - 1 == this.MonitoringPathes.Capacity)
                    //{
                    //    iSAddFileEnabled = false;
                    //    AutoClosingMessageBox.Show($"Wszystkie punkty pomiarowe zostali dodane.", "", 1200);
                    //    NumberOfPoint = 1;

                    //}

                    AutoClosingMessageBox.Show($"Wszystkie punkty pomiarowe zostali dodane.", "", 1200);



                })
        );

        private RelayCommand browseMonFileCommand;
        public ICommand BrowseMonFileCommand =>
            browseMonFileCommand ??
            (browseMonFileCommand = new RelayCommand(
                () =>
                {
                    var t = MeasureDto;
                    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                    dlg.Multiselect = true;
                    dlg.DefaultExt = ".MSD";
                    dlg.Filter = "MSD Files (*.MSD)|*.MSD";
                    dlg.ShowDialog();
                    string[] pathesMonFiles = dlg.FileNames;

                    var countOfPoint = MeasureDto.HSeptum == TypeOfGTEM.GTEM_0_5 ? 5 : 6;

                    this.MonitoringPathes = new List<string>(countOfPoint);

                    if (pathesMonFiles.Length == countOfPoint)
                    {
                        this.MonitoringPathes.AddRange(pathesMonFiles);
                        AutoClosingMessageBox.Show($"Wszystkie punkty Monitorujące zostali dodane.", "", 1200);
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show($"Nieodpowiednia ilość punktów.Dodaj: {NumberOfPoint} punktów!",
                                           "Niepoprawna ilość punktów",
                                           MessageBoxButton.OK,
                                           MessageBoxImage.Error);
                    }



                    //Nullable<bool> result = dlg.ShowDialog();

                    //if (result == true)
                    //{
                    //    var monitoringFilePath = dlg.FileName;
                    //    this.monitoringFilePath = monitoringFilePath;
                    //    this.filtredMonitoringFilePath = monitoringFilePath.Remove(0, monitoringFilePath.LastIndexOf('\\') + 1);
                    //    this.FilePathForMeasure = new FilePathForMeasure() { MonitoringPath = this.filtredMonitoringFilePath, CalibrationPath = "No file path" };
                    //}
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

                    if (pathesCalFiles.Length == countOfPoint)
                    {
                        this.MonitoringPathes.AddRange(pathesCalFiles);
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
                    var catOpts = new Views.FrequenceStepsWindow(selectedMeasure, this.frequenceStepsRepository,
                                                                 this.MonitoringPathes, this.CalibrationPathes);
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