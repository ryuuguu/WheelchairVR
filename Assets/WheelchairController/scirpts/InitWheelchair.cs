using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitWheelchair : MonoBehaviour
{
    public Transform cameraOffset;
    public Transform headCamera;
    public void setForwardAngle() {
        cameraOffset.eulerAngles += new Vector3(0, -1*headCamera.eulerAngles.y, 0);
        gameObject.SetActive(false);
    }
}
