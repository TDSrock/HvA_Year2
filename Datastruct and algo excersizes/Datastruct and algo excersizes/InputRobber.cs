using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datastruct_and_algo_excersizes.StateMananger;

namespace Datastruct_and_algo_excersizes
{
    /*
     * Class to contain the robber's variables and any references he may need to other objects
     * aswell as his respective stateMachine
     */
    class InputRobber
    {
        StateManager<InputRobber> myStateMachine;
        public string agentString;

        public InputRobber()
        {
            myStateMachine = new StateManager<InputRobber>(this);
            var robbingBankState = new InputRobbinBankState<InputRobber>();
            var fleeingState = new InputFleeingState<InputRobber>();
            var goodTimeState = new InputHavingGoodTimeState<InputRobber>();
            var layingLowState = new InputLayingLowState<InputRobber>();

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

        public void StateInput()
        {
            string[] options = new string[myStateMachine._currentState.exitStates.Count + 1];
            {
                int i = 0;
                foreach (KeyValuePair<string, State<InputRobber>> state in myStateMachine._currentState.exitStates)
                {
                    options[i++] = state.Value._stateName;
                }
                options[i] = "Do nothing";
            }//we no longer need the i value so toss it.
            string input;
            var validInput = false;
            do
            {
                Console.WriteLine("Please tell the Robber what action to take. The possible options are(No leading or trailing spaces, caps mattern \'|\' are seperators):\n" +
                    "Current action is: " + this.myStateMachine._currentState._stateName);
                foreach(string option in options)
                {
                    Console.Write(option + " | ");
                }
                Console.WriteLine();
                input = Console.ReadLine();
                if (options.Contains(input))
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Boy, that input was wrong, get your act toghther...");
                }
                Console.WriteLine();
            } while (!validInput);
            this.agentString = input;
            this.myStateMachine.ExecuteCurrentState();
        }
    }
}
