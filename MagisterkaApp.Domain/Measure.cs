using MagisterkaApp.Domain.Enums;
using MagisterkaApp.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;

namespace MagisterkaApp.Domain
{
    public class Measure: INotifyPropertyChanged
    {
        public Guid Id { get; private set; }
        private string nameOfMeasure;
        private string nameOfOperator;
        private DateTime dateOfMeasure;
        private double researchfieldStrength;
        private double verificationfieldStrength;
        private TypeOfGTEM hSeptum;
        private NumberOfPoints numberOfPoints { get; set; }
        private List<string> monitoringPath { get; set; }
        private List<string> calibrationPath { get; set; }


        public Measure() { } //parametrless constructor for LiteDB
        public Measure(string nameOfMeasure, string nameOfOperator, double researchfieldStrength, double verificationfieldStrength, 
            TypeOfGTEM hSeptum, List<string> monitoringPath, List<string> calibrationPath)
        {
            this.Id = Guid.NewGuid();
            this.nameOfMeasure = nameOfMeasure;
            this.nameOfOperator = nameOfOperator;
            this.researchfieldStrength = researchfieldStrength;
            this.verificationfieldStrength = verificationfieldStrength;
            this.HSeptum = hSeptum;
            this.dateOfMeasure = GetDateOfMeasure();
            this.numberOfPoints = MeasureExtensions.GetNumberOfPoints(HSeptum);
            this.monitoringPath = monitoringPath;
            this.calibrationPath = calibrationPath;
        }

        public string NameOfMeasure
        {
            get { return nameOfMeasure; }
            set
            {
                nameOfMeasure = value;
                OnPropertyChanged("NameOfMeasure");
            }
        }

        public string NameOfOperator
        {
            get { return nameOfOperator; }
            set
            {
                nameOfOperator = value;
                OnPropertyChanged("NameOfOperator");
            }
        }

        public double ResearchfieldStrength
        {
            get { return researchfieldStrength; }
            set
            {
                researchfieldStrength = value;
                OnPropertyChanged("ResearchfieldStrength");
            }
        }

        public double VerificationfieldStrength
        {
            get { return verificationfieldStrength; }
            set
            {
                verificationfieldStrength = value;
                OnPropertyChanged("VerificationfieldStrength");
            }
        }

        public DateTime DateOfMeasure
        {
            get { return dateOfMeasure; }
            set
            {
                dateOfMeasure = value;
                OnPropertyChanged("DateOfMeasure");
            }
        }
        public TypeOfGTEM HSeptum
        {
            get => hSeptum; 
            set
            {
                hSeptum = value;
                OnPropertyChanged("HSeptum");
            }
        }

        public NumberOfPoints NumberOfPoints
        {
            get => numberOfPoints;
            set
            {
                numberOfPoints = value;
                OnPropertyChanged("NumberOfPoints");
            }
        }


        public List<string> MonitoringPath
        {
            get => monitoringPath;
            set
            {
                monitoringPath = value;
                OnPropertyChanged("MonitoringPath");
            }
        }

        public List<string> CalibrationPath
        {
            get => calibrationPath;
            set
            {
                calibrationPath = value;
                OnPropertyChanged("CalibrationPath");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private DateTime GetDateOfMeasure()
        {
            var date = DateTime.Now;
            return date;
            //IFormatProvider ci = new CultureInfo(Thread.CurrentThread.CurrentCulture.Name);
            //return dt.ToString("d", ci);
        }
    }
}
