﻿using MagisterkaApp.Domain.Notifications;
using System.Windows.Media;

namespace MagisterkaApp.Domain
{
    public class Notification
    {
        public string Text { get; set; }
        public string backgroundColor { get; set; }

        public Notification(string normNotification)
        {
            this.Text = normNotification;
            this.backgroundColor = GetBackgroundColor(normNotification);
        }

        private string GetBackgroundColor(string normNotification)
        {
            switch (normNotification)
            {
                case NormNotification.Correct:
                    return "FF98FB98";                    //Brushes.PaleGreen
                case NormNotification.ErrorFirstRequirement:
                    return "FFFFFF00";                              //Brushes.Yellow;
                case NormNotification.ErrorSecondRequirement:
                    return "FFFF8C00";                                       //Brushes.DarkOrange;                  
                case NormNotification.ErrorFrequence:
                    return "FFFF0000";                                 // Brushes.Red;

                case TEMdominantNotification.Correct:
                    return "FF98FB98";
                case TEMdominantNotification.ConfirmFirstRequirement:
                    return "FFFFFF00";
                case TEMdominantNotification.ConfirmSecondRequirement:
                    return "FFFF8C00";
                case TEMdominantNotification.ErrorDominant:
                    return "FFFF0000";

                default:
                    return "FF808080";                 //Brushes.Gray;
            }
        }
    }
}
