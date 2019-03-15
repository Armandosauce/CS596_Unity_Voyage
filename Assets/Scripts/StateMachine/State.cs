using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    private StateMachine Machine { get; set; }

    public State()
    {

    }

    public static implicit operator bool(State state)
    {
        return state != null;
    }

    public virtual void OnStateInitialize(StateMachine machine)
    {
        Machine = machine;
    }

    public virtual void OnStateEnter()
    {

    }

    public virtual void OnStateExit()
    {

    }

    public virtual void Update()
    {

    }
}