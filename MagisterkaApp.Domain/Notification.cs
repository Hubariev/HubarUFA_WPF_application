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
                    return Brushes.White;
                case NormNotification.ErrorFirstRequirement:
                    return Brushes.Yellow;
                case NormNotification.ErrorSecondRequirement:
                    return Brushes.DarkOrange;                  
                case NormNotification.ErrorFrequence:
                    return Brushes.Red;
                case NormNotification.ErrorTEMdominant:
                    return Brushes.Violet;
                default:
                    return Brushes.Gray;
            }
        }
    }
}
