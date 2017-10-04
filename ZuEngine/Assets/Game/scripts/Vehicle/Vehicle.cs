using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public enum DriveType
{
	RearWheelDrive,
	FrontWheelDrive,
	AllWheelDrive
}

public class Vehicle : MonoBehaviour , ICameraTarget , IVehicle
{

	[Range(0.1f, 20f)]
	[Tooltip("Natural frequency of the suspension springs. Describes bounciness of the suspension.")]
	public float naturalFrequency = 10;

	[Range(0f, 3f)]
	[Tooltip("Damping ratio of the suspension springs. Describes how fast the spring returns back after a bounce. ")]
	public float dampingRatio = 0.8f;

	[Range(-1f, 1f)]
	[Tooltip("The distance along the Y axis the suspension forces application point is offset below the center of mass")]
	public float forceShift = 0.03f;

	[Tooltip("Adjust the length of the suspension springs according to the natural frequency and damping ratio. When off, can cause unrealistic suspension bounce.")]
	public bool setSuspensionDistance = true;

	[Tooltip("Maximum steering angle of the wheels")]
	public float maxAngle = 30f;
	[Tooltip("Maximum torque applied to the driving wheels")]
	public float maxTorque = 300f;
	[Tooltip("Maximum brake torque applied to the driving wheels")]
	public float brakeTorque = 30000f;

	[Tooltip("The vehicle's speed when the physics engine can use different amount of sub-steps (in m/s).")]
	public float criticalSpeed = 5f;
	[Tooltip("Simulation sub-steps when the speed is above critical.")]
	public int stepsBelow = 5;
	[Tooltip("Simulation sub-steps when the speed is below critical.")]
	public int stepsAbove = 1;

	[Tooltip("The vehicle's drive type: rear-wheels drive, front-wheels drive or all-wheels drive.")]
	public DriveType driveType;

	private Transform m_trans;
	private WheelCollider [] m_wheelColliders;
	private Rigidbody m_rigidbody;

	void Start () 
	{
		m_trans = gameObject.transform;
		m_wheelColliders = GetComponentsInChildren<WheelCollider> ();
		m_rigidbody = GetComponent<Rigidbody> ();
	}
		
	void Update () {
		UpdateController ();
	}

	void FixedUpdate()
	{
		OnPhysicUpdate (Time.fixedDeltaTime);
	}

	#region IVehicle implementation

	public void OnPhysicUpdate (float deltaTime)
	{
		UpdateSuspension ();
	}

	#endregion

	private void UpdateSuspension()
	{
		// Work out the stiffness and damper parameters based on the better spring model.
		foreach (WheelCollider wc in m_wheelColliders) 
		{
			JointSpring spring = wc.suspensionSpring;

			float sqrtWcSprungMass = Mathf.Sqrt (wc.sprungMass);
			spring.spring = sqrtWcSprungMass * naturalFrequency * sqrtWcSprungMass * naturalFrequency;
			spring.damper = 2f * dampingRatio * Mathf.Sqrt(spring.spring * wc.sprungMass);

			wc.suspensionSpring = spring;

			Vector3 wheelRelativeBody = transform.InverseTransformPoint(wc.transform.position);
			float distance = m_rigidbody.centerOfMass.y - wheelRelativeBody.y + wc.radius;

			wc.forceAppPointDistance = distance - forceShift;

			// Make sure the spring force at maximum droop is exactly zero
			if (spring.targetPosition > 0 && setSuspensionDistance)
				wc.suspensionDistance = wc.sprungMass * Physics.gravity.magnitude / (spring.targetPosition * spring.spring);
		}
	}

	private void UpdateController()
	{
		m_wheelColliders[0].ConfigureVehicleSubsteps(criticalSpeed, stepsBelow, stepsAbove);

		float angle = maxAngle * Input.GetAxis("Horizontal");
		float torque = maxTorque * Input.GetAxis("Vertical");

		float handBrake = Input.GetKey(KeyCode.X) ? brakeTorque : 0;

		foreach (WheelCollider wheel in m_wheelColliders)
		{
			// A simple car where front wheels steer while rear ones drive.
			if (wheel.transform.localPosition.z > 0)
				wheel.steerAngle = angle;

			if (wheel.transform.localPosition.z < 0)
			{
				wheel.brakeTorque = handBrake;
			}

			if (wheel.transform.localPosition.z < 0 && driveType != DriveType.FrontWheelDrive)
			{
				wheel.motorTorque = torque;
			}

			if (wheel.transform.localPosition.z >= 0 && driveType != DriveType.RearWheelDrive)
			{
				wheel.motorTorque = torque;
			}
				
			Quaternion q;
			Vector3 p;
			wheel.GetWorldPose (out p, out q);

			// Assume that the only child of the wheelcollider is the wheel shape.
			Transform shapeTransform = wheel.transform.GetChild (0);
			shapeTransform.position = p;
			shapeTransform.rotation = q;

		}
	}

	#region ICameraTarget implementation
	public Vector3 GetPosition ()
	{
		return m_trans.position;
	}
	public Vector3 GetForward ()
	{
		return m_trans.forward;
	}
	#endregion


}
