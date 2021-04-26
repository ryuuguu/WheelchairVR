using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelchairMoveProvider :  UnityEngine.XR.Interaction.Toolkit.ActionBasedContinuousMoveProvider
{
    public VJHandler vjHandler;
    
    private void Start() {
        if (vjHandler == null) {
            vjHandler = GameObject.Find("VirtualJoyStick").GetComponent<VJHandler>();
        }
        
    }
    
    protected override Vector2 ReadInput()
    {
       
        var leftHandValue = leftHandMoveAction.action?.ReadValue<Vector2>() ?? Vector2.zero;
        var rightHandValue = rightHandMoveAction.action?.ReadValue<Vector2>() ?? Vector2.zero;
        Vector2 vjvalue =  vjHandler?.InputDirection ?? Vector2.zero;
        return leftHandValue + rightHandValue + vjvalue;
    }
}
