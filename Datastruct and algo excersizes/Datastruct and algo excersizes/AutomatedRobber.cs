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
        public float distanceToCop = 10, wealth = 2, strength = 5;

        public AutomatedRobber()
        {
            myStateMachine = new StateManager<AutomatedRobber>(this);
            var robbingBankState = new AutomatedRobbinBankState<AutomatedRobber>();
            var fleeingState = new AutomatedFleeingState<AutomatedRobber>();
            var goodTimeState = new AutomatedHavingGoodTimeState<AutomatedRobber>();
            var layingLowState = new AutomatedLayingLowState<AutomatedRobber>();

            //connect the states with one another
            robbingBankState.exitStates.Add(goodTimeState);
            robbingBankState.exitStates.Add(fleeingState);
            layingLowState.exitStates.Add(robbingBankState);
            goodTimeState.exitStates.Add(fleeingState);
            goodTimeState.exitStates.Add(layingLowState);
            fleeingState.exitStates.Add(layingLowState);
            fleeingState.exitStates.Add(robbingBankState);

            //add my new state transitions
            layingLowState.exitStates.Add(goodTimeState);
            fleeingState.exitStates.Add(goodTimeState);

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

        public override void OnStayInState()
        {
            Console.WriteLine("I'm still robbin");
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

        public override void OnStayInState()
        {
            Console.WriteLine("Gotta go fast");
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

        public override void OnStayInState()
        {
            Console.WriteLine("Keep the good times goin!");
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

        public override void OnStayInState()
        {
            Console.WriteLine("Ima chill a bit longer");
        }
    }
}
