using UnityEngine;

public struct PlayerInputs
{
    public Inputs GetKeys(Inputs currentInput)
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            return Inputs.Ctrl;
        }
        
        // todo space => jump
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            return Inputs.Shift;
        }
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            return Inputs.Move;
        }

        return currentInput;
        // todo GetKeyUp => Input.Release
    }
}


