using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelchairWheelMoveProvider :  UnityEngine.XR.Interaction.Toolkit.ActionBasedContinuousMoveProvider {
    public WheelChairDrive wheelChairDrive;
    public VJHandler vjHandler;
    public bool disableControl;
    
    
    private void Start() {
        if (vjHandler == null) {
            vjHandler = GameObject.Find("VirtualJoyStick").GetComponent<VJHandler>();
        }
        
    }
    
    protected override Vector2 ReadInput()
    {
        var leftHandValue = leftHandMoveAction.action?.ReadValue<Vector2>() ?? Vector2.zero;
        var rightHandValue = rightHandMoveAction.action?.ReadValue<Vector2>() ?? Vector2.zero;
        Vector2 vjvalue = vjHandler?.InputDirection ?? Vector2.zero;
        var v2 = leftHandValue + rightHandValue + vjvalue;
       
        return v2;
    }
    
    protected new void Update() {
        var input = Vector2.zero;
        if (!disableControl) {
            input = ReadInput();
        }
        wheelChairDrive.DriveWheels(input,  true);
    }
}
