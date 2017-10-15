using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datastruct_and_algo_excersizes
{
    class Excersize3_Q7 : Excersize
    {
        public override int run()
        {
            var robber = new CapturableAutomatedRobber();
            var cop = new AutomatedCop(robber);
            while (!robber._isCaptured)
            {
                Console.Write("robberBlerb :");
                robber.EvaluateStateMachine();
                Console.Write("copBlerb :");
                cop.EvaluateStateMachine();
                Console.WriteLine();
                System.Threading.Thread.Sleep(500);
            }
            return base.run();
        }
    }
}
