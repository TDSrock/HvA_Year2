using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datastruct_and_algo_excersizes.StateMananger;

namespace Datastruct_and_algo_excersizes
{
    class AutomatedCop
    {
        public CapturableAutomatedRobber robber;
        public CapturableAutomatedRobber chasing;
        public float onDutyTime = 0;
        StateManager<AutomatedCop> myStateMachine;

        public bool _goOffDuty
        {
            get { return this.onDutyTime > 12; }
        }

        public bool _goOnnDuty
        {
            get { return this.onDutyTime <= 0; }
        }

        public bool _capturedTarget
        {
            get { return this.chasing._isCaptured; }
        }

        public AutomatedCop(CapturableAutomatedRobber robber)
        {
            this.robber = robber;

            myStateMachine = new StateManager<AutomatedCop>(this);

            //build states
            var stakeOutState = new AutomatedCopStakeOutState<AutomatedCop>();
            var offDutyState = new AutomatedCopOffDutyState<AutomatedCop>();
            var chasingState = new AutomatedCopChasingState<AutomatedCop>();

            //connect states
            stakeOutState.AddExitState(chasingState);
            stakeOutState.AddExitState(offDutyState);
            offDutyState.AddExitState(stakeOutState);
            chasingState.AddExitState(stakeOutState);
            chasingState.AddExitState(offDutyState);

            //add states too stateMachine
            myStateMachine.AddState(stakeOutState, true);
            myStateMachine.AddState(offDutyState);
            myStateMachine.AddState(chasingState);

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

    class AutomatedCopStakeOutState<Cop> : State<Cop>, StateInterface<Cop>
        where Cop : Datastruct_and_algo_excersizes.AutomatedCop
    {
        public AutomatedCopStakeOutState() : base("StakeOut")
        {
        }

        public override bool EvaluateAgent(Cop agent, out State<Cop> changeStateToo)
        {
            changeStateToo = null;
            if (agent.chasing != null)
            {
                changeStateToo = this.exitStates["Chasing"];
                return true;
            }
            if (agent._goOffDuty)
            {
                changeStateToo = this.exitStates["OffDuty"];
                return true;
            }
            return false;
        }

        public override void OnEnterState(State<Cop> prevState)
        {
            Console.WriteLine("Time for active duty");
        }

        public override void OnExitState(State<Cop> nextState)
        {
            if(nextState._stateName.Equals("OffDuty"))
                Console.WriteLine("I've been copping too long");
            if (nextState._stateName.Equals("Chasing"))
                Console.WriteLine("Suspect spotted, engaging");
        }

        public override void OnStayInState(Cop agent)
        {
            Console.WriteLine("Stakin out places, trying to find trouble");
            agent.onDutyTime += 1;
            if (agent.robber._getCurrentState._stateName.Equals("RobbinBank") || agent.robber._getCurrentState._stateName.Equals("HavingGoodTime"))
            {
                Random r = new Random(Guid.NewGuid().GetHashCode());
                if (r.Next(100) > 25)
                {
                    agent.chasing = agent.robber;
                }
            }
        }
    }

    class AutomatedCopOffDutyState<Cop> : State<Cop>, StateInterface<Cop>
        where Cop : Datastruct_and_algo_excersizes.AutomatedCop
    {
        public AutomatedCopOffDutyState() : base("OffDuty")
        {
        }

        public override bool EvaluateAgent(Cop agent, out State<Cop> changeStateToo)
        {
            changeStateToo = null;
            if (agent._goOnnDuty)
            {
                changeStateToo = this.exitStates["StakeOut"];
                return true;
            }
            return false;
        }

        public override void OnEnterState(State<Cop> prevState)
        {
            Console.WriteLine("Active duty no longer, headed home");
        }

        public override void OnExitState(State<Cop> nextState)
        {
            Console.WriteLine("Back to Active duty...");
        }

        public override void OnStayInState(Cop agent)
        {
            Console.WriteLine("The cop drama shows on TV nowadays are so unrealistic....");
            agent.onDutyTime -= 2;
            Random r = new Random(Guid.NewGuid().GetHashCode());
        }
    }

    class AutomatedCopChasingState<Cop> : State<Cop>, StateInterface<Cop>
        where Cop : Datastruct_and_algo_excersizes.AutomatedCop
    {
        public AutomatedCopChasingState() : base("Chasing")
        {
        }

        public override bool EvaluateAgent(Cop agent, out State<Cop> changeStateToo)
        {
            changeStateToo = null;
            if (agent._goOffDuty)
            {
                changeStateToo = this.exitStates["OffDuty"];
                agent.chasing = null;
                return true;
            }
            if (agent.chasing._isCaptured)
            {
                changeStateToo = this.exitStates["OffDuty"];
                agent.chasing = null;
                return true;
            }
            return false;
        }

        public override void OnEnterState(State<Cop> prevState)
        {
            Console.WriteLine("Time for active duty");
        }

        public override void OnExitState(State<Cop> nextState)
        {
            if (nextState._stateName.Equals("OffDuty"))
                Console.WriteLine("I've been copping too long");
            if (nextState._stateName.Equals("Chasing"))
                Console.WriteLine("Suspect spotted, engaging");
        }

        public override void OnStayInState(Cop agent)
        {
            Console.WriteLine("I'm getting closer too the purp " + agent.chasing.distanceToCop);
            if (agent.chasing._getCurrentState._stateName.Equals("Fleeing"))
            {
                Console.WriteLine("I am in HOT pursuit, I do not need backup as I am a badass!");
            }
            else
            {
                Console.WriteLine("The dummy doesn't even know I am on his tail");
            }
            agent.onDutyTime += 1;
            agent.chasing.distanceToCop -= 8;
        }
    }
}
