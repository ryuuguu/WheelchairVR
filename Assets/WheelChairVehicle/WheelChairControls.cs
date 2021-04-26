using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelChairControls : MonoBehaviour {
    public WheelChairDrive wheelChairDrive;
    public VJHandler vjHandler;

    private void Start() {
        if (wheelChairDrive == null) {
            wheelChairDrive = GameObject.Find("WheelChairAvatar").GetComponent<WheelChairDrive>();
        }
        
    }

    // Update is called once per frame
    void Update() 
    {
        /*
        float wheel0Input = Input.GetAxis("Horizontal");
        float wheel1Input = Input.GetAxis("Vertical");
        wheelChairDrive.DriveWheels(wheel0Input, wheel1Input);
        */
        wheelChairDrive.DriveWheels(vjHandler.InputDirection, true);
    }
}
