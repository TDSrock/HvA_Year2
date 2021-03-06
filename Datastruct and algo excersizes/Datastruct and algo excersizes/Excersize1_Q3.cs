﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Datastruct_and_algo_excersizes
{
    class Excersize_1_Q3 : Excersize
    {
        public Excersize_1_Q3()
        {

        }

        private int[] data;

        public override void ConstructData(string[] paramArgs = null)
        {
            base.ConstructData(paramArgs);
            int entities = this._n;
            this.data = new int[10000];
            Random random = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < entities; i++)
            {
                data[random.Next(data.Length)]++;
            }
        }

        public override int run()
        {
            return HasMore.HasTwoOrMore(data);
        }
    }

    public static class HasMore
    {
        public static int HasTwoOrMore(this IEnumerable<int> source, bool counting = false)
        {
            int count = 0;
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            //var orderedBuffer = source.OrderByDescending(i => i);
            //lets not order as we then don't have to go through O=nlog(n)
            //but instead O=n

            foreach (int item in source)
            {
                if (item > 1)
                {
                    if (counting)
                    {
                        count++;
                    }
                    else
                    {
                        //Console.WriteLine("found a 2 or higher : " + item);
                        return 1;
                    }
                }
            }
            return count;
        }
    }
}
