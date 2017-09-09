using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Datastruct_and_algo_excersizes
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup a timer
            Stopwatch timer = new Stopwatch();
            Stopwatch testTimer = new Stopwatch();
            //setup excersizes to run.
            Dictionary<Excersize, int> excersizes = new Dictionary<Excersize, int>();
            excersizes.Add(new Excersize_1(), 5000);//make excersize 1 and I want to run it 5 times

            foreach(KeyValuePair<Excersize, int> entry in excersizes)
            {
                int[] results = new int[entry.Value];
                double[] times = new double[entry.Value];
                testTimer.Restart();
                for(int i = 0; i < entry.Value; i++)
                {
                    timer.Restart();//make sure the timer is ready to rock
                    var result = entry.Key.run();//run the excersize
                    timer.Stop();
                    times[i] = timer.Elapsed.TotalMilliseconds;
                    results[i] = result;
                    //comment and un-comment this line if you want to keep track of test progress
                    Console.WriteLine(entry.Key + " has completed in " + times[i] + " milliseconds Result was " + result + " this was run " + (i + 1));
                }
                testTimer.Stop();
                Console.WriteLine("All " + entry.Key + " tests completed");

                Console.WriteLine("Average time = " + calcAvg(times) + " milliseconds, full test time: " + testTimer.Elapsed.TotalMilliseconds +
                    " milliseconds(includes upper for loop for tests)");
                Console.WriteLine("Average result = " + calcAvg(results));
                Console.WriteLine();
            }
            Console.WriteLine("press enter key to close");
            Console.ReadLine();
        }

        public static double calcAvg(double[] a)
        {
            double avg = 0.0;
            foreach(double entry in a)
            {
                avg += entry;
            }
            return avg / a.Length;
        }

        public static double calcAvg(int[] a)
        {
            double avg = 0.0;
            foreach(int entry in a)
            {
                avg += entry;
            }
            return avg / a.Length;
        }

        public static double calcAvg(float[] a)
        {
            double avg = 0.0;
            foreach(float entry in a)
            {
                avg += entry;
            }
            return avg / a.Length;
        }

    }
}
