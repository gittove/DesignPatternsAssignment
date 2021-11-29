using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private State _activeState;
    private Inputs _currentInput;

    private enum State
    {
        Idle, 
        MoveLeft, 
        MoveRight,
        Crouch, 
        SneakLeft,
        SneakRight,
        Jump, 
        RunLeft,
        RunRight
    }

    private enum Inputs
    {
        Down = KeyCode.S, 
        Left = KeyCode.A,
        Right = KeyCode.D,
        Space = KeyCode.Space, 
        Shift = KeyCode.LeftShift, 
        Release = KeyCode.None
    }

    void Start()
    {
        _activeState = State.Idle;
    }

    void GetInputs()
    {
        if (Input.GetKey("S"))
        {
            _currentInput = Inputs.Down;
            HandleInput(_currentInput);
        }
        if (Input.GetKey("A"))
        {
            _currentInput = Inputs.Left;
            HandleInput(_currentInput);
        }
        if (Input.GetKey("D"))
        {
            _currentInput = Inputs.Right;
            HandleInput(_currentInput);
        }
        if (Input.GetKey("LeftShift"))
        {
            _currentInput = Inputs.Shift;
            HandleInput(_currentInput);
        }

        // todo handle keyUp
        
        if (Input.GetKey("Space"))
        {
            _currentInput = Inputs.Space;
            HandleInput(_currentInput);
        }
    }

    private void Update()
    {
     //   HandleInput();
    }

    void HandleInput(Inputs inputs)
    {
        // if-checks på input here
        
        switch (_activeState)
        {
            case State.Idle:
                if (inputs == Inputs.Down)
                {
                    _activeState = State.Crouch;
                }
                else if (inputs == Inputs.Left)
                {
                    _activeState = State.MoveLeft;
                }
                else if (inputs == Inputs.Right)
                {
                    _activeState = State.MoveRight;
                }
                
                break;
            
            case State.MoveLeft:
                if (inputs == Inputs.Shift)
                {
                    _activeState = State.RunLeft;
                }
                
                else if (inputs == Inputs.Space)
                {
                    _activeState = State.Jump;
                    // you don't need to change the state, just insert the jump method call here.
                }

                else if (inputs == Inputs.Release)
                {
                    _activeState = State.Idle;
                }

                break;
            
            case State.MoveRight:
                if (inputs == Inputs.Shift)
                {
                    _activeState = State.RunRight;
                }
                
                else if (inputs == Inputs.Space)
                {
                    _activeState = State.Jump;
                    // you don't need to change the state, just insert the jump method call here.
                }

                else if (inputs == Inputs.Release)
                {
                    _activeState = State.Idle;
                }

                break;
            
            case State.Crouch:
                if (inputs == Inputs.Left)
                {
                    _activeState = State.SneakLeft;
                }
                else if (inputs == Inputs.Right)
                {
                    _activeState = State.SneakRight;
                }

                break;
        }
    }
}
