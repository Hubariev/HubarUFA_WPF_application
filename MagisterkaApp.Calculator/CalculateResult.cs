﻿using MagisterkaApp.Domain;
using MagisterkaApp.Domain.Enums;
using MagisterkaApp.Domain.Notifications;
using MagisterkaApp.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MagisterkaApp.Calculator
{
    public static class CalculateResult
    {
        public static ObservableCollection<FrequencyStep> GetResult(ObservableCollection<FrequencyStep> frequencySteps, double fieldstrength)
        {
            var calculatedFrequencySteps = new ObservableCollection<FrequencyStep>();
            foreach (var frequencyStep in frequencySteps)
            {
                var pointsCount = frequencyStep.Points.Count;

                //for (int i = 0; i < pointsCount; i++)
                //{
                double averagePower = 0;
                double squarePower = 0;

                double[] powersAfterCorrection = new double[pointsCount];
                double[] squaresOfPowers = new double[pointsCount];

                #region averagePowerAlgorithm
                for (int j = 0; j < pointsCount; j++)
                {
                    var correctedPower = PowerCorrectionToPrimaryEy(frequencyStep.Points[j].PrimaryEy, fieldstrength,
                                                                     frequencyStep.Points[j].PowerLevel);
                    averagePower += correctedPower;
                    powersAfterCorrection[j] = correctedPower;

                    frequencyStep.Points[j].IsTEMdominant = CheckTEMdominant(frequencyStep.Points[j]);
                }
                averagePower = averagePower / pointsCount;
                #endregion

                #region CalculateDeviation
                for (int z = 0; z < pointsCount; z++)
                {
                    var squarePowerPoint = Math.Pow((powersAfterCorrection[z] - averagePower), 2);
                    squarePower += squarePowerPoint;
                    squaresOfPowers[z] = squarePowerPoint;
                }
                var deviation = Math.Sqrt(squarePower / (pointsCount - 1));
                #endregion


                #region CheckTermsOfDeviation

                if (deviation <= NormProperties.FirstRequirement)
                {
                    frequencyStep.PowerLevelResult = averagePower + (NormProperties.FactorK * deviation);
                    frequencyStep.SetDeviationNotification(NormNotification.Correct);
                }
                else
                {
                    var squaresOfPowersList = (squaresOfPowers.ToList());
                    squaresOfPowersList.Sort();

                    int count75Percent = Convert.ToInt16(squaresOfPowers.Length * 0.75);

                    for (int z = 0; z < count75Percent; z++)
                    {
                        squarePower += squaresOfPowersList[z];
                    }

                    deviation = Math.Sqrt(squarePower / (count75Percent - 1));

                    //var errorPoint = Array.IndexOf(squaresOfPowers, squaresOfPowersList[pointsCount - 1]);

                    if (deviation <= NormProperties.FirstRequirement)
                    {
                        //write numberOfPoint
                        frequencyStep.PowerLevelResult = averagePower + (NormProperties.FactorK * deviation);
                        frequencyStep.SetDeviationNotification(NormNotification.ErrorFirstRequirement);
                    }
                    else
                    {
                        if (deviation <= NormProperties.SecondRequirement)
                        {
                            frequencyStep.PowerLevelResult = averagePower + (NormProperties.FactorK * deviation);
                            frequencyStep.SetDeviationNotification(NormNotification.ErrorSecondRequirement);
                        }
                        else
                        {
                            frequencyStep.PowerLevelResult = averagePower + (NormProperties.FactorK * deviation);
                            frequencyStep.SetDeviationNotification(NormNotification.ErrorFrequence);
                        }
                    }
                }
                #endregion
                //}
                calculatedFrequencySteps.Add(frequencyStep);
            }
            return calculatedFrequencySteps;
        }

        private static double PowerCorrectionToPrimaryEy(double primaryEy, double fieldStrength, double powerLevel)
        {
            var correctionResult = 20 * Math.Log10(fieldStrength / primaryEy) + powerLevel;
            return correctionResult;
        }

        private static TEMdominant CheckTEMdominant(Point point)
        {
            if ((2 * point.SecondaryEx < point.PrimaryEy) && (2 * point.SecondaryEz < point.PrimaryEy))
                return TEMdominant.TEMdominantSmaller_Minus6dB;
            else if((0.79 * point.SecondaryEx < point.PrimaryEy) && (0.79 * point.SecondaryEz < point.PrimaryEy))
                return TEMdominant.TEMdominantSmaller_Minus2dB;
            else
                return TEMdominant.TEMdominantHigher_Minus2dB;
        }
    }
}
