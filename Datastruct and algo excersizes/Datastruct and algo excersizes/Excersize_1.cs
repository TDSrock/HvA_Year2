using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace Datastruct_and_algo_excersizes
{
    
    class Excersize_1 : Excersize
    {
        int[] highScores;
        public Excersize_1()
        {

        }

        public override void ConstructData(string[] paramArgs = null)
        {
            base.ConstructData(paramArgs);
            int entities = this._n;
            this.highScores = new int[entities];
            Random random = new Random(Guid.NewGuid().GetHashCode());
            //construct data
            for (int i = 0; i < highScores.Length; i++)
            {
                highScores[i] = random.Next(10000);
            }

        }

        public override int run()
        {
            var re = HasDupe.HasDuplicate(this.highScores);
            return re;//replace this with whatever you want to report.
        }

    }

    public static class HasDupe
    {
        public static int HasDuplicate(this IEnumerable<int> source, bool counting = false)
        {
            int count = 0;
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var checkBuffer = new HashSet<int>();
            foreach (int item in source)
            {
                if (!checkBuffer.Add(item))
                {
                    //Console.WriteLine("could not add" + item);
                    if (counting)
                    {
                        count++;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }

            return count;
        }
    }
}
