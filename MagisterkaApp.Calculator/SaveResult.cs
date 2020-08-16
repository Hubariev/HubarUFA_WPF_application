using MagisterkaApp.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace MagisterkaApp.Calculator
{
    public static class SaveResult
    {
        public static void WriteResult(List<FrequencyStep> frequencySteps, string NameOfMeasure, double FieldStrength, double HSeptum)
        {
            //string pathForWrite = $"F:/A_STUDIA/MAGISTERKA/3 semestr/Diplomka_magisterka/Bochanek/Results/{measure.NameOfMeasure}.DAT";
            //using (StreamWriter file = new StreamWriter(pathForWrite))
            //{             
            //    file.WriteLine("DEVICE:  TEM");
            //    file.WriteLine("TYP:      GTEM1750");
            //    file.WriteLine("");
            //    file.WriteLine($"# test name:\t{measure.NameOfMeasure}");
            //    file.WriteLine("");
            //    file.WriteLine($"# calibration date:\t");
            //    file.WriteLine("");
            //    file.WriteLine($"FMIN:\t{measure.MeasureResult.Frequence[0].ToString(".000000").Replace(',','.')}");
            //    file.WriteLine($"FMAX:\t{measure.MeasureResult.Frequence[measure.MeasureResult.Frequence.Count-1].ToString(".000000").Replace(',', '.')}");
            //    file.WriteLine("");
            //    file.WriteLine($"HSEPTUM:   {measure.HSeptum.ToString().Replace(',', '.')} m");
            //    file.WriteLine("");
            //    file.WriteLine($"REFERENCE: {measure.PrimaryE.ToString(".000000").Replace(',', '.')} V/m");
            //    file.WriteLine("");
            //    file.WriteLine($"INPUT:     f[MHz] input[dBm]");
            //    for(int i = 0; i < measure.MeasureResult.Frequence.Count; i++)
            //    {
            //        file.WriteLine($"{measure.MeasureResult.Frequence[i].ToString(".000000").Replace(',', '.')} " +
            //            $"{measure.MeasureResult.PowerLevels[i].ToString(".000000").Replace(',', '.')}");
            //    }

            //}

        }
    }
}
