using UnityEngine;
using System.Collections.Generic;

public class CharacterStateMachine
{
    public Inputs currentInput;
    public State currentState;

    private Stack<State> _stateStack;
    private PlayerController _characterController;

    public CharacterStateMachine(PlayerController playerController)
    {
        _characterController = playerController;
        _stateStack = new Stack<State>();
        currentState = State.Idle;
        _stateStack.Push(currentState);
    }

    public Inputs CurrentInput
    {
        set
        {
            if (value != currentInput)
            {
                currentInput = value;
                OnInputChanged();
            }
        }
    }

    private void OnInputChanged()
    {
        SetNewState(currentInput);
        _characterController.CurrentPlayerState = currentState;
    }

    public void SetNewState(Inputs newInput)
    {
        switch (newInput)
        {
            case Inputs.Move:
                switch (currentState)
                {
                    case State.Jumping:
                        break;
                    case State.Idle:
                        currentState = State.Walking;
                        _stateStack.Push(currentState);
                        break;
                    case State.Running:
                        TryPopStack();
                        ReturnToPreviousState();
                        break;
                    case State.Crouching:
                        currentState = State.Sneaking;
                        _stateStack.Push(currentState);
                        break;
                }

                break;
            case Inputs.Ctrl:
                switch (currentState)
                {
                    case State.Jumping:
                        _stateStack.Push(State.Crouching);
                        break;
                    case State.Idle:
                        currentState = State.Crouching;
                        _stateStack.Push(currentState);
                        break;
                    case State.Walking:
                        currentState = State.Sneaking;
                        _stateStack.Push(currentState);
                        break;
                    case State.Running:
                        currentState = State.Sneaking;
                        _stateStack.Push(currentState);
                        break;
                }

                break;
            case Inputs.Space:
                switch (currentState)
                {
                    case State.Crouching: 
                        break;
                    case State.Jumping: 
                        break;
                    default:
                        currentState = State.Jumping;
                        break;
                }

                break;
            case Inputs.Shift:
                switch (currentState)
                {
                    case State.Jumping:
                        _stateStack.Push(State.Running);
                        break;
                    case State.Walking:
                        currentState = State.Running;
                        _stateStack.Push(currentState);
                        break;
                }

                break;
            case Inputs.Release:
                switch (currentState)
                {
                    case State.Jumping:
                        Debug.Log("state on top before popping:" + _stateStack.Peek());
                        TryPopStack();
                        Debug.Log("stack popped, state on top: " + _stateStack.Peek());
                        break;
                    case State.Idle:
                        break;
                    default:
                        TryPopStack();
                        ReturnToPreviousState();
                        Debug.Log("key  released. returning to previous state: " + currentState);
                        break;
                }

                break;
        }
    }

    public void ReturnToPreviousState()
    {
        currentState = _stateStack.Peek();
        _characterController.CurrentPlayerState = currentState;
    }

    public void TryPopStack()
    {
        if (_stateStack.Count > 1)
        {
            _stateStack.Pop();
        }
    }
}