using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datastruct_and_algo_excersizes
{
    class StateManager
    {
        Dictionary<State, string> myStates;
        State startState;
        State currentState;
        public StateManager()
        {

        }

        public bool AddState(State newState, bool isStartState = false)
        {
            if(myStates.ContainsKey(newState))
            {
                return false;
            }
            try {
                myStates.Add(newState, "comperator should be here");
                this.startState = newState;
            } catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
            return true;
        }

        public bool ConnectState(State connectingState, State connectorState/*, comperator???*/)
        {
            
            try
            {
                connectingState.exitStates.Add(connectorState);
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public void evalState()
        {
            currentState.evalState();
        }

        public List<State> GetAllStates()
        {
            List<State> allStates = new List<State>();
            foreach(KeyValuePair<State, string> keyValuePair in myStates)
            {
                allStates.Add(keyValuePair.Key);
            }
            return allStates;
        }

        public List<State> GetUnreachableStates(State StartState)
        {
            List<State> unreachableStates = GetAllStates();
            List<State> discoveredStates = new List<State>();

            if(!unreachableStates.Contains(StartState))
            {
                throw new StateNotIncludedException("The state " + StartState + " is not a part of this StateManager");
            }
            unreachableStates.Remove(StartState);
            discoveredStates.Add(StartState);

            while(discoveredStates.Count != 0)
            {
                State investigatingState = discoveredStates[0];//get a state we know off
                discoveredStates.Remove(investigatingState);
                foreach(State foundState in investigatingState.exitStates)
                {
                    if(unreachableStates.Contains(foundState))//if the state found is still considered unreachable
                    {
                        unreachableStates.Remove(foundState);//make it no longer unreachalble
                        discoveredStates.Add(foundState);//and add it too the discovered states
                    }
                }
            }

            return unreachableStates;

        }

        public bool AreAllStatesReachable()
        {
            return GetUnreachableStates(this.startState).Count < 1;
        }

       
    }

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
}
