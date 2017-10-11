namespace Datastruct_and_algo_excersizes
{
    interface StateInterface<T>
    {
        string _stateName { get; }

        /*
         * Called if the state is being evaulted.
         * Use the Agent param to acces variables from the object this state belongs too
         * if a stateChange needs to happen, return true
         * Use the changeStateToo param to tell to which state needs to be switched
         */
        bool EvaluateAgent(T agent, out State<T> changeStateToo);

        /*
         * Called when entering this state
         * Use the prevState param to custimize behavior based on the prevState
         */
        void OnEnterState(State<T> prevState);

        /*
         * Called when leaving this state, end state's don't have to implement this
         * Use the nextState param to custimize behavior based on the nextState
         */
        void OnExitState(State<T> nextState);

        /*
         * Called if no state transsition occurs on this evaluation
         */
        void OnStayInState();
    }
}