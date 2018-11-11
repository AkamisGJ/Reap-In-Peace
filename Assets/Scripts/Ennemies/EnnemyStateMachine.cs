using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum State
{
    Patrolling,
    Alerted,
    Seeking,
    Win,
    Dead
}

public enum Command
{
    Patrol,
    Alert,
    LoseTrack,
    Seek,
    Win,
    Die
}

public class EnnemyStateMachine {

    Dictionary<StateTransition, State> transitions;
    public State currentState { get; private set; }

    public EnnemyStateMachine()
    {
        currentState = State.Patrolling;
        transitions = new Dictionary<StateTransition, State>()
        {
            { new StateTransition(State.Patrolling, Command.Win),      State.Win },
            { new StateTransition(State.Alerted,    Command.Win),      State.Win },
            { new StateTransition(State.Seeking,    Command.Win),      State.Win },

            { new StateTransition(State.Patrolling, Command.Die),       State.Dead },
            { new StateTransition(State.Alerted,    Command.Die),       State.Dead },
            { new StateTransition(State.Seeking,    Command.Die),       State.Dead },
            //{ new StateTransition(State.Win,        Command.Die),       State.Dead },

            { new StateTransition(State.Patrolling, Command.Alert),     State.Alerted },
            { new StateTransition(State.Seeking,    Command.LoseTrack), State.Alerted },

            { new StateTransition(State.Patrolling, Command.Seek),      State.Seeking },
            { new StateTransition(State.Alerted,    Command.Seek),      State.Seeking },

            { new StateTransition(State.Alerted,    Command.Patrol),    State.Patrolling },

        };
    }

    public State GetNext(Command command)
    {
        StateTransition transition = new StateTransition(currentState, command);
        State nextState;

        if (!transitions.TryGetValue(transition, out nextState))
        {
            //Debug.Log("Invalid transition: " + currentState + " -> " + command);
            return currentState;
        }

        return nextState;
    }

    public State MoveNext(Command command)
    {
        currentState = GetNext(command);
        Debug.Log("State : " + currentState.ToString());
        return currentState;
    }

    class StateTransition
    {
        readonly State currentState;
        readonly Command command;

        public StateTransition(State currentState, Command command)
        {
            this.currentState = currentState;
            this.command = command;
        }

        public override int GetHashCode()
        {
            return 17 + 31 * currentState.GetHashCode() + 31 * command.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            StateTransition other = obj as StateTransition;
            return other != null && this.currentState == other.currentState && this.command == other.command;
        }
    }
}
