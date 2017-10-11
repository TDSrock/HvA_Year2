using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datastruct_and_algo_excersizes
{
    class Excersize3_Q3 : Excersize
    {
        public override int run()
        {
            //Create agent
            var robber = new InputRobber();

            for (int i = this._n; i > 0; i--) {//not a fan of forever so lets do this N times
                //run the input robber's get input method.
                robber.StateInput();
                Console.WriteLine("You still have " + i + " turns left");
            }

            return base.run();
        }
    }
}
