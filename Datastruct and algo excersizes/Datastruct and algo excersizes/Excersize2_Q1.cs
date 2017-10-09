using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datastruct_and_algo_excersizes
{
    class Excersize2_Q1 : Excersize
    {
        Stack<string> data;
        List<string> dataBuffer;
        public override void ConstructData(string[] paramArgs = null)
        {
            base.ConstructData(paramArgs);

            dataBuffer = new List<string>();
            bool input = true;
            foreach (string param in paramArgs)
            {
                if(param == "generate")
                {
                    input = false;
                    Random random = new Random(Guid.NewGuid().GetHashCode());
                    for(int i = 0; i < this._words; i++)//build words equaal too the param provided
                    {
                        var length = random.Next(3, 10);
                        var word = generateRandomString(length, random);
                        if (paramArgs.Contains("numbertagwords"))
                        {
                            word = (i + 1) + " " + word;
                        }
                        if (paramArgs.Contains("printwords"))
                        {
                            Console.WriteLine(i + " " + word);
                        }
                        dataBuffer.Add(word);
                    }
                }
            }
            while (input)
            {
                var line = Console.ReadLine();
                if(line == null || line == "")
                {
                    input = false;
                    break;
                }
   
                if (paramArgs.Contains("numbertagwords"))
                    line = (dataBuffer.Count() + 1) + " " + line;
                dataBuffer.Add(line);

            }
            data = new Stack<string>(dataBuffer);//commit the buffer into the stack
        }

        public override int run()
        {
            var s = GenerateContentsString(data);
             //Console.WriteLine("contents:\n" + s);

            return base.run();
        }

        public string GenerateContentsString(Stack<string> data)
        {
            if (data.Count() == 0)
                return "";
            if(data.Count() == 1)
            {
                return data.Pop();
            }
            else
            {
                return data.Pop() + "\n" + GenerateContentsString(data); 
            }
        }

        private string generateRandomString(int length, Random random)
        {
            char[] word = new char[length];
            for(int i = 0; i < length; i++)
            {
                word[i] = (char)random.Next('Z', 'a');
            }
            return new string(word);
        }
    }
}
