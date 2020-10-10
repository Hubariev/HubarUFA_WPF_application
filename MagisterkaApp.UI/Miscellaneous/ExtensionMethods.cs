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
    }
}


