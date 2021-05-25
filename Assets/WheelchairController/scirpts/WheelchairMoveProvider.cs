using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelchairMoveProvider :  UnityEngine.XR.Interaction.Toolkit.ActionBasedContinuousMoveProvider {
    public WheelChairDrive wheelChairDrive;
    public VJHandler vjHandler;
    public bool useWheelDrive;
    
    private void Start() {
        if (vjHandler == null) {
            vjHandler = GameObject.Find("VirtualJoyStick")?.GetComponent<VJHandler>();
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
        var input = ReadInput();
        if (useWheelDrive) {
            wheelChairDrive.DriveWheels(input, true);
        }
        else {
            var xrRig = system.xrRig;
            if (xrRig == null)
                return;
            var motion = ComputeDesiredMove(input);
            if (CanBeginLocomotion() && BeginLocomotion()) {
                xrRig.rig.transform.position += motion;
                EndLocomotion();
            }
        }
    }
}
