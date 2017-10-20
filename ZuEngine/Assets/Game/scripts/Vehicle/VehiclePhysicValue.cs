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

public class VehiclePhysicValue : MonoBehaviour 
{
	[Tooltip("The vehicle's drive type: rear-wheels drive, front-wheels drive or all-wheels drive.")]
	public DriveType DriveType;

	/*#################### Suspension  ########################*/
	[Header("Suspension")]
	[Range(0.1f, 20f)]
	[Tooltip("Natural frequency of the suspension springs. Describes bounciness of the suspension.")]
	public float NaturalFrequency = 10;

	[Range(0f, 6f)]
	[Tooltip("Damping ratio of the suspension springs. Describes how fast the spring returns back after a bounce. ")]
	public float DampingRatio = 1.6f;

	[Range(-1f, 1f)]
	[Tooltip("The distance along the Y axis the suspension forces application point is offset below the center of mass")]
	public float ForceShift = 0.03f;

	[Tooltip("Adjust the length of the suspension springs according to the natural frequency and damping ratio. When off, can cause unrealistic suspension bounce.")]
	public bool SuspensionDistance = true;

	/*#################### Wheel  ########################*/
	[Header("wheel")]
	[Tooltip("Maximum steering angle of the wheels")]
	public float MaxAngle = 30f;
	[Tooltip("Maximum torque applied to the driving wheels")]
	public float MaxTorque = 300f;
	[Tooltip("Maximum brake torque applied to the driving wheels")]
	public float brakeTorque = 30000f;
	[Tooltip("The vehicle's speed when the physics engine can use different amount of sub-steps (in m/s).")]
	public float CriticalSpeed = 5f;
	[Tooltip("Simulation sub-steps when the speed is above critical.")]
	public int StepsBelow = 5;
	[Tooltip("Simulation sub-steps when the speed is below critical.")]
	public int StepsAbove = 1;
	[Header("wheel Forward Friction")]
	public float Forward_ExtremumSlip = 0.4f;
	public float Forward_ExtremumValue = 1f;
	public float Forward_AsymptoteSlip = 0.8f;
	public float Forward_AsymptoteValue = 0.5f;
	public float Forward_Siffness = 1f;
	[Header("wheel Sideways Friction")]
	public float Sideways_ExtremumSlip = 0.2f;
	public float Sideways_ExtremumValue = 1f;
	public float Sideways_AsymptoteSlip = 0.5f;
	public float Sideways_AsymptoteValue = 0.75f;
	public float Sideways_Siffness = 1f;

	/*#################### Vehicle Force  ########################*/
	[Header("MaxSpeed")]
	[SerializeField]
	public float MaxSpeed = 15f;
	[Header("Center of Mass")]
	[SerializeField]
	public Vector3 CenterOfMass = new Vector3(0,1.4f,0.2f);
	[Header("Stable Force")]
	[SerializeField]
	public float StableForce = 10f;

	[Header("Drag")]
	[SerializeField]
	[Range(0f, 1f)]
	public float Drag = 0.5f;
	[SerializeField]
	[Range(0f, 1f)]
	public float AngularDrag = 0.9f;
	[SerializeField]
	[Range(0f, 1f)]
	public float JumpDrag = 0.1f;
	[SerializeField]
	[Range(0f, 1f)]
	public float JumpAngularDrag = 0.1f;

	[Header("Jump")]
	[SerializeField]
	public float JumpForce = 400f;
	[SerializeField]
	public float BicycleJumpForce = 20f;
	[SerializeField]
	public float BicycleJumpImpulse = 20f;

	[Header("boost")]
	[SerializeField]
	public float BoostForce = 5f;
	[SerializeField]
	public float AutoBrakeDelta = 0.8f;

	public void UpdatePhysicParam(WheelCollider wc, Rigidbody rigidBody)
	{
		rigidBody.centerOfMass = CenterOfMass;

		wc.ConfigureVehicleSubsteps(CriticalSpeed, StepsBelow, StepsAbove);

		JointSpring spring = wc.suspensionSpring;

		float sqrtWcSprungMass = Mathf.Sqrt (wc.sprungMass);
		spring.spring = sqrtWcSprungMass * NaturalFrequency * sqrtWcSprungMass * NaturalFrequency;
		spring.damper = DampingRatio * Mathf.Sqrt(spring.spring * wc.sprungMass);

		wc.suspensionSpring = spring;

		Vector3 wheelRelativeBody = transform.InverseTransformPoint(wc.transform.position);
		float distance = rigidBody.centerOfMass.y - wheelRelativeBody.y + wc.radius;

		wc.forceAppPointDistance = distance - ForceShift;

		// Make sure the spring force at maximum droop is exactly zero
		if (spring.targetPosition > 0 && SuspensionDistance)
			wc.suspensionDistance = wc.sprungMass * Physics.gravity.magnitude / (spring.targetPosition * spring.spring);

		WheelFrictionCurve forwardFriction = wc.forwardFriction;
		forwardFriction.extremumSlip = Forward_ExtremumSlip;
		forwardFriction.extremumValue = Forward_ExtremumValue;
		forwardFriction.asymptoteSlip = Forward_AsymptoteSlip;
		forwardFriction.asymptoteValue = Forward_AsymptoteValue;
		forwardFriction.stiffness = Forward_Siffness;
		wc.forwardFriction = forwardFriction;

		WheelFrictionCurve sidewaysFriction = wc.sidewaysFriction;
		sidewaysFriction.extremumSlip = Sideways_ExtremumSlip;
		sidewaysFriction.extremumValue = Sideways_ExtremumValue;
		sidewaysFriction.asymptoteSlip = Sideways_AsymptoteSlip;
		sidewaysFriction.asymptoteValue = Sideways_AsymptoteValue;
		sidewaysFriction.stiffness = Sideways_Siffness;
		wc.sidewaysFriction = sidewaysFriction;
	}

}
