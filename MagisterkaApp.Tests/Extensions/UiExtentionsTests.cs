using MagisterkaApp.Domain;
using MagisterkaApp.UI.ViewModel;
using System;
using System.Collections.ObjectModel;
using MagisterkaApp.UI.Miscellaneous;
using Xunit;

namespace MagisterkaApp.Tests.UiExtentionsTests
{
    public class UiExtentionsTests
    {
        [Fact]
        public void CheckFilterCriteriaMeasure()
        {
            var criteria = new FilterMeasureDto()
            {
                FilterNameOfMeasure = "pomiar",
                FilterSurname = "Jan",
                DateFrom = DateTime.ParseExact("05.10.2020 07:33:21", "dd.MM.yyyy HH:mm:ss",
                                                       System.Globalization.CultureInfo.InvariantCulture),

                DateTo = DateTime.ParseExact("09.10.2020 07:33:21", "dd.MM.yyyy HH:mm:ss",
                                                       System.Globalization.CultureInfo.InvariantCulture)
            };

            var measure1 = new Measure()
            {
                NameOfMeasure = "pomiar332",
                NameOfOperator = "Jan",
                DateOfMeasure = DateTime.ParseExact("07.10.2020 07:33:21", "dd.MM.yyyy HH:mm:ss",
                                                       System.Globalization.CultureInfo.InvariantCulture)
            };

            var measure2 = new Measure()
            {
                NameOfMeasure = "measure332",
                NameOfOperator = "Bob",
                DateOfMeasure = DateTime.ParseExact("07.10.2020 07:33:21", "dd.MM.yyyy HH:mm:ss",
                                                       System.Globalization.CultureInfo.InvariantCulture)
            };

            var measure3 = new Measure()
            {
                NameOfMeasure = "pomiar332",
                NameOfOperator = "Jan",
                DateOfMeasure = DateTime.ParseExact("04.10.2020 07:33:21", "dd.MM.yyyy HH:mm:ss",
                                                       System.Globalization.CultureInfo.InvariantCulture)
            };


            var measures = new ObservableCollection<Measure>() { measure1, measure2, measure3 };

            var resultMeasures = new ObservableCollection<Measure>() { measure1 };

            Assert.Equal(resultMeasures, measures.GetFiltredMeasures(criteria));
        }
    }
}