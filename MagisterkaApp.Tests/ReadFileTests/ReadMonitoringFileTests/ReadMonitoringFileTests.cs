using MagisterkaApp.Calculator;
using MagisterkaApp.Domain;
using System;
using System.Collections.Generic;
using Xunit;

namespace MagisterkaApp.Tests.ReadFileTests.ReadMonitoringFileTests
{  
    public class ReadMonitoringFileTests
    {
        [Fact]
        public void CheckReadingOfMonitoringFile_2freqSteps()
        {
            List<string> pathesMonitoringFiles = new List<string>();
            pathesMonitoringFiles.Add("F:/_My_ProgaLibrary/Own-Projects/WPF_UniformityFieldApp/MagisterkaApp.Tests/MeasurePointsForTests/MonitoringFiles_5points/2FrequencySteps/Punkt_1.MSD");
            pathesMonitoringFiles.Add("F:/_My_ProgaLibrary/Own-Projects/WPF_UniformityFieldApp/MagisterkaApp.Tests/MeasurePointsForTests/MonitoringFiles_5points/2FrequencySteps/Punkt_2.MSD");
            pathesMonitoringFiles.Add("F:/_My_ProgaLibrary/Own-Projects/WPF_UniformityFieldApp/MagisterkaApp.Tests/MeasurePointsForTests/MonitoringFiles_5points/2FrequencySteps/Punkt_3.MSD");
            pathesMonitoringFiles.Add("F:/_My_ProgaLibrary/Own-Projects/WPF_UniformityFieldApp/MagisterkaApp.Tests/MeasurePointsForTests/MonitoringFiles_5points/2FrequencySteps/Punkt_4.MSD");
            pathesMonitoringFiles.Add("F:/_My_ProgaLibrary/Own-Projects/WPF_UniformityFieldApp/MagisterkaApp.Tests/MeasurePointsForTests/MonitoringFiles_5points/2FrequencySteps/Punkt_5.MSD");

            var readPoints = ReadFile.ReadMonitoringFile(pathesMonitoringFiles, Guid.NewGuid());

            var point = new Point(1, 4.9747, 0.889516, 1.0488, "Ex_Primary", "Ey_Secondary", "Ez_Secondary");


            Assert.Equal(2, readPoints.Count);
            Assert.Equal(5, readPoints[0].Points.Count);

            Assert.Equal(point.Primary.Input, readPoints[0].Points[0].Primary.Input);
            Assert.Equal(point.SecondaryOne.Input, readPoints[0].Points[0].SecondaryOne.Input);
            Assert.Equal(point.SecondaryTwo.Input, readPoints[0].Points[0].SecondaryTwo.Input);
        }
    }
}
