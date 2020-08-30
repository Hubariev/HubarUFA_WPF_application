﻿using System;
using System.Collections.Generic;
using MagisterkaApp.Domain.Notifications;

namespace MagisterkaApp.Domain
{
    //ToDo: calculateMethod
    public class FrequencyStep
    {
        public Guid MeasureId { get; set; }
        public double Frequency { get; set; }
        public double PowerLevelResult { get; set; }
        public List<Point> Points { get; set; }
        public Notification DeviationNotification { get; set; }
        public Notification TEMNotification { get; set; }

        public bool IsHidden { get; set; }

        public FrequencyStep() { }
        public FrequencyStep(Guid measureId, double frequence)
        {
            this.MeasureId = measureId;
            this.Frequency = frequence;
            this.Points = new List<Point>();
        }

        public void AddPoint(int id, double primaryInput, double secondaryOneInput, double secondaryTwoInput,
            string primaryName, string secondaryOneName, string secondaryTwoName)
        {
            this.Points.Add(new Point(id, primaryInput, secondaryOneInput, secondaryTwoInput,
                                 primaryName, secondaryOneName, secondaryTwoName));
        }

        public void SetDeviationNotification(string text) => this.DeviationNotification = new Notification(text);
        public void SetTEMNotification(string text) => this.TEMNotification = new Notification(text);
    }
}
