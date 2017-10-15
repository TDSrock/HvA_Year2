using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datastruct_and_algo_excersizes.StateMananger;

namespace Datastruct_and_algo_excersizes
{
    class AutomatedRobber
    {
        StateManager<AutomatedRobber> myStateMachine;
        //create the agents variables.
        public float distanceToCop = 10, wealth = 2, strength = 5, feelSafe = 0;

        public AutomatedRobber()
        {
            myStateMachine = new StateManager<AutomatedRobber>(this);
            var robbingBankState = new AutomatedRobbinBankState<AutomatedRobber>();
            var fleeingState = new AutomatedFleeingState<AutomatedRobber>();
            var goodTimeState = new AutomatedHavingGoodTimeState<AutomatedRobber>();
            var layingLowState = new AutomatedLayingLowState<AutomatedRobber>();

            //connect the states with one another
            robbingBankState.AddExitState(goodTimeState);
            robbingBankState.AddExitState(fleeingState);
            layingLowState.AddExitState(robbingBankState);
            goodTimeState.AddExitState(fleeingState);
            goodTimeState.AddExitState(layingLowState);
            fleeingState.AddExitState(layingLowState);
            fleeingState.AddExitState(robbingBankState);

            //add my new state transitions
            layingLowState.AddExitState(goodTimeState);
            fleeingState.AddExitState(goodTimeState);

            //add states too the manager
            myStateMachine.AddState(fleeingState);
            myStateMachine.AddState(goodTimeState);
            myStateMachine.AddState(layingLowState);
            myStateMachine.AddState(robbingBankState, true);//this is our starting state, so pass true for the optional paramater
            try
            {
                if (!myStateMachine.EnableStateMachine())
                {
                    Console.WriteLine("State machine failed to enalbe");
                }
            }
            catch (StateNotIncludedException e)
            {
                Console.WriteLine(e.Message + "\n" + e.StackTrace);
            }
        }

        public bool _lostCop
        {
            get { return this.distanceToCop > 25; }
        }

        public bool _feelSafe
        {
            get { return this.feelSafe > 3;  }
        }

        public bool _spotCop
        {
            get { return this.distanceToCop == 0; }
        }

        public bool _isRich
        {
            get { return this.wealth > 2; }
        }

        public bool _gotRich
        {
            get { return this.wealth > 9; }
        }

        public bool _notTired
        {
            get { return this.strength > 7; }
        }

        public bool _stillRich
        {
            get { return this.wealth >= 3; }
        }

        public bool _tired
        {
            get { return this.strength < 0; }
        }

        public void EvaluateStateMachine()
        {
            this.myStateMachine.ExecuteCurrentState();
        }


    }

    //Becuase I am lazy I am just going to toss these states here. Lazyness ftw

    class AutomatedRobbinBankState<Robber> : State<Robber>, StateInterface<Robber>
        where Robber : Datastruct_and_algo_excersizes.AutomatedRobber
    {
        public AutomatedRobbinBankState() : base("RobbinBank")
        {

        }

        public override bool EvaluateAgent(Robber agent, out State<Robber> changeStateToo)
        {
            changeStateToo = null;
            if (agent._gotRich)
            {
                changeStateToo = this.exitStates["HavingGoodTime"];
                return true;
            }
            if (agent._spotCop)
            {
                changeStateToo = this.exitStates["Fleeing"];
                return true;
            }
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
            agent.strength -= 1;
            agent.feelSafe -= 1;
            agent.wealth += 1;
            Random r = new Random(Guid.NewGuid().GetHashCode());
            if(r.Next(100) > 25)
            {
                agent.distanceToCop = 0;
            }
        }
    }

    class AutomatedFleeingState<Robber> : State<Robber>, StateInterface<Robber>
        where Robber : Datastruct_and_algo_excersizes.AutomatedRobber
    {
        public AutomatedFleeingState() : base("Fleeing")
        {

        }

        public override bool EvaluateAgent(Robber agent, out State<Robber> changeStateToo)
        {
            changeStateToo = null;

            if (agent._tired)
            {
                changeStateToo = this.exitStates["LayingLow"];
                return true;
            }
            if(agent._lostCop && agent._feelSafe
            && agent._notTired && agent._stillRich)
            {
                changeStateToo = this.exitStates["HavingGoodTime"];
                return true;
            }
            if(agent._lostCop && agent._feelSafe)
            {
                changeStateToo = this.exitStates["RobbinBank"];
                return true;
            }
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
            agent.strength -= 1;
            agent.distanceToCop += 5;
            agent.feelSafe -= 1;
        }
    }

    class AutomatedHavingGoodTimeState<Robber> : State<Robber>, StateInterface<Robber>
        where Robber : Datastruct_and_algo_excersizes.AutomatedRobber
    {
        public AutomatedHavingGoodTimeState() : base("HavingGoodTime")
        {

        }

        public override bool EvaluateAgent(Robber agent, out State<Robber> changeStateToo)
        {
            changeStateToo = null;
            if (agent._spotCop)
            {
                changeStateToo = this.exitStates["Fleeing"];
                return true;
            }
            if (agent._tired)
            {
                changeStateToo = this.exitStates["LayingLow"];
                return true;
            }
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
            agent.strength -= 1;
            agent.wealth -= 1;
            Random r = new Random(Guid.NewGuid().GetHashCode());
            if (r.Next(100) > 25)
            {
                agent.distanceToCop = 0;
            }
        }
    }

    class AutomatedLayingLowState<Robber> : State<Robber>, StateInterface<Robber>
        where Robber : Datastruct_and_algo_excersizes.AutomatedRobber
    {
        public AutomatedLayingLowState() : base("LayingLow")
        {

        }

        public override bool EvaluateAgent(Robber agent, out State<Robber> changeStateToo)
        {
            changeStateToo = null;
            if(agent._feelSafe && !agent._isRich && agent._notTired)
            {
                changeStateToo = this.exitStates["RobbinBank"];
                return true;
            }
            if(agent._stillRich && agent._feelSafe && agent._notTired)
            {
                changeStateToo = this.exitStates["HavingGoodTime"];
                return true;
            }
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
            agent.strength += 3;
            agent.feelSafe += 2;
        }
    }
}
