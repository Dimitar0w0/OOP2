using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;

namespace Race
{
   public enum Boost
   {
      DO100, DO200
   }

   public class Racer
   {
      public string FirstName;
      public string LastName;
      public string CBrand;
      public double CWeight;
      public double CHP;
      public Boost CBoost;
      public double time1
      {
         get
         {
            if (CBoost == Boost.DO100)
            {
               return 500 / CHP * 0.7;
            }
            return 500 / CHP;

         }
      }
      public double time2
      {
         get
         {
            if (CBoost == Boost.DO200)
            {
               return 1000 / CHP * 0.8;
            }
            return 1000 / CHP;
         }
      }
      public int Points;

      public Racer(string firstname, string lastname, string cbrand, double cweight, double chp, Boost cboost)
      {
         this.FirstName = firstname;
         this.LastName = lastname;
         this.CBrand = cbrand;
         this.CWeight = cweight;
         this.CHP = chp;
         this.CBoost = cboost;
      }
   }

   class Program
   {
      static void Main(string[] args)
      {
         List<Racer> racers = new List<Racer>();

         while (true)
         {
            string input = Console.ReadLine();
            if (input == "end")
            {
               break;
            }

            string[] data = input.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(str => str.Trim())
                .ToArray();
            string firstName = data[0];
            string lastName = data[1];
            string cbrand = data[2];
            double cweight = double.Parse(data[3]);
            double chp = double.Parse(data[4]);
            string cboost = data[5];

            Racer r = new Racer(firstName, lastName, cbrand, cweight, chp, (cboost == "do-100" ? Boost.DO100 : Boost.DO200));
            racers.Add(r);
         }

         for (int f = 0; f < racers.Count; f++)
         {
            for (int c = f; c < racers.Count; c++)
            {
               if (c == f) continue;

               Racer r0 = racers[f];
               Racer r1 = racers[c];

               if (r0.time1 < r1.time1) r0.Points += 3;
               else if (r0.time1 > r1.time1) r1.Points += 3;

               if (r0.time1 + r0.time2 < r1.time1 + r1.time2) r0.Points += 3;
               else if (r0.time1 + r0.time2 > r1.time1 + r1.time2) r1.Points += 3;
            }
         }

         foreach (var racer in racers.OrderByDescending(x => x.Points))
         {
            Console.WriteLine($"{racer.FirstName} {racer.Points}");
         }

         Console.ReadLine();
      }
   }
}
