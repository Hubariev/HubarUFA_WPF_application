﻿using MagisterkaApp.Calculator;
using MagisterkaApp.Domain;
using MagisterkaApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MagisterkaApp
{


    class Program
    {
        static void Main(string[] args)
        {
            //string number = "737,6200E-3";
            //Console.WriteLine(double.Parse(number));

            List<Measure> Measures = new List<Measure>();
            List<FrequencyStep> FrequencySteps = new List<FrequencyStep>();

            List<string> filePathes = new List<string>();

            string fileName1 = $"F:/A_STUDIA/MAGISTERKA/3 semestr/Diplomka_magisterka/Bochanek/Ready_points/pkt_1.MSD";
            string fileName2 = $"F:/A_STUDIA/MAGISTERKA/3 semestr/Diplomka_magisterka/Bochanek/Ready_points/pkt_2.MSD";
            string fileName3 = $"F:/A_STUDIA/MAGISTERKA/3 semestr/Diplomka_magisterka/Bochanek/Ready_points/pkt_3.MSD";
            string fileName4 = $"F:/A_STUDIA/MAGISTERKA/3 semestr/Diplomka_magisterka/Bochanek/Ready_points/pkt_4.MSD";
            string fileName5 = $"F:/A_STUDIA/MAGISTERKA/3 semestr/Diplomka_magisterka/Bochanek/Ready_points/pkt_5.MSD";

            filePathes.Add(fileName1);
            filePathes.Add(fileName2);
            filePathes.Add(fileName3);
            filePathes.Add(fileName4);
            filePathes.Add(fileName5);

            var measure = new Measure("measure1","Tolik", 3, TypeOfGTEM.GTEM_0_5);
            Measures.Add(measure);
            var readSteps = ReadFile.ReadMeasureSteps(filePathes, measure.Id);

            FrequencySteps = CalculateResult.GetResult(readSteps, measure.FieldStrength);



            //SaveResult.WriteResult(measure);

            //measure.MeasureResult.ResultShowValues();
            //measure.Points[0].TestShowValues();

            //double number = 1000;
            ////Console.WriteLine(number.ToString().Replace(',', '.').ToString("n6"));
            //Console.WriteLine(number.ToString(".000000"));

            //string dateTime = DateTime.Now.ToString();
            //string result = DateTime.ParseExact(dateTime, "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture).ToString();

            //Console.WriteLine(dateTime);
        }
    }
}
