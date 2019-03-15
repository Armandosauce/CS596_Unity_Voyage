﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public StateMachine Machine;

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