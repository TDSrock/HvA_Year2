using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datastruct_and_algo_excersizes
{
    class State
    {
        public List<State> exitStates;
        private string stateName;

        public string _stateName
        {
            get { return this.stateName; }
        }

        public State(string name)
        {
            this.stateName = name;
        }

        public void evalState()
        {

        }

        public void OnExitState()
        {

        }

        public void OnEnterState()
        {

        }

        public void OnStayInState()
        {

        }

    }
}
