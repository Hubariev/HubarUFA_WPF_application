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
        public static void WriteResult(List<FrequencyStep> frequencySteps, string NameOfMeasure, double FieldStrength, string HSeptum)
        {
            string pathForWrite = $"F:/A_STUDIA/MAGISTERKA/3_semestr/Diplomka_magisterka/Bochanek/Results/{NameOfMeasure}.DAT";
            using (StreamWriter file = new StreamWriter(pathForWrite))
            {
                file.WriteLine("DEVICE:  TEM");
                file.WriteLine("TYP:      GTEM1750");
                file.WriteLine("");
                file.WriteLine($"# test name:\t{NameOfMeasure}");
                file.WriteLine("");
                file.WriteLine($"# calibration date:\t");
                file.WriteLine("");
                file.WriteLine($"FMIN:\t{frequencySteps[0].Frequency.ToString(".000000").Replace(',', '.')}");
                file.WriteLine($"FMAX:\t{frequencySteps[frequencySteps.Count - 1].Frequency.ToString(".000000").Replace(',', '.')}");
                file.WriteLine("");
                file.WriteLine($"HSEPTUM:   {HSeptum.Replace(',', '.')} m");
                file.WriteLine("");
                file.WriteLine($"REFERENCE: {FieldStrength.ToString(".000000").Replace(',', '.')} V/m");
                file.WriteLine("");
                file.WriteLine($"INPUT:     f[MHz] input[dBm]");
                for (int i = 0; i < frequencySteps.Count; i++)
                {
                    file.WriteLine($"{frequencySteps[i].Frequency.ToString(".000000").Replace(',', '.')} " +
                        $"{frequencySteps[i].PowerLevelResult.ToString(".000000").Replace(',', '.')}");
                }

            }

        }
    }
}
