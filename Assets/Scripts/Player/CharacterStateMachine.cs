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
                        PushState(currentState);
                        break;
                    case State.Running:
                        TryPopStack();
                        ReturnToPreviousState();
                        break;
                    case State.Crouching:
                        currentState = State.Sneaking;
                        PushState(currentState);
                        break;
                }

                break;
            case Inputs.Ctrl:
                switch (currentState)
                {
                    case State.Jumping:
                        PushState(State.Crouching);
                        break;
                    case State.Idle:
                        currentState = State.Crouching;
                        PushState(currentState);
                        break;
                    case State.Walking:
                        currentState = State.Sneaking;
                        PushState(currentState);
                        break;
                    case State.Running:
                        currentState = State.Sneaking;
                        PushState(currentState);
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
                        PushState(State.Running);
                        break;
                    case State.Walking:
                        currentState = State.Running;
                        PushState(currentState);
                        break;
                }

                break;
            case Inputs.Release:
                switch (currentState)
                {
                    case State.Jumping:
                        TryPopStack();
                        break;
                    case State.Idle:
                        break;
                    default:
                        TryPopStack();
                        ReturnToPreviousState();
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

    public void PushState(State stateToStack)
    {
        if (stateToStack == _stateStack.Peek())
        {
            return;
        }

        _stateStack.Push(stateToStack);
    }
}