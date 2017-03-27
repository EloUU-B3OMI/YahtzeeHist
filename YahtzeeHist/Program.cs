using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace YahtzeeHist
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var data = new List<int[]>();
            for (int q = 10; q <= 100; q += 10)
                data.Add(File.ReadAllLines($"samples-d{q}.dat").Select(int.Parse).ToArray());
            var max = data.Select(a => a.Max()).Max();
            var min = data.Select(a => a.Min()).Min();
            var hist = new int[max - min + 1, data.Count];
            for (var i = 0; i < data.Count; i++)
                for (var j = 0; j < data[i].Length; j++)
                    hist[data[i][j] - min, i]++;
            var sums = new double[data.Count];
            for (var i = 0; i < sums.Length; i++)
                for (var j = 0; j < hist.GetLength(0); j++)
                    sums[i] += hist[j, i];
            for (var i = 0; i < hist.GetLength(0); i++)
            {
                Console.Write(i + min);
                for (var j = 0; j < hist.GetLength(1); j++)
                {
                    var norm = hist[i, j] / sums[j];
                    if (double.IsNaN(norm))
                        norm = 0;

                    Console.Write($"\t{hist[i, j].ToString().Replace('.', ',')}");
                }
                Console.WriteLine();
            }
        }
    }
}
