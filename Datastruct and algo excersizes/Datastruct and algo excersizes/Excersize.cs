using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Datastruct_and_algo_excersizes
{
    class Excersize
    {
        private List<string> missingPorperties = new List<string>();
        private int n;
        private int words;
        public Excersize()
        {

        }

        public int _n { get { return n; } set { n = value; } }
        public int _words { get { return words; } set { words = value; } }

        //override this method and construct the class's data set within it.
        virtual public void ConstructData(string[] paramArgs = null)
        {
            foreach (string param in paramArgs)
            {
                if (param != String.Empty)//filter out acidental empty strings
                {
                    int indexOfEquaals = param.IndexOf('=');
                    if (indexOfEquaals != -1)//if it does not have an = it is meant for the other method to be dealth with, so we can ignore it
                    {
                        string variableName = param.Substring(0, indexOfEquaals);
                        if (!missingPorperties.Contains(variableName))//if we already know we don't have this property no need to try it again.
                        {
                            try
                            {
                                //use reflection to set var
                                this.GetType().GetProperty("_" + variableName).SetValue(this, Convert.ToInt32(param.Substring(indexOfEquaals + 1, param.Length - (indexOfEquaals + 1))));
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Missing property: _" + variableName + "\nOccured in " + this + " please check the param args and remove or add the property too the base-class if intended\n");
                                Console.WriteLine(e.StackTrace);
                                missingPorperties.Add(variableName);
                                Console.Beep();
                            }
                        }
                    }
                }
            }
        }

        virtual public int run()
        {
            return 0;
        }
    }
}
