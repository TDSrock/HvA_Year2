using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Datastruct_and_algo_excersizes
{
    class Excersize_1 : Excersize
    {
        public override int run()
        {
            int[] boop = new int[300];
            for(int i = 0; i < 100; i++)
            {
                double p = Math.Log( (double)i / 4, i);
                boop[i] = i * 200 / (i + 5);
            }
            return base.run();//replace this with whatever you want to report.
        }
    }
}
