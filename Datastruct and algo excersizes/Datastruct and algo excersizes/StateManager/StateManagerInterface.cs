using System.Collections.Generic;

namespace Datastruct_and_algo_excersizes.StateMananger
{
    interface StateManagerInterface<T>
    {
        /* 
         * Whether or not the stateMachine has reached an end-state state
         */ 
        bool _isInEndState { get ; }

        /* 
         * Add a state into the state machine, required for validating the state machine.
         * Returns true if adding was succesfull
         * Returns false if the state._stateName already exsists in the state machine
         * Can throw StateManagerAlreadyActiveException
         */
        bool AddState(State<T> newState, bool isStartState = false);


        /*
         * Changes the state too the param state.
         * Cals the current states OnExitState method and the new states OnEnterState method
         */
        void ChangeState(State<T> stateToChangeToo);

        /*
         * Disable's the state machine to allow for changes.
         */
        void DisableStateMachine();

        /*
         * Validates the state machine. If validation was succesfull returns true
         * A valid state machine has a startState and has every state be reachable AND
         * has no reachable states not be added too the stateManager
         * Returns true if the stateMachine is valid
         * Returns false if the stateMachine is invalid
         * Note: Does NOT reset the currentState too startState if the stateMachine is already valid!
         */
        bool EnableStateMachine();

        /*
         * Execute the current state's logic via it's EvalueteAgent method
         * If Evaluate Agent returns true the currentState of the StateManager will change via the ChangeState method
         * Note: If a state change is enacted the currentState when the method is called will NOT execute it's RemainInState method.
         * Can throw StateManagerNotValidatedException
         */
        void ExecuteCurrentState();

        /*
         * Returns all states included in the state manager
         */
        List<State<T>> GetAllStates();

        /*
         * Returns all states without an exit state
         */
        List<State<T>> GetEndStates();

        /*
         * Returns all reachable states(including states that may not be included in the state mannager)
         */
        List<State<T>> GetReachableStates(State<T> startState);

        /*
         * Returns all unreachable states, great for debugging a state machine
         */
        List<State<T>> GetUnreachableStates(State<T> StartState);
    }
}
 