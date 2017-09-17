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
            Console.WindowWidth = (int)(Console.LargestWindowWidth /1.5);
            //setup a timer
            Stopwatch timer = new Stopwatch();
            Stopwatch testTimer = new Stopwatch();

            //setup paramaters for classes
            //setup excersizes to run.
            Dictionary<Excersize, int> excersizes = new Dictionary<Excersize, int>();
            excersizes.Add(new Excersize_1_Q3(100), 5000);
            excersizes.Add(new Excersize_1_Q3(200), 5000);
            excersizes.Add(new Excersize_1_Q3(400), 5000);
            excersizes.Add(new Excersize_1_Q3(800), 5000);
            excersizes.Add(new Excersize_1_Q3(1600), 5000);
            excersizes.Add(new Excersize_1(100), 5000);
            excersizes.Add(new Excersize_1(200), 5000);
            excersizes.Add(new Excersize_1(400), 5000);
            excersizes.Add(new Excersize_1(800), 5000);
            List<string> testResults = new List<string>();

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
                    //comment and un-comment this line if you want to keep track of test progress(recommended to have the line on if debugging or running tests that take a large amount of time)
                    //Console.WriteLine("{0,-60} has completed in {1,10:0.0000} milliseconds Result was {2,5} this was run {3,6}", entry.Key, times[i], result, (i + 1));
                }
                testTimer.Stop();
                string testResult = string.Format("{0,-60} All {4,8} tests completed " +
                    "\r\nAverage time = {1,30:0.000000000000000} milliseconds, full test time: {2,30:0.000000000000000} milliseconds(includes upper for loop for tests)" +
                    "\r\nAverage result = {3,30:0.000000} variable n was: {5,10}\r\n", entry.Key, CalcAvg(times), testTimer.Elapsed.TotalMilliseconds, CalcAvg(results), entry.Value, entry.Key.n);
                testResults.Add(testResult);
            }
            //echo out each tests results
            foreach(string s in testResults)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine("press enter key to close");
            Console.ReadLine();
        }

        public static double CalcAvg(double[] a)
        {
            double avg = 0.0;
            foreach(double entry in a)
            {
                avg += entry;
            }
            return avg / a.Length;
        }

        public static double CalcAvg(int[] a)
        {
            double avg = 0.0;
            foreach(int entry in a)
            {
                avg += entry;
            }
            return avg / a.Length;
        }

        public static double CalcAvg(float[] a)
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
