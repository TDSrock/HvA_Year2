using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datastruct_and_algo_excersizes.StateMananger
{
    /* Improvements:
     * Alter the state class to no longer require instance of T
     * and exitStates
     */
    abstract class State<T> : StateInterface<T>
    {
        public Dictionary<string, State<T>> exitStates;
        private string stateName;

        public string _stateName
        {
            get { return this.stateName; }
        }

        public State(string name)
        {
            this.exitStates = new Dictionary<string, State<T>>();
            this.stateName = name;
        }

        public virtual bool EvaluateAgent(T agent, out State<T> changeStateToo)
        {
            changeStateToo = null;
            return false;
        }

        public virtual void OnExitState(State<T> nextState)
        {

        }

        public virtual void OnEnterState(State<T> prevState)
        {

        }

        public virtual void OnStayInState(T agent)
        {

        }
        public void AddExitState(State<T> stateToAdd)
        {
            this.exitStates.Add(stateToAdd._stateName, stateToAdd);
        }

    }
}
