using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datastruct_and_algo_excersizes
{
    class StackQueue<T>
    {
        private Stack<T> outputStack;
        private Stack<T> inputStack;
        private int size;

        public StackQueue()
        {
            inputStack = new Stack<T>();
            outputStack = new Stack<T>();
            this.size = 0;
        }

        public StackQueue(IEnumerable<T> data)
        {
            inputStack = new Stack<T>();
            outputStack = new Stack<T>();
            this.size = 0;
            foreach(T element in data)
            {
                this.Enqueue(element);
            }
        }

        public void Enqueue(T e)
        {
            inputStack.Push(e);
            size++;
        }

        public T Dequeue()
        {
            // fill out all the Input if output stack is empty
            if (outputStack.Count == 0)
                while (inputStack.Count != 0)
                    outputStack.Push(inputStack.Pop());

            T temp = default(T);
            if (outputStack.Count != 0)
            {
                temp = outputStack.Pop();
                size--;
            }

            return temp;
        }

        public int Count()
        {
            return size;
        }

        public bool IsEmpty()
        {
            return size == 0;
        }

    }
}
