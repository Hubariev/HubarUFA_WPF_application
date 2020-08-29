using MagisterkaApp.Domain.Enums;
using System;
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
        private double corrrectedfieldStrength;
        private TypeOfGTEM hSeptum;
        private NumberOfPoints numberOfPoints { get; set; }


        public Measure() { } //parametrless constructor for LiteDB
        public Measure(string nameOfMeasure, string nameOfOperator, double fieldStrength, TypeOfGTEM hSeptum)
        {
            this.Id = Guid.NewGuid();
            this.nameOfMeasure = nameOfMeasure;
            this.nameOfOperator = nameOfOperator;
            this.corrrectedfieldStrength = fieldStrength;
            this.HSeptum = hSeptum;
            this.dateOfMeasure = GetDateOfMeasure();
            this.numberOfPoints = GetNumberOfPoints(HSeptum);
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

        public double CorrrectedfieldStrength
        {
            get { return corrrectedfieldStrength; }
            set
            {
                corrrectedfieldStrength = value;
                OnPropertyChanged("FieldStrength");
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private NumberOfPoints GetNumberOfPoints(TypeOfGTEM typeOfGTEM)
        {
            if (typeOfGTEM == TypeOfGTEM.GTEM_0_5)
                return NumberOfPoints.Five;
            else
                return NumberOfPoints.Six;
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
