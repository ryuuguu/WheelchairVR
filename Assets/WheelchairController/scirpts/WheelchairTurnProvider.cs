using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelchairTurnProvider : UnityEngine.XR.Interaction.Toolkit.ActionBasedContinuousTurnProvider
{
    public VJHandler vjHandler;
    
    private void Start() {
        if (vjHandler == null) {
            vjHandler = GameObject.Find("VirtualJoyStick")?.GetComponent<VJHandler>();
        }
    }
    protected void FixedUpdate()
    {
        // Use the input amount to scale the turn speed.
        var input = ReadInput();
        var turnAmount = GetTurnAmount(input);
        TurnRig(turnAmount);
    }

    protected override Vector2 ReadInput() {
        Vector2 vjvalue =  vjHandler?.InputDirection ?? Vector2.zero;
        return new Vector2((base.ReadInput()+vjvalue).x,0) ;
    }
}
