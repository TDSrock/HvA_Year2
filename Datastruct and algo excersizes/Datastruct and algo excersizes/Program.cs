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
            Console.WindowWidth = (int)(Console.LargestWindowWidth /1.3);
            Console.WindowHeight = (int)(Console.LargestWindowHeight / 1.3);

            bool printTestResultsDuring = false;//whether or not we should print the test results during the tests or not
            bool beepAfterEachTest = false;//whether or not console.beep() is called after each test(only enable on LONG tests)
            bool printExcersizeResultsDuring = true;//whether or not we should print excersize results during the tests or not
            bool beepAfterExersize = false;// whether or not console.beep() is called after each excersize(usefull if running several long excersizes)
            bool printAllResultsAtEnd = true;//whether or not we should print all the results at the end 
            bool beepAfterAllExcersizes = true;//whether or not console.beep() is called when all results have been posted(usefull on long or many excersizes so you can hear when the program is done)

            //setup a timer
            Stopwatch timer = new Stopwatch();
            Stopwatch testTimer = new Stopwatch();

            //setup paramaters for classes
            //setup excersizes to run.
            Dictionary<Excersize, Tuple<int, string[]>> excersizes = new Dictionary<Excersize, Tuple<int, string[]>>
            {
                { new Excersize3_Q5(), new Tuple<int, string[]>(1, new string[] { "n=1000" }) }
            };
            List<string> testResults = new List<string>();

            foreach(KeyValuePair<Excersize, Tuple<int, string[]>> entry in excersizes)
            {
                int[] results = new int[entry.Value.Item1];
                double[] times = new double[entry.Value.Item1];
                testTimer.Restart();
                for(int i = 0; i < entry.Value.Item1; i++)
                {
                    entry.Key.ConstructData(entry.Value.Item2);//have the excersize construct it's dataset.
                    timer.Restart();//make sure the timer is ready to rock
                    var result = entry.Key.run();//run the excersize
                    timer.Stop();
                    times[i] = timer.Elapsed.TotalMilliseconds;
                    results[i] = result;
                    if(printTestResultsDuring)
                        Console.WriteLine("{0,-60} has completed in {1,10:0.0000} milliseconds Result was {2,5} this was run {3,6}", entry.Key, times[i], result, (i + 1));
                    if (beepAfterEachTest)
                        Console.Beep();
                }
                testTimer.Stop();
                string paramArgs = StringArrayToString(entry.Value.Item2);
                string testResult = string.Format("{0,-60} All {4,8} tests completed " +
                    "\r\nAverage time = {1,30:0.000000000000000} milliseconds, full test time: {2,30:0.000000000000000} milliseconds(includes upper for loop for tests and dataconstruction)" +
                    "\r\nAverage result = {3,28:0.000000000000000}   paramArgs: {5,-1}\r\n", entry.Key, CalcAvg(times), testTimer.Elapsed.TotalMilliseconds, CalcAvg(results), entry.Value.Item1, paramArgs);
                if (printExcersizeResultsDuring)
                    Console.WriteLine("\r\n" + testResult);
                if (beepAfterExersize)
                    Console.Beep();
                testResults.Add(testResult);
            }
            if (printAllResultsAtEnd)
            {
                Console.WriteLine("\r\n" +
                    "Full report:\r\n");
                //echo out each tests results
                foreach (string s in testResults)
                {
                    Console.WriteLine(s);
                }
                if (beepAfterAllExcersizes)
                    Console.Beep();
            }

            Console.WriteLine(String.Format("Print settings were: \r\n{0,7} printTestResultsDuring  " +
                "\r\n{1,7} printExcersizeResultsDuring  " +
                "\r\n{2,7} printAllResultsAtEnd", printTestResultsDuring, printExcersizeResultsDuring, printAllResultsAtEnd));
            Console.WriteLine("press enter key to close");
            Console.ReadLine();

            Console.ReadLine();

        }

        public static string StringArrayToString(string[] s)
        {
            string r = "";
            foreach(string param in s)
            {
                r += param + " | ";
            }
            return r.Substring(0, r.Length - 3);
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
