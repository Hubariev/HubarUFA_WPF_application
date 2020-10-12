using MagisterkaApp.Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MagisterkaApp.Tests.ReadFileTests.ReadCalibrationFileTests
{
    public class ReadCalibrationFileTests
    {
        [Fact]
        public void CheckReadingOfCalibrationFile_2freqSteps()
        {
            List<string> pathesMonitoringFiles = new List<string>();
            pathesMonitoringFiles.Add("F:/_My_ProgaLibrary/Own-Projects/WPF_UniformityFieldApp/MagisterkaApp.Tests/MeasurePointsForTests/MonitoringFiles_5points/2FrequencySteps/Punkt_1.MSD");
            pathesMonitoringFiles.Add("F:/_My_ProgaLibrary/Own-Projects/WPF_UniformityFieldApp/MagisterkaApp.Tests/MeasurePointsForTests/MonitoringFiles_5points/2FrequencySteps/Punkt_2.MSD");
            pathesMonitoringFiles.Add("F:/_My_ProgaLibrary/Own-Projects/WPF_UniformityFieldApp/MagisterkaApp.Tests/MeasurePointsForTests/MonitoringFiles_5points/2FrequencySteps/Punkt_3.MSD");
            pathesMonitoringFiles.Add("F:/_My_ProgaLibrary/Own-Projects/WPF_UniformityFieldApp/MagisterkaApp.Tests/MeasurePointsForTests/MonitoringFiles_5points/2FrequencySteps/Punkt_4.MSD");
            pathesMonitoringFiles.Add("F:/_My_ProgaLibrary/Own-Projects/WPF_UniformityFieldApp/MagisterkaApp.Tests/MeasurePointsForTests/MonitoringFiles_5points/2FrequencySteps/Punkt_5.MSD");

            var readPointsMon = ReadFile.ReadMonitoringFile(pathesMonitoringFiles, Guid.NewGuid());

            List<string> pathesCalibrationFiles = new List<string>();

            pathesCalibrationFiles.Add("F:/_My_ProgaLibrary/Own-Projects/WPF_UniformityFieldApp/MagisterkaApp.Tests/MeasurePointsForTests/CalibrationFiles_5points/2FrequencySteps/Punkt_1_2020_08_17.DAT");
            pathesCalibrationFiles.Add("F:/_My_ProgaLibrary/Own-Projects/WPF_UniformityFieldApp/MagisterkaApp.Tests/MeasurePointsForTests/CalibrationFiles_5points/2FrequencySteps/Punkt_2_2020_08_17.DAT");
            pathesCalibrationFiles.Add("F:/_My_ProgaLibrary/Own-Projects/WPF_UniformityFieldApp/MagisterkaApp.Tests/MeasurePointsForTests/CalibrationFiles_5points/2FrequencySteps/Punkt_3_2020_08_18.DAT");
            pathesCalibrationFiles.Add("F:/_My_ProgaLibrary/Own-Projects/WPF_UniformityFieldApp/MagisterkaApp.Tests/MeasurePointsForTests/CalibrationFiles_5points/2FrequencySteps/Punkt_4_2020_08_18.DAT");
            pathesCalibrationFiles.Add("F:/_My_ProgaLibrary/Own-Projects/WPF_UniformityFieldApp/MagisterkaApp.Tests/MeasurePointsForTests/CalibrationFiles_5points/2FrequencySteps/Punkt_5_2020_08_18.DAT");


            var readPoints = ReadFile.ReadCalibrationFile(pathesCalibrationFiles, readPointsMon);

            var t = "";
        }
    }
}
