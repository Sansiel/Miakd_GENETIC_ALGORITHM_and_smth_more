using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miakd_1_dist
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] x = { 100, 120, 130, 150 };
            int[] y = { 70, 100, 120, 140 };

            int sumX = 0;
            int sumY = 0;
            int sumXY = 0;
            int sumXX = 0;

            foreach (int i in x) { sumX += i; } 
            foreach (int i in y) { sumY += i; }
            foreach (int i in x) { foreach (int j in y) { sumXY = sumXY + i * j; } }
            foreach (int i in x) { foreach (int j in x) { sumXX = sumXX + i * j; } }

            int n = x.Length;
            int b1 = (sumXY - (sumX - sumY) / n) / (sumXX - sumX * sumX / n);
            int b0 = (sumY - b1 * sumX) / n;

            Console.WriteLine(b0);
            Console.WriteLine(b1);

            var exit = Console.ReadLine();
        }
    }
}
