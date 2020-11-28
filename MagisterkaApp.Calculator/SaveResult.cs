﻿using MagisterkaApp.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace MagisterkaApp.Calculator
{
    public static class SaveResult
    {
        public static void WriteResult(List<FrequencyStep> frequencySteps, string pathForSave, Measure measure)
        {

            using (StreamWriter file = new StreamWriter(pathForSave))
            {
                file.WriteLine("DEVICE:  TEM");
                file.WriteLine("TYP:      GTEM1750");
                file.WriteLine("");
                file.WriteLine($"# test name:\t{measure.NameOfMeasure}");
                file.WriteLine("");
                file.WriteLine($"# calibration date:\t");
                file.WriteLine("");
                file.WriteLine($"FMIN:\t{frequencySteps[0].Frequency.ToString(".000000").Replace(',', '.')}");
                file.WriteLine($"FMAX:\t{frequencySteps[frequencySteps.Count - 1].Frequency.ToString(".000000").Replace(',', '.')}");
                file.WriteLine("");
                file.WriteLine($"HSEPTUM:   {nameof(measure.HSeptum).Replace(',', '.')} m");
                file.WriteLine("");
                file.WriteLine($"REFERENCE: {measure.ResearchfieldStrength.ToString(".000000").Replace(',', '.')} V/m");
                file.WriteLine("");
                file.WriteLine($"INPUT:     f[MHz] input[dBm]");
                for (int i = 0; i < frequencySteps.Count; i++)
                {
                    file.WriteLine($"{frequencySteps[i].Frequency.ToString(".000000").Replace(',', '.')} " +
                        $"{frequencySteps[i].PowerLevelResult.ToString(".000000").Replace(',', '.')}");
                }

            }

        }

        public static void WriteTestValues(List<FrequencyStep> frequencySteps, string pathForSave)
        {

            using (StreamWriter file = new StreamWriter(pathForSave))
            {
                for (int i = 0; i < frequencySteps.Count; i++)
                {
                    
                    file.WriteLine($"{frequencySteps[i].Frequency.ToString(".000000").Replace(',', '.')} " +
                        $"{frequencySteps[i].Points[0].Primary.Test.ToString(".000000").Replace(',', '.')} " +
                        $"{frequencySteps[i].Points[1].Primary.Test.ToString(".000000").Replace(',', '.')} " +
                        $"{frequencySteps[i].Points[2].Primary.Test.ToString(".000000").Replace(',', '.')} " +
                        $"{frequencySteps[i].Points[3].Primary.Test.ToString(".000000").Replace(',', '.')} " +
                        $"{frequencySteps[i].Points[4].Primary.Test.ToString(".000000").Replace(',', '.')} " +
                        $"{frequencySteps[i].Points[5].Primary.Test.ToString(".000000").Replace(',', '.')} ");
                }

            }

        }
    }
}
