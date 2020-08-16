using System;
using System.Collections.Generic;
using MagisterkaApp.Domain.Notifications;

namespace MagisterkaApp.Domain
{
    //ToDo: calculateMethod
    public class FrequencyStep
    {
        public Guid MeasureId { get; }
        public double Frequency { get; }
        public double PowerLevelResult { get; set; }
        public List<Point> Points { get; }
        public List<Notification> Notifications { get; set; }

        public FrequencyStep(Guid measureId, double frequence)
        {
            this.MeasureId = measureId;
            this.Frequency = frequence;
            this.Points = new List<Point>();
            this.Notifications = new List<Notification>();
        }

        public void AddPoint(int id, double primaryEy, double secondaryEx, double secondaryEz, double powerLevel)
        {
            this.Points.Add(new Point(id, primaryEy, secondaryEx, secondaryEz, powerLevel));
        }

        public void SetNotification(string text) => this.Notifications.Add(new Notification(text));
    }
}
