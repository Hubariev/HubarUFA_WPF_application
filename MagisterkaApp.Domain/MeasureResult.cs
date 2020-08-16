using System;
using System.Collections.Generic;
using System.Text;

namespace MagisterkaApp.Domain
{
    public class MeasureResult
    {
        public List<double> Frequence { get;}
        public List<double> PowerLevels { get; }
        public List<string> PointsWithProblem { get; }
        public MeasureResult(List<double> frequence, List<double> powerLevels, List<string> pointWithProblem)
        {
            this.Frequence = frequence;
            this.PowerLevels = powerLevels;
            this.PointsWithProblem = pointWithProblem;//no string а пусть передается поинт и тут внутри уже будет метод, который сам будет стоить стринг сообщение
        }

        public void ResultShowValues()
        {
            System.Console.WriteLine("f \t Power");
            for (int i = 0; i < Frequence.Count; i++)
            {
                //System.Console.WriteLine(Frequence[i] + "\t" + PrimaryEy[i] + "\t" + SecondaryEx[i] +
                //    "\t" + SecondaryEz[i] + "\t" + PowerLevels[i]);
                System.Console.WriteLine(Frequence[i] + " " + PowerLevels[i]);
            }
            foreach(var problemPoint in PointsWithProblem)
            {
                Console.WriteLine(problemPoint);
            }
        }
    }
}
