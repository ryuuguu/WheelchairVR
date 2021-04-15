using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelchairTurnProvider : UnityEngine.XR.Interaction.Toolkit.ActionBasedContinuousTurnProvider
{
    
    protected new  void Update()
    {
        // Use the input amount to scale the turn speed.
        var input = ReadInput();
        var turnAmount = GetTurnAmount(input);
        TurnRig(turnAmount);
    }

    protected override Vector2 ReadInput() {
        return new Vector2(base.ReadInput().x,0);
    }
}
