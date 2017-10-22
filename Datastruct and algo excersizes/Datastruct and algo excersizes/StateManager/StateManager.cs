using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datastruct_and_algo_excersizes.StateMananger
{

    /* Improvements:
     * Alter the StateMananger to evaluate the agents variables and then dictate state changes.
     * Every evaluation a stateChange should occur
     * Keep track of which state can lead into which within the mananger
     */
    class StateManager<T> : StateManagerInterface<T>
    {
        Dictionary<string, State<T>> myStates;
        State<T> startState;
        State<T> currentState;
        T agent;
        private List<State<T>> endStates;
        private bool isInEndState;
        bool isValidStateMachine = false;

        public bool _isInEndState {  get { return isInEndState; } }

        internal State<T> _currentState { get { return currentState; } }

        public StateManager(T agent)
        {
            myStates = new Dictionary<string, State<T>>();
            this.agent = agent;
        }

        public bool AddState(State<T> newState, bool isStartState = false)
        {
            if(isValidStateMachine)
            {
                throw new StateManagerAlreadyActiveException("State machine has already been validated and may not be edited anymore, " +
                    "If intended please disable the stateMachine first");
            }

            if(myStates.ContainsKey(newState._stateName))
            {
                return false;
            }

             myStates.Add(newState._stateName, newState);

            if (isStartState)
            {
                startState = newState;
            }
            return true;
        }

        public void ExecuteCurrentState()
        {
            if(!isValidStateMachine)
            {
                throw new StateManagerNotValidatedException("State machine has not been validated yet. Did you forget to validate the statemachine?");
            }

            if(this.isInEndState)   //once we know we are in an endState we won't be able to move out of it anyway
            {
                currentState.OnStayInState(this.agent);
                return;
            }

            State<T> changeState;
            if (this.currentState.EvaluateAgent(this.agent, out changeState))
            {
                this.ChangeState(changeState);
            }
            else
            {
                this.isInEndState = this.endStates.Contains(currentState);
                currentState.OnStayInState(this.agent);
            }
        }

        public void ChangeState(State<T> stateToChangeToo)
        {
            var prevState = currentState;
            currentState.OnExitState(stateToChangeToo);
            currentState = stateToChangeToo;
            currentState.OnEnterState(prevState);
        }


        public List<State<T>> GetAllStates()
        {
            List<State<T>> allStates = new List<State<T>>();
            foreach(KeyValuePair<string, State<T>> keyValuePair in myStates)
            {
                allStates.Add(keyValuePair.Value);
            }
            return allStates;
        }

        public List<State<T>> GetUnreachableStates(State<T> StartState)
        {
            List<State<T>> unreachableStates = GetAllStates();

            List<State<T>> discoveredStates = new List<State<T>>
            {
                StartState
            };
            if (!unreachableStates.Contains(StartState))
            {
                throw new StateNotIncludedException("The state " + StartState + " is not a part of this StateManager");
            }
            unreachableStates.Remove(StartState);


            while (discoveredStates.Count != 0)
            {
                State<T> investigatingState = discoveredStates[0];//get a state we know off
                discoveredStates.Remove(investigatingState);
                foreach(KeyValuePair<string, State<T>> foundState in investigatingState.exitStates)
                {
                    if(unreachableStates.Contains(foundState.Value))//if the state found is still considered unreachable
                    {
                        unreachableStates.Remove(foundState.Value);//make it no longer unreachalble
                        discoveredStates.Add(foundState.Value);//and add it too the discovered states
                    }
                }
            }

            return unreachableStates;

        }

        public List<State<T>> GetReachableStates(State<T> startState)
        {
            List<State<T>> reachedStates = new List<State<T>>
            {
                startState
            };
            List<State<T>> discoveredStates = new List<State<T>>
            {
                startState
            };
            while (discoveredStates.Count != 0)
            {
                State<T> investigatingState = discoveredStates[0];//get a state we know off
                discoveredStates.Remove(investigatingState);
                foreach(KeyValuePair<string,State<T>> foundState in investigatingState.exitStates)
                {
                    if(!reachedStates.Contains(foundState.Value))//if the state is not in the reached states yet
                    {
                        reachedStates.Add(foundState.Value);//Add it too the reached states
                        discoveredStates.Add(foundState.Value);//and add it too the discovered states
                    }
                }
            }
            return reachedStates;
        }

        public List<State<T>> GetEndStates()
        {
            List<State<T>> endStates = new List<State<T>>();
            foreach(KeyValuePair<string, State<T>> stateNamePair in myStates)
            {
                if(stateNamePair.Value.exitStates.Count == 0)
                {
                    endStates.Add(stateNamePair.Value);
                }
            }
            return endStates;
        }

        private bool AreAllReachableStatesInStateMachine()
        {
            List<State<T>> reachableStates = GetReachableStates(this.startState);
            foreach(State<T> reachableState in reachableStates)
            {
                if (!this.myStates.ContainsKey(reachableState._stateName))//if any reachable state doesn't exsist in the dictionary, return false.
                {
                    return false;
                }
            }
            return true;
        }

        private bool AreAllStatesReachable()
        {
            return GetUnreachableStates(this.startState).Count < 1;
        }

        public bool EnableStateMachine()
        {
            if(this.isValidStateMachine)//already validated, don't do it again.
            {
                return this.isValidStateMachine;
            }

            if(this.AreAllStatesReachable() && this.AreAllReachableStatesInStateMachine())
            {
                this.currentState = this.startState;
                this.endStates = GetEndStates();//cache the result in a list so we don't need to run this expensive method over and over again.
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
}
