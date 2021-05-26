using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class WheelChairDrive : MonoBehaviour
{
   
	[Tooltip("Maximum RPM of driving wheels as RPM approaches max torque decrease towards 0")]
	public float maxRPM = 20f;
	[Tooltip("Maximum torque applied to the driving wheels")]
	public float maxTorque = 300f;
	[Tooltip("Maximum brake torque applied to the driving wheels")]
	public float brakeTorque = 30000f;
	[Tooltip("If you need the visual wheels to be attached automatically, drag the wheel shape here.")]
	public GameObject wheelShape;
	[Tooltip("Sensitifity rounding factor")] 
	public float roundingFactor = 10;
	

	[Tooltip("Radius of wheel shape")] 
	public Vector3 wheelScale = Vector3.one;
	[Tooltip("Create wheel shape")]
	public bool createWheelShapes = true;

	[Tooltip("The vehicle's speed when the physics engine can use different amount of sub-steps (in m/s).")]
	public float criticalSpeed = 5f;
	[Tooltip("Simulation sub-steps when the speed is above critical.")]
	public int stepsBelow = 5;
	[Tooltip("Simulation sub-steps when the speed is below critical.")]
	public int stepsAbove = 1;

    public WheelCollider[] wheels;

    private Rigidbody _rigidbody;
    
    private readonly float _cosN45 = Mathf.Cos(Mathf.Deg2Rad*-45);
    private readonly float _sinN45 = Mathf.Sin(Mathf.Deg2Rad*-45);
	void Start() {
		CreateWheelShapes();
		_rigidbody = GetComponent<Rigidbody>();
	}

	public void CreateWheelShapes() {
		for (int i = 0; i < wheels.Length; ++i) {
			var wheel = wheels[i];

			// Create wheel shapes only when needed.
			if (wheelShape != null && createWheelShapes) {
				var ws = Instantiate(wheelShape);
				ws.transform.parent = wheel.transform;
				ws.transform.localScale = new
					Vector3(wheelScale.x, wheel.radius / wheelScale.y, wheel.radius / wheelScale.z);
				ws.transform.localPosition = Vector3.zero;
			}
		}
	}

	// This is a really simple approach to updating wheels.
	void Update()
	{
		wheels[0].ConfigureVehicleSubsteps(criticalSpeed, stepsBelow, stepsAbove);
	}

	public void DriveWheels(Vector2 v2, bool joystick = true) {
		Debug.Log("DriveWheels");
		v2 = new Vector2(Mathf.Round(v2.x *  roundingFactor) /  roundingFactor,
			Mathf.Round(v2.y * roundingFactor) / roundingFactor);
		if (!joystick) {
			DriveWheels(v2.x, v2.y);
		}
		else {
			//rotate -45 degrees to get r&l wheel 
			Vector2 rotated = new Vector2(v2.x * _cosN45 - v2.y * _sinN45,
				v2.x * _sinN45 + v2.y * _cosN45);
			// if going in reverse switch r&l wheel drive
			if (v2.y < 0 ) {
				rotated = new Vector2(rotated.y, rotated.x);
			}
			DriveWheels(rotated.x,rotated.y);
		}
	}
	
	public void DriveWheels(float wheel0Input, float wheel1Input) {
		float torque0 = maxTorque * wheel0Input;
		float torque1 = maxTorque * wheel1Input;

		bool brake = (Mathf.Abs(wheel0Input) < 0.05f) && (Mathf.Abs(wheel1Input) < 0.05f);

		float handBrake = brake ? brakeTorque : 0;

		wheels[0].brakeTorque = handBrake;
		wheels[1].brakeTorque = handBrake;

		wheels[0].motorTorque = torque0 * (Mathf.Max(maxRPM - Mathf.Abs(wheels[0].rpm), 0.001f)) / maxRPM;
		wheels[1].motorTorque = torque1 * (Mathf.Max(maxRPM - Mathf.Abs(wheels[1].rpm), 0.001f)) / maxRPM;

		//For turning speed this are good variables to watch
		//Debug.Log("RPM: " + wheels[0].rpm + " : " + wheels[1].rpm + " :: " 
		//          +wheel0Input  + " : " + wheel1Input);
		//Debug.Log("RPM: "+ wheels[0].rpm + " : " + wheels[0].motorTorque 
		//          + " : "+ _rigidbody.velocity.magnitude);
		
		foreach (WheelCollider wheel in wheels) {
			// Update visual wheels if any.
			if (wheelShape) {
				Quaternion q;
				Vector3 p;
				wheel.GetWorldPose(out p, out q);

				// Assume that the only child of the wheelcollider is the wheel shape.
				Transform shapeTransform = wheel.transform.GetChild(0);

				if (wheel.name == "a0l" || wheel.name == "a1l" || wheel.name == "a2l") {
					shapeTransform.rotation = q * Quaternion.Euler(0, 180, 0);
					shapeTransform.position = p;
				}
				else {
					shapeTransform.position = p;
					shapeTransform.rotation = q;
				}
			}
		}
		
	}
}
