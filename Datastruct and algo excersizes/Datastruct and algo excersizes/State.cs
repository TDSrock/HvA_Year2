using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datastruct_and_algo_excersizes
{
    class State<T>
    {
        public List<State<T>> exitStates;
        private string stateName;

        public string _stateName
        {
            get { return this.stateName; }
        }

        public State(string name)
        {
            this.stateName = name;
        }

        public virtual bool evaluateWorld(T agent, out State<T> changeStateToo)
        {
            changeStateToo = null;
            return false;

        }

        public virtual void OnExitState()
        {

        }

        public virtual void OnEnterState()
        {

        }

        public virtual void OnStayInState()
        {

        }

    }
}
