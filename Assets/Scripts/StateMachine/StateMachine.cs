using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateMachine : MonoBehaviour
{

    protected List<State> statesList = new List<State>();
    protected State currentState;

    void Update()
    {
        currentState.Update();
    }

    /// <summary>
    /// Switch the currentState to a specific State object
    /// </summary>
    /// <param name="state">
    /// The state object to set as the currentState</param>
    /// <returns>Whether the state was changed</returns>
    protected virtual bool SwitchState(State state)
    {
        bool success = false;
        if (state && state != currentState)
        {
            if (currentState)
                currentState.OnStateExit();
            currentState = state;
            currentState.OnStateEnter();
            success = true;
        }
        return success;
    }

    /// <summary>
    /// Switch the currentState to a State of a the given type.
    /// </summary>
    /// <typeparam name="StateType">
    /// The type of state to use for the currentState</typeparam>
    /// <returns>Whether the state was changed</returns>
    public virtual bool SwitchState<StateType>() where StateType : State, new()
    {
        bool success = false;
        bool found = false;
        //if the state can be found in the list of states 
        //already created, switch to the existing version
        foreach (State state in statesList)
        {
            if (state is StateType)
            {
                found = true;
                success = SwitchState(state);
                break;
            }
        }
        //if the state is not found in the list, 
        //make a new instance
        if (!found)
        {
            State newState = new StateType();
            newState.OnStateInitialize(this);
            statesList.Add(newState);
            success = SwitchState(newState);
        }
        return success;
    }
}