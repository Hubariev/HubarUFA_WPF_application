using MagisterkaApp.Domain;
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
        public static ObservableCollection<FrequencyStep> GetResult(ObservableCollection<FrequencyStep> frequencySteps, double researchfieldStrength,
            double verificationfieldStrength)
        {
            var calculatedFrequencySteps = new ObservableCollection<FrequencyStep>();
            foreach (var frequencyStep in frequencySteps)
            {
                var pointsCount = frequencyStep.Points.Count;

                double averagePower = 0;
                double squarePower = 0;

                double[] powersAfterCorrection = new double[pointsCount];
                double[] squaresOfPowers = new double[pointsCount];

                #region averagePowerAlgorithm
                for (int j = 0; j < pointsCount; j++)
                {
                    var correctedPower = PowerCorrectionToPrimaryEy(frequencyStep.Points[j].Primary.Input, verificationfieldStrength,
                                                                     frequencyStep.Points[j].PowerLevel.Input);
                    averagePower += correctedPower;
                    powersAfterCorrection[j] = correctedPower;

                    frequencyStep.Points[j].IsTEMdominant = CheckTEMdominant(frequencyStep.Points[j]);
                    frequencyStep.Points[j].Primary.Corrected = verificationfieldStrength;
                    frequencyStep.Points[j].SecondaryOne.Corrected = GetCorrectedSecondary(verificationfieldStrength, frequencyStep.Points[j].Primary.Input,
                                                                                           frequencyStep.Points[j].SecondaryOne.Input);
                    frequencyStep.Points[j].SecondaryTwo.Corrected = GetCorrectedSecondary(verificationfieldStrength, frequencyStep.Points[j].Primary.Input,
                                                                                           frequencyStep.Points[j].SecondaryTwo.Input);
                    frequencyStep.Points[j].PowerLevel.Corrected = correctedPower;

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
                if (frequencyStep.Frequency == 253.728555)
                {
                    var a = "b";
                }
                var deviation = Math.Sqrt(squarePower / (pointsCount * (pointsCount - 1)));
                #endregion


                #region CheckTermsOfDeviation

                if (deviation <= NormProperties.FirstRequirement)
                {
                    var fieldStrengthLog = 20 * Math.Log10(researchfieldStrength / verificationfieldStrength);
                    var powerResultOne = averagePower + (NormProperties.FactorK * deviation);
                    frequencyStep.PowerLevelResult = powerResultOne + fieldStrengthLog;
                    frequencyStep.SetDeviationNotification(NormNotification.Correct);
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

                        for (int position = 0; position < pointsCount; position++)
                        {
                            frequencyStep.Points[position].PointBackgroundColor = new BackgroundColor(NormNotification.ErrorFrequence);
                        }
                    }

                }

                for (int t = 0; t < pointsCount; t++)
                {
                    frequencyStep.Points[t].Primary.Test = GetPrimaryTest(verificationfieldStrength, powersAfterCorrection[t],
                        frequencyStep.PowerLevelResult);

                    frequencyStep.Points[t].SecondaryOne.Test = GetTestSecondary(verificationfieldStrength, frequencyStep.Points[t].Primary.Test,
                        frequencyStep.Points[t].SecondaryOne.Corrected);
                    frequencyStep.Points[t].SecondaryTwo.Test = GetTestSecondary(verificationfieldStrength, frequencyStep.Points[t].Primary.Test,
                  frequencyStep.Points[t].SecondaryTwo.Corrected);

                    frequencyStep.Points[t].PowerLevel.Test = frequencyStep.PowerLevelResult;
                }

                #endregion
                calculatedFrequencySteps.Add(frequencyStep);

                int count75procent = Convert.ToInt16(pointsCount * 0.75);

                int firstCounter = 0;
                int seconfCounter = 0;
                int ThirdCounter = 0;

                for (int r = 0; r < pointsCount; r++)
                {
                    if (frequencyStep.Points[r].IsTEMdominant == TEMdominant.TEMdominantSmaller_Minus6dB)
                        firstCounter++;
                    else if (frequencyStep.Points[r].IsTEMdominant == TEMdominant.TEMdominantSmaller_Minus2dB)
                        seconfCounter++;
                    else if (frequencyStep.Points[r].IsTEMdominant == TEMdominant.TEMdominantHigher_Minus2dB)
                        ThirdCounter++;
                }

                if (firstCounter > count75procent)
                {
                    frequencyStep.SetTEMNotification(TEMdominantNotification.Correct);
                }
                else if (firstCounter >= count75procent)
                {
                    frequencyStep.SetTEMNotification(TEMdominantNotification.ConfirmFirstRequirement);
                }
                else if ((seconfCounter + firstCounter) >= count75procent)
                    frequencyStep.SetTEMNotification(TEMdominantNotification.ConfirmSecondRequirement);
                else if (ThirdCounter > (pointsCount - count75procent))
                    frequencyStep.SetTEMNotification(TEMdominantNotification.ErrorDominant);
            }
            return calculatedFrequencySteps;
        }

        private static double GetPrimaryTest(double primary, double correctedPower, double resultPower)
        {
            var powerDifference = 0.1 * (resultPower - correctedPower);
            var powResult = Math.Pow(10, powerDifference);

            return primary * Math.Sqrt(powResult);
        }
        private static double GetTestSecondary(double fieldStrength, double primary, double secondary)
        {
            return (primary / fieldStrength) * secondary;
        }

        private static double GetCorrectedSecondary(double fieldStrength, double primary, double secondary)
        {
            return (fieldStrength / primary) * secondary;
        }

        private static double PowerCorrectionToPrimaryEy(double primaryEy, double fieldStrength, double powerLevel)
        {
            var correctionResult = 20 * Math.Log10(fieldStrength / primaryEy) + powerLevel;
            return correctionResult;
        }

        private static TEMdominant CheckTEMdominant(Point point)
        {
            if ((2 * point.SecondaryOne.Input < point.Primary.Input) && (2 * point.SecondaryTwo.Input < point.Primary.Input))
                return TEMdominant.TEMdominantSmaller_Minus6dB;
            else if ((0.79 * point.SecondaryOne.Input < point.Primary.Input) && (0.79 * point.SecondaryTwo.Input < point.Primary.Input))
                return TEMdominant.TEMdominantSmaller_Minus2dB;
            else
                return TEMdominant.TEMdominantHigher_Minus2dB;
        }
    }
}
