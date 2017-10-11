using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datastruct_and_algo_excersizes
{
    class StateManager<T>
    {
        Dictionary<State<T>, string> myStates;
        State<T> startState;
        State<T> currentState;
        T agent;
        bool isValidStateMachine = false; 

        public StateManager(T agent)
        {
            this.agent = agent;
        }

        public bool AddState(State<T> newState, bool isStartState = false)
        {
            if(isValidStateMachine)
            {
                Console.WriteLine("State machine has already been validated and may not be edited anymore");
                return false;
            }
            if(myStates.ContainsKey(newState))
            {
                return false;
            }
            try {
                myStates.Add(newState, newState._stateName);
                this.startState = newState;
            } catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
            return true;
        }

        public void ExecuteState()
        {
            if(isValidStateMachine)
            {
                Console.WriteLine("State machine has not been validated yet. Did you forget to validate the statemachine?");
                return;
            }
            State<T> changeState;
            if(this.currentState.evaluateWorld(this.agent ,out changeState))
            {
                this.ChangeState(changeState);
            }
            else
            {
                currentState.OnStayInState();
            }

        }

        private void ChangeState(State<T> stateToChangeToo)
        {
            currentState.OnExitState();
            currentState = stateToChangeToo;
            currentState.OnEnterState();
        }

        public List<State<T>> GetAllStates()
        {
            List<State<T>> allStates = new List<State<T>>();
            foreach(KeyValuePair<State<T>, string> keyValuePair in myStates)
            {
                allStates.Add(keyValuePair.Key);
            }
            return allStates;
        }

        public List<State<T>> GetUnreachableStates(State<T> StartState)
        {
            List<State<T>> unreachableStates = GetAllStates();
            List<State<T>> discoveredStates = new List<State<T>>();

            if(!unreachableStates.Contains(StartState))
            {
                throw new StateNotIncludedException("The state " + StartState + " is not a part of this StateManager");
            }
            unreachableStates.Remove(StartState);
            discoveredStates.Add(StartState);

            while(discoveredStates.Count != 0)
            {
                State<T> investigatingState = discoveredStates[0];//get a state we know off
                discoveredStates.Remove(investigatingState);
                foreach(State<T> foundState in investigatingState.exitStates)
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

        public List<State<T>> GetReachableStates(State<T> startState)
        {
            List<State<T>> reachedStates = new List<State<T>>();
            reachedStates.Add(startState);
            List<State<T>> discoveredStates = new List<State<T>>();
            discoveredStates.Add(startState);

            while(discoveredStates.Count != 0)
            {
                State<T> investigatingState = discoveredStates[0];//get a state we know off
                discoveredStates.Remove(investigatingState);
                foreach(State<T> foundState in investigatingState.exitStates)
                {
                    if(!reachedStates.Contains(foundState))//if the state is not in the reached states yet
                    {
                        reachedStates.Add(foundState);//Add it too the reached states
                        discoveredStates.Add(foundState);//and add it too the discovered states
                    }
                }
            }
            return reachedStates;
        }

        public List<State<T>> GetEndStates()
        {
            List<State<T>> endStates = new List<State<T>>();
            foreach(KeyValuePair<State<T>, string> stateNamePair in myStates)
            {
                if(stateNamePair.Key.exitStates.Count == 0)
                {
                    endStates.Add(stateNamePair.Key);
                }
            }
            return endStates;
        }

        public bool AreAllReachableStatesInStateMachine()
        {
            return GetReachableStates(this.startState).Count < 1;
        }

        public bool AreAllStatesReachable()
        {
            return GetUnreachableStates(this.startState).Count < 1;
        }

        public bool EnableStateMachine()
        {
            if(this.isValidStateMachine)//already validated, don't do it again.
            {
                return this.isValidStateMachine;
            }

            if(this.AreAllStatesReachable())
            {
                this.isValidStateMachine = true;
            }
            return this.isValidStateMachine;
        }


        public void DisableStateMachine()
        {
            this.isValidStateMachine = false;
            this.currentState = this.startState;
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
