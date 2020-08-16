using GalaSoft.MvvmLight.Command;
using MagisterkaApp.Domain;
using MagisterkaApp.Domain.Enums;
using MagisterkaApp.Repo.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MagisterkaApp.UI
{
    public class ApplicationViewModel: ViewModelBase
    {
        public ObservableCollection<Measure> Measures { get; set; }
        private readonly IRepository<Measure, Guid> measureRepository;

        private Measure newMeasure;
        public MeasureDto MeasureDto { get; set; } = new MeasureDto();

        public ApplicationViewModel(IRepository<Measure, Guid> measureRepository)
        {
            this.measureRepository = measureRepository;
            Measures = new ObservableCollection<Measure>();
            GetMeasures();
            //Measures = new ObservableCollection<Measure>();
            //FrequencySteps = new List<FrequencyStep>();

            //List<string> filePathes = new List<string>();

            //string fileName1 = $"F:/A_STUDIA/MAGISTERKA/3 semestr/Diplomka_magisterka/Bochanek/Ready_points/pkt_1.MSD";
            //string fileName2 = $"F:/A_STUDIA/MAGISTERKA/3 semestr/Diplomka_magisterka/Bochanek/Ready_points/pkt_2.MSD";
            //string fileName3 = $"F:/A_STUDIA/MAGISTERKA/3 semestr/Diplomka_magisterka/Bochanek/Ready_points/pkt_3.MSD";
            //string fileName4 = $"F:/A_STUDIA/MAGISTERKA/3 semestr/Diplomka_magisterka/Bochanek/Ready_points/pkt_4.MSD";
            //string fileName5 = $"F:/A_STUDIA/MAGISTERKA/3 semestr/Diplomka_magisterka/Bochanek/Ready_points/pkt_5.MSD";

            //filePathes.Add(fileName1);
            //filePathes.Add(fileName2);
            //filePathes.Add(fileName3);
            //filePathes.Add(fileName4);
            //filePathes.Add(fileName5);

            //var measure = new Measure("measure1", "Tolik", 3, TypeOfGTEM.GTEM_0_5);
            //Measures.Add(measure);
            ////var readSteps = ReadFile.ReadMeasureSteps(filePathes, measure.Id);

            ////FrequencySteps = CalculateResult.GetResult(readSteps, measure.FieldStrength);
            
        }

        private async Task GetMeasures()
        {
            var measures = await this.measureRepository.GetAllAsync();
            this.Measures = new ObservableCollection<Measure>(measures);
        }

        protected override void RegisterCollections()
        {
            Measures = new ObservableCollection<Measure>();
        }


        #region Commands
        private RelayCommand addMeasureCommand;
        public ICommand AddMeasureCommand =>
            addMeasureCommand ??
            (addMeasureCommand = new RelayCommand(
                () =>
                {
                    Measures.Add(new Measure(
                        MeasureDto.NameOfMeasure,
                        MeasureDto.NameOfOperator,
                        Convert.ToDouble(MeasureDto.FieldStrength),
                        MeasureDto.HSeptum));
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
    }
    public class MeasureDto
    {
        public string NameOfMeasure { get; set; }
        public string NameOfOperator { get; set; }
        public string FieldStrength { get; set; }
        public TypeOfGTEM HSeptum { get; set; }
    }
}


//Measures = new ObservableCollection<Measure>();
//            FrequencySteps = new List<FrequencyStep>();

//            List<string> filePathes = new List<string>();

//string fileName1 = $"F:/A_STUDIA/MAGISTERKA/3 semestr/Diplomka_magisterka/Bochanek/Ready_points/pkt_1.MSD";
//string fileName2 = $"F:/A_STUDIA/MAGISTERKA/3 semestr/Diplomka_magisterka/Bochanek/Ready_points/pkt_2.MSD";
//string fileName3 = $"F:/A_STUDIA/MAGISTERKA/3 semestr/Diplomka_magisterka/Bochanek/Ready_points/pkt_3.MSD";
//string fileName4 = $"F:/A_STUDIA/MAGISTERKA/3 semestr/Diplomka_magisterka/Bochanek/Ready_points/pkt_4.MSD";
//string fileName5 = $"F:/A_STUDIA/MAGISTERKA/3 semestr/Diplomka_magisterka/Bochanek/Ready_points/pkt_5.MSD";

//filePathes.Add(fileName1);
//            filePathes.Add(fileName2);
//            filePathes.Add(fileName3);
//            filePathes.Add(fileName4);
//            filePathes.Add(fileName5);

//            var measure = new Measure("measure1", "Tolik", 3, TypeOfGTEM.GTEM_0_5);
//Measures.Add(measure);
//            //var readSteps = ReadFile.ReadMeasureSteps(filePathes, measure.Id);

//            //FrequencySteps = CalculateResult.GetResult(readSteps, measure.FieldStrength);