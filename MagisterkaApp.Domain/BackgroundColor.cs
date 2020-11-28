
using MagisterkaApp.Domain.Notifications;
using System.Windows.Media;

namespace MagisterkaApp.Domain
{
    public class BackgroundColor
    {
        public Brush DeviationBackgroundColor { get; set; }
        public Brush TEMdominantBackgroundColor { get; set; }

        public BackgroundColor(string notification)
        {
            this.DeviationBackgroundColor = GetDeviationBackgroundColor(notification);
        }

        private Brush GetDeviationBackgroundColor(string notification)
        {
            switch (notification)
            {
                case NormNotification.Correct:
                    return Brushes.PaleGreen;
                case NormNotification.ErrorSecondRequirement:
                    return Brushes.Orange;
                case NormNotification.ErrorFrequence:
                    return Brushes.Red;

                default:
                    return Brushes.Gray;
            }
        }

        public void AddTEMdominantBackgroundColor(string notification)
        {
            this.TEMdominantBackgroundColor = GetTEMdominantBackgroundColor(notification);
        }

        private Brush GetTEMdominantBackgroundColor(string notification)
        {
            switch (notification)
            {
                case TEMdominantNotification.Correct:
                    return Brushes.PaleGreen;
                case TEMdominantNotification.ConfirmFirstRequirement:
                    return Brushes.Yellow;
                case TEMdominantNotification.ConfirmSecondRequirement:
                    return Brushes.Orange;
                case TEMdominantNotification.ErrorDominant:
                    return Brushes.Red;

                default:
                    return Brushes.Gray;
            }
        }
    }
}
