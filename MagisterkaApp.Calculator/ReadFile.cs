using MagisterkaApp.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MagisterkaApp.Calculator
{
    public static class ReadFile
    {

        /*
         ToDoList:
         1. Change way to read columns and area between start line and line where columns exactly starts:
         frEx: if(Ch1 or Ch2 or bla bla V/m) then 2 lines below will be columns
             */
        public static List<FrequencyStep> ReadMeasureSteps(List<string> pathMeasuredPoints, Guid measureId)
        {
            var frequenceSteps = new List<FrequencyStep>();
            int pointId = 1;

            foreach (var pathName in pathMeasuredPoints)
            {               
                using (StreamReader file = new StreamReader(pathName))
                {
                    int counter = 3;
                    string line;
                    string filtredLine;
                    string[] resultLine = new string[] { };

                    while ((line = file.ReadLine()) != null)
                    {
                        if (counter == 0)
                        {
                            filtredLine = line.Replace("\t", " ");
                            filtredLine = filtredLine.Replace(".", ",");
                            resultLine = filtredLine.Split(new char[] { ' ' }).ToArray();
                            resultLine = resultLine.Where(element => !string.IsNullOrWhiteSpace(element)).ToArray();

                            //Console.WriteLine(line.ToString());
                            //Console.WriteLine(filtredLine.ToString());

                            var frequency = double.Parse(resultLine[0]);
                            var primaryEy = double.Parse(resultLine[4]);
                            var secondaryEx = double.Parse(resultLine[3]);
                            var secondaryEz = double.Parse(resultLine[5]);
                            var powerLevel = double.Parse(resultLine[6]);

                            FrequencyStep step;

                            if (pointId > 1)
                            {
                                step = frequenceSteps.FirstOrDefault(x => x.Frequency == frequency);
                            }
                            else
                            {
                                step = new FrequencyStep(measureId, frequency);
                            }
                            
                            step.AddPoint(pointId, primaryEy, secondaryEx, secondaryEz, powerLevel);
                                                       
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
    }
}

//public static Point ReadMeasurePoint(string fileName, int id)
//{
//    List<double> Frequence;
//    List<double> PrimaryEy;
//    List<double> SecondaryEx;
//    List<double> SecondaryEz;
//    List<double> PowerLevels;
//    Point point = new Point(id);

//    using (StreamReader file = new StreamReader(fileName))
//    {
//        int counter = 3;
//        string line;
//        string filtredLine;
//        string[] resultLine = new string[] { };

//        while ((line = file.ReadLine()) != null)
//        {
//            if (counter == 0)
//            {
//                filtredLine = line.Replace("\t", " ");
//                filtredLine = filtredLine.Replace(".", ",");
//                resultLine = filtredLine.Split(new char[] { ' ' }).ToArray();
//                resultLine = resultLine.Where(element => !string.IsNullOrWhiteSpace(element))
//                       .ToArray();
//                //Console.WriteLine(line.ToString());
//                //Console.WriteLine(filtredLine.ToString());

//                point.AddMeasure(double.Parse(resultLine[0]), double.Parse(resultLine[4]), double.Parse(resultLine[3]),
//                    double.Parse(resultLine[5]), double.Parse(resultLine[6]));

//            }
//            if (counter < 3 && counter != 0)
//            {
//                counter--;
//            }
//            if (line.Contains("Ch1"))
//            {
//                counter = 2;
//            }
//        }
//        file.Close();
//    }
//    return point;
//}