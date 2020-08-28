using MagisterkaApp.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace MagisterkaApp.Calculator
{
    public static class ReadFile
    {
        /*
         ToDoList:
         1. Change way to read columns and area between start line and line where columns exactly starts:
         frEx: if(Ch1 or Ch2 or bla bla V/m) then 2 lines below will be columns
             */
        public static ObservableCollection<FrequencyStep> ReadMonitoringFile(List<string> pathMeasuredPoints, Guid measureId)
        {
            var frequenceSteps = new ObservableCollection<FrequencyStep>();
            int pointId = 1;
            const int columnDifference = 1;
  

            foreach (var pathName in pathMeasuredPoints)
            {               
                using (StreamReader file = new StreamReader(pathName))
                {
                    int counter = 3;
                    string line;
                    string filtredLine;
                    string[] resultLine = new string[] { };

                    #region positions
                    int frequencePosition = 0;
                    int primaryPosition = 0;
                    int secondaryOnePosition = 0;
                    int secondaryTwoPosition = 0;
                    #endregion

                    while ((line = file.ReadLine()) != null)
                    {
                        if(line.ToUpper().Contains("PRIMAR") || line.ToUpper().Contains("VERTICAL") || line.ToUpper().Contains("PIONOWO"))
                        {
                            var primaryLine = line.Replace("\t", " ");
                            primaryLine = primaryLine.Replace(":", "");
                            string[] resultPrimaryLine = primaryLine.Split(new char[] { ' ' }).ToArray();
                            resultPrimaryLine = resultPrimaryLine.Where(element => !string.IsNullOrWhiteSpace(element)).ToArray();
                            primaryPosition = Convert.ToInt16(resultPrimaryLine[1]) + columnDifference;
                        }

                        if ((line.ToUpper().Contains("SECOND") || line.ToUpper().Contains("TRANSWERSE") || line.ToUpper().Contains("POPRZECZ")) 
                            && secondaryOnePosition == 0)
                        {
                            var primaryLine = line.Replace("\t", " ");
                            primaryLine = primaryLine.Replace(":", "");
                            string[] resultPrimaryLine = primaryLine.Split(new char[] { ' ' }).ToArray();
                            resultPrimaryLine = resultPrimaryLine.Where(element => !string.IsNullOrWhiteSpace(element)).ToArray();
                            secondaryOnePosition = Convert.ToInt16(resultPrimaryLine[1]) + columnDifference;
                        }

                        if (line.ToUpper().Contains("SECOND") || line.ToUpper().Contains("LONGITUDINAL") || line.ToUpper().Contains("WZD"))
                        {
                            var primaryLine = line.Replace("\t", " ");
                            primaryLine = primaryLine.Replace(":", "");
                            string[] resultPrimaryLine = primaryLine.Split(new char[] { ' ' }).ToArray();
                            resultPrimaryLine = resultPrimaryLine.Where(element => !string.IsNullOrWhiteSpace(element)).ToArray();
                            secondaryTwoPosition = Convert.ToInt16(resultPrimaryLine[1]) + columnDifference;
                        }


                        if (counter == 0)
                        {
                            filtredLine = line.Replace("\t", " ");
                            filtredLine = filtredLine.Replace(".", ",");
                            resultLine = filtredLine.Split(new char[] { ' ' }).ToArray();
                            resultLine = resultLine.Where(element => !string.IsNullOrWhiteSpace(element)).ToArray();

                            var frequency = double.Parse(resultLine[frequencePosition]);
                            var primary = double.Parse(resultLine[primaryPosition]);
                            var secondaryOne = double.Parse(resultLine[secondaryOnePosition]);
                            var secondarySecond = double.Parse(resultLine[secondaryTwoPosition]);

                            FrequencyStep step;

                            if (pointId > 1)
                            {
                                step = frequenceSteps.FirstOrDefault(x => x.Frequency == frequency);
                            }
                            else
                            {
                                step = new FrequencyStep(measureId, frequency);
                            }
                            
                            step.AddPoint(pointId, primary, secondaryOne, secondarySecond);
                                                       
                            if(pointId == 1)
                                frequenceSteps.Add(step);
                        }
                        if (counter < 3 && counter != 0)
                        {
                            counter--;
                        }
                        if (line.Contains("Ch1"))
                        {
                            counter = 2;
                        }                       
                    }
                    file.Close();
                }
                pointId++;
            }
            return frequenceSteps;          
        }

        public static ObservableCollection<FrequencyStep> ReadCalibrationFile(List<string> calibrationPathes,
            ObservableCollection<FrequencyStep> frequencySteps)
        {
            int pointId = 1;

            #region positions
            int frequencePosition = 0;
            int powerPosition = 1;
            #endregion

            for(int i = 0; i < calibrationPathes.Count; i++)
            {
                using (StreamReader file = new StreamReader(calibrationPathes[i]))
                {
                    int counter = 1;
                    string line;
                    string filtredLine;
                    string[] resultLine = new string[] { };

                    int frequencyStepsCounter = 0;

                    while ((line = file.ReadLine()) != null || (line = file.ReadLine()).Length != 0)
                    {                        
                        if (counter == 0)
                        {
                            filtredLine = line.Replace(".", ",");
                            resultLine = filtredLine.Split(new char[] { ' ' }).ToArray();

                            if (resultLine.Length < 2)
                                break ;

                            var frequency = double.Parse(resultLine[frequencePosition]);
                            var power = double.Parse(resultLine[powerPosition]);

                            if (frequencySteps[frequencyStepsCounter].Frequency == frequency)
                                frequencySteps[frequencyStepsCounter].Points[i].AddPowerResult(power);
                            else
                                throw new Exception("Error with Reading calibration file.");

                            frequencyStepsCounter++;
                        }
                        if (line.Contains("INPUT"))
                        {
                            counter = 0;
                        }
                        
                    }
                    file.Close();
                }
                pointId++;
            }
            return frequencySteps;
        }
    }
}