using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

/// <summary>
/// Use this class to iad in set up of  addition transitions between controller states (modes).
/// in ActionBasedControllerManager instance
/// </summary>

public class ActionBasedControllerHelper : MonoBehaviour {
    public ActionBasedControllerManager actionBasedControllerManager;
    
    static InputAction GetInputAction(InputActionReference actionReference)
    {
#pragma warning disable IDE0031 // Use null propagation -- Do not use for UnityEngine.Object types
        return actionReference != null ? actionReference.action : null;
#pragma warning restore IDE0031
    }
    public void EnableAction(InputActionReference actionReference)
    {
        var action = GetInputAction(actionReference);
        if (action != null && !action.enabled)
            action.Enable();
    }

    public void DisableAction(InputActionReference actionReference)
    {
        var action = GetInputAction(actionReference);
        if (action != null && action.enabled)
            action.Disable();
    }

    public void EnableMove() {
        EnableAction(actionBasedControllerManager.move);
    }
    
    public void DisableMove() {
        DisableAction(actionBasedControllerManager.move);
    }
}
