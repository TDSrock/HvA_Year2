using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datastruct_and_algo_excersizes.StateMananger
{
    public class StateNotIncludedException : Exception
    {
        private string _message;

        public StateNotIncludedException(string message)
        {

            this._message = message;
        }

        public override string Message
        {
            get
            {
                return this._message;
            }
        }
    }

    public class StateManagerAlreadyActiveException : Exception
    {
        private string _message;

        public StateManagerAlreadyActiveException(string message)
        {

            this._message = message;
        }

        public override string Message
        {
            get
            {
                return this._message;
            }
        }
    }

    public class StateManagerNotValidatedException : Exception
    {
        private string _message;

        public StateManagerNotValidatedException(string message)
        {

            this._message = message;
        }

        public override string Message
        {
            get
            {
                return this._message;
            }
        }
    }
}
