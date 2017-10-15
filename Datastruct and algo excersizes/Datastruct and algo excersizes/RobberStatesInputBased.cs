using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datastruct_and_algo_excersizes.StateMananger;

namespace Datastruct_and_algo_excersizes
{
    class InputRobbinBankState<Robber> : State<Robber>, StateInterface<Robber>
        where Robber : Datastruct_and_algo_excersizes.InputRobber
    {
        public InputRobbinBankState() : base("RobbinBank")
        {

        }

        public override bool EvaluateAgent(Robber agent, out State<Robber> changeStateToo)
        {
            changeStateToo = null;
            if(agent.agentString.Equals("Do nothing"))
            {
                return false;
            }
            foreach(KeyValuePair<string, State<Robber>> state in this.exitStates)
            {
                if (state.Value._stateName.Equals(agent.agentString))
                {
                    changeStateToo = state.Value;
                    return true;
                }
            }
            Console.WriteLine("Error: input fell through, something went wrong in the statemanager debug info:\n" +
                "{0} State, {1} agentString", this, agent.agentString);
            return false;
        }

        public override void OnEnterState(State<Robber> prevState)
        {
            Console.WriteLine("It's robbin time");
        }

        public override void OnExitState(State<Robber> nextState)
        {
            Console.WriteLine("I aint robbin no more");
        }

        public override void OnStayInState(Robber agent)
        {
            Console.WriteLine("I'm still robbin");
        }
    }

    class InputFleeingState<Robber> : State<Robber>, StateInterface<Robber>
        where Robber : Datastruct_and_algo_excersizes.InputRobber
    {
        public InputFleeingState() : base("Fleeing")
        {

        }

        public override bool EvaluateAgent(Robber agent, out State<Robber> changeStateToo)
        {
            changeStateToo = null;
            if (agent.agentString.Equals("Do nothing"))
            {
                return false;
            }
            foreach (KeyValuePair<string, State<Robber>> state in this.exitStates)
            {
                if (state.Value._stateName.Equals(agent.agentString))
                {
                    changeStateToo = state.Value;
                    return true;
                }
            }
            Console.WriteLine("Error: input fell through, something went wrong in the statemanager debug info:\n" +
                "{0} State, {1} agentString", this, agent.agentString);
            return false;
        }

        public override void OnEnterState(State<Robber> prevState)
        {
            Console.WriteLine("Fuck this, time to bolt");
        }

        public override void OnExitState(State<Robber> nextState)
        {
            Console.WriteLine("I aint runnin no more");
        }

        public override void OnStayInState(Robber agent)
        {
            Console.WriteLine("Gotta go fast");
        }
    }

    class InputHavingGoodTimeState<Robber> : State<Robber>, StateInterface<Robber>
        where Robber : Datastruct_and_algo_excersizes.InputRobber
    {
        public InputHavingGoodTimeState() : base("HavingGoodTime")
        {

        }

        public override bool EvaluateAgent(Robber agent, out State<Robber> changeStateToo)
        {
            changeStateToo = null;
            if (agent.agentString.Equals("Do nothing"))
            {
                return false;
            }
            foreach (KeyValuePair<string, State<Robber>> state in this.exitStates)
            {
                if (state.Value._stateName.Equals(agent.agentString))
                {
                    changeStateToo = state.Value;
                    return true;
                }
            }
            Console.WriteLine("Error: input fell through, something went wrong in the statemanager debug info:\n" +
                "{0} State, {1} agentString", this, agent.agentString);
            return false;
        }

        public override void OnEnterState(State<Robber> prevState)
        {
            Console.WriteLine("Time to have a good time");
        }

        public override void OnExitState(State<Robber> nextState)
        {
            Console.WriteLine("I aint havin a good time no more");
        }

        public override void OnStayInState(Robber agent)
        {
            Console.WriteLine("Keep the good times goin!");
        }
    }

    class InputLayingLowState<Robber> : State<Robber>, StateInterface<Robber>
        where Robber : Datastruct_and_algo_excersizes.InputRobber
    {
        public InputLayingLowState() : base("LayingLow")
        {

        }

        public override bool EvaluateAgent(Robber agent, out State<Robber> changeStateToo)
        {
            changeStateToo = null;
            if (agent.agentString.Equals("Do nothing"))
            {
                return false;
            }
            foreach (KeyValuePair<string, State<Robber>> state in this.exitStates)
            {
                if (state.Value._stateName.Equals(agent.agentString))
                {
                    changeStateToo = state.Value;
                    return true;
                }
            }
            Console.WriteLine("Error: input fell through, something went wrong in the statemanager, debug info:\n" +
                "{0} State, {1} agentString", this, agent.agentString);
            return false;
        }

        public override void OnEnterState(State<Robber> prevState)
        {
            Console.WriteLine("A shit, time to Duck down");
        }

        public override void OnExitState(State<Robber> nextState)
        {
            Console.WriteLine("I aint chillin no more");
        }

        public override void OnStayInState(Robber agent)
        {
            Console.WriteLine("Ima chill a bit longer");
        }
    }
}
