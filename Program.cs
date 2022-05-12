using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace z1
{
    class Program
    {
        static double multSekwencyjnie(double [] x, double [] y)
        {
            double result = 0;
                for(int i = 0; i < x.Length; i++)
                    result += x[i] * y[i];
            return result;
        }

        static double multParallelfor(double[] x, double[] y)
        {
            double result = 0;
            object _ = new object();

            Parallel.For(0, x.Length, () => 0.0, (i, state, local) =>
            {
                return local + x[i] * y[i];
            }, local =>
            {
                lock (_)
                    result += local;
            }
            );
            return result; 
        }

        static async Task<double> multTask(double [] x, double [] y)
        {
            double result2 = await Task.Factory.StartNew(() =>
            {
                double result = 0;
                for (int i = 0; i < x.Length; i++)
                    result += x[i] * y[i];
                return result;
            });
            return result2;
        }

        static void Main(string[] args)
        {
            int N = 1000000;
            Stopwatch sw = new Stopwatch();
            Random rdm = new Random();


            double[] vector1 = Enumerable.Range(0, N).Select(x => (rdm.Next(0, N)).ToArray();
            double[] vector2 = Enumerable.Range(0, N).Select(x => (rdm.Next(0, N)).ToArray();

            for(int i = 0; i < 50; i++)
            {
                Console.WriteLine(vector1[i]);
                Console.WriteLine(vector2[i]);

            }

            sw.Start();
            var res1 = multSekwencyjnie(vector1, vector2);
            sw.Stop();
            Console.WriteLine($" Sekwencyjnie: {sw.ElapsedMilliseconds} ms ");
            sw.Reset();
            
            sw.Start();
            var res2 = multParallelfor(vector1, vector2);
            sw.Stop();
            Console.WriteLine($" Parallelfor: {sw.ElapsedMilliseconds} ms");
            sw.Reset();

            sw.Start();
            var res3 = multTask(vector1, vector2);
            sw.Stop();
            Console.WriteLine($" Task: {sw.ElapsedMilliseconds} ms");

        }
    }
}
