using UnityEngine;

public class CharacterStateMachine
{
    public Inputs currentInput;
    public State currentState;
    
    private int _stackPointer;
    private State[] _stateStack;

    public CharacterStateMachine()
    {
        _stateStack = new State[2];
        currentState = State.Idle;
        Push(currentState);
    }
    
    public Inputs CurrentInput
    {
        set
        {
            if (value != this.currentInput)
            {
                this.currentInput = value;
                OnInputChanged();
            }
        }
    }

    private void OnInputChanged()
    {
        SetNewState(currentInput);
    }

    public void SetNewState(Inputs newInput)
    {
        switch (newInput)
        {
            case Inputs.Move:
                switch (currentState)
                {
                    case State.Idle:
                        currentState = State.Walking;
                        break;
                    case State.Crouching:
                        currentState = State.Sneaking;
                        break;
                }

                Push(currentState);
                break;
            case Inputs.Ctrl:
                switch (currentState)
                {
                    case State.Idle:
                        currentState = State.Crouching;
                        break;
                    case State.Running:
                        currentState = State.Sneaking;
                        break;
                }

                Push(currentState);
                break;
            case Inputs.Space:
                switch (currentState)
                {
                    case State.Crouching: break;
                    default:
                        currentState = State.NotGrounded;
                        break;
                }

                Push(currentState);
                break;
            case Inputs.Shift:
                switch (currentState)
                {
                    case State.Walking:
                        currentState = State.Running;
                        break;
                }

                Push(currentState);
                break;
            case Inputs.Release:
                switch (currentState)
                {
                    default:
                        currentState = Pop();
                        break;
                }
                break;
        }
    }

    private void Push(State state)
    {
        _stateStack[_stackPointer] = state;
        _stackPointer++;
    }

    private State Pop()
    {
        _stackPointer--;
        return _stateStack[_stackPointer];
    }
}