using MagisterkaApp.Domain;
using MagisterkaApp.Domain.Enums;
using MagisterkaApp.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisterkaApp.UI.Miscellaneous
{
    public static class ExtensionMethods
    {
        public static string GetImagePath(this MeasureDto measureDto)
        {
            if (measureDto != null)
            {
                switch (measureDto.HSeptum)
                {
                    case TypeOfGTEM.None:
                        return "/Images/noimage.png";

                    case TypeOfGTEM.GTEM_0_5:

                        return "/Images/uklad5points.png";
                    case TypeOfGTEM.GTEM_1_735:
                        return "/Images/uklad6points.png";

                    default:
                        return "/Images/noimage.png";
                }
            }
            else
            {
                return "/Images/noimage.png";
            }
        }

        public static ObservableCollection<Measure> GetFiltredMeasures(this ObservableCollection<Measure> obsMeasures, FilterMeasureDto filterMeasureDto)
        {
            var measures = obsMeasures.ToList();

            var filtredMeasures = new ObservableCollection<Measure>();

            

            foreach(var measure in measures)
            {
                bool measureChoosed = true;

                if (!String.IsNullOrEmpty(filterMeasureDto.FilterNameOfMeasure))
                {
                    measureChoosed = measure.NameOfMeasure.ToUpper().Contains(filterMeasureDto.FilterNameOfMeasure.ToUpper());
                }

                if (!String.IsNullOrEmpty(filterMeasureDto.FilterSurname) && measureChoosed != false)
                {
                    measureChoosed = measure.NameOfOperator.ToUpper().Contains(filterMeasureDto.FilterSurname.ToUpper());
                }

                var defaultDate = DateTime.MinValue.AddYears(DateTime.Now.Year - 1).AddMonths(DateTime.Now.Month - 1);

                if (filterMeasureDto.DateFrom != defaultDate && filterMeasureDto.DateTo != defaultDate && measureChoosed != false)
                {
                    measureChoosed = measure.DateOfMeasure > filterMeasureDto.DateFrom
                                  && measure.DateOfMeasure < filterMeasureDto.DateTo;
                }

                if (measureChoosed)
                {
                    filtredMeasures.Add(measure);
                }
            }

            return filtredMeasures;
        }


        public static ObservableCollection<FrequencyStep> GetFiltredFrequencySteps(this ObservableCollection<FrequencyStep> frequencySteps,
                                Boolean smaller, Boolean smaller75, Boolean between, Boolean bigger)
        {
            var filtredFrequencySteps = new ObservableCollection<FrequencyStep>();

            var filtredSmaller = new List<FrequencyStep>();
            var filtredSmaller75 = new List<FrequencyStep>();
            var filtredBetween = new List<FrequencyStep>();
            var filtredBigger = new List<FrequencyStep>();

            var filtredOrderById = new List<FrequencyStep>();

            if (!smaller && !smaller75 && !between && !bigger)
            {
                filtredFrequencySteps.AddRange(frequencySteps);
            }
            else
            {
                if (smaller)
                {
                    filtredSmaller = frequencySteps.Where(x => x.DeviationNotification.backgroundColor == "#FF98FB98").ToList();
                }

                if (smaller75)
                {
                    filtredSmaller75 = frequencySteps.Where(x => x.DeviationNotification.backgroundColor == "#FFFFFF00").ToList();
                }

                if (between)
                {
                    filtredBetween = frequencySteps.Where(x => x.DeviationNotification.backgroundColor == "#FFFFA500").ToList();
                }

                if (bigger)
                {
                    filtredBigger = frequencySteps.Where(x => x.DeviationNotification.backgroundColor == "#FFFF0000").ToList();
                }



                filtredOrderById.AddRange(filtredSmaller);
                filtredOrderById.AddRange(filtredSmaller75);
                filtredOrderById.AddRange(filtredBetween);
                filtredOrderById.AddRange(filtredBigger);

                filtredOrderById = filtredOrderById.OrderBy(x => x.FrequencyNumber).ToList();

                filtredFrequencySteps.AddRange(filtredOrderById);
            }

           

            return filtredFrequencySteps;
        }



        public static ObservableCollection<FrequencyStep> GetFiltredTEMFrequencySteps(this ObservableCollection<FrequencyStep> frequencySteps,
                                Boolean smaller, Boolean smaller75, Boolean between, Boolean bigger)
        {
            var filtredFrequencySteps = new ObservableCollection<FrequencyStep>();

            var filtredSmaller = new List<FrequencyStep>();
            var filtredSmaller75 = new List<FrequencyStep>();
            var filtredBetween = new List<FrequencyStep>();
            var filtredBigger = new List<FrequencyStep>();

            var filtredOrderById = new List<FrequencyStep>();

            if (!smaller && !smaller75 && !between && !bigger)
            {
                filtredFrequencySteps.AddRange(frequencySteps);
            }
            else
            {
                if (smaller)
                {
                    filtredSmaller = frequencySteps.Where(x => x.TEMNotification.backgroundColor == "#FF98FB98").ToList();
                }

                if (smaller75)
                {
                    filtredSmaller75 = frequencySteps.Where(x => x.TEMNotification.backgroundColor == "#FFFFFF00").ToList();
                }

                if (between)
                {
                    filtredBetween = frequencySteps.Where(x => x.TEMNotification.backgroundColor == "#FFFFA500").ToList();
                }

                if (bigger)
                {
                    filtredBigger = frequencySteps.Where(x => x.TEMNotification.backgroundColor == "#FFFF0000").ToList();
                }



                filtredOrderById.AddRange(filtredSmaller);
                filtredOrderById.AddRange(filtredSmaller75);
                filtredOrderById.AddRange(filtredBetween);
                filtredOrderById.AddRange(filtredBigger);

                filtredOrderById = filtredOrderById.OrderBy(x => x.FrequencyNumber).ToList();

                filtredFrequencySteps.AddRange(filtredOrderById);
            }



            return filtredFrequencySteps;
        }
    }
}


