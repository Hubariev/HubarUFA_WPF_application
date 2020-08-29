using MagisterkaApp.Domain.Notifications;
using System.Windows.Media;

namespace MagisterkaApp.Domain
{
    public class Notification
    {
        public string Text { get; set; }
        public Brush backgroundColor { get; set; }

        public Notification(string normNotification)
        {
            this.Text = normNotification;
            this.backgroundColor = GetBackgroundColor(normNotification);
        }

        private Brush GetBackgroundColor(string normNotification)
        {
            switch (normNotification)
            {
                case NormNotification.Correct:
                    return Brushes.PaleGreen;
                case NormNotification.ErrorFirstRequirement:
                    return Brushes.Yellow;
                case NormNotification.ErrorSecondRequirement:
                    return Brushes.DarkOrange;                  
                case NormNotification.ErrorFrequence:
                    return Brushes.Red;

                case TEMdominantNotification.Correct:
                    return Brushes.PaleGreen;
                case TEMdominantNotification.ConfirmFirstRequirement:
                    return Brushes.Yellow;
                case TEMdominantNotification.ConfirmSecondRequirement:
                    return Brushes.DarkOrange;
                case TEMdominantNotification.ErrorDominant:
                    return Brushes.Red;

                default:
                    return Brushes.Gray;
            }
        }
    }
}
