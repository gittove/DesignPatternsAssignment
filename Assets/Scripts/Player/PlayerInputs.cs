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
        
        // if u press wasd, then shift, then hold wasd and shift again, currentInput keeps changing and states keeps adding to stack
        //that is problem. ok thanks
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            return Inputs.Shift;
        }
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            return Inputs.Move;
        }
        
        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.LeftShift) || !Input.GetKey(KeyCode.W)
            && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            return Inputs.Release;
        }

        return currentInput;
    }
}


