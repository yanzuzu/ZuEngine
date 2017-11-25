using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.Service;
using ZuEngine.Utility;

public class Vehicle : MonoBehaviour , ICameraTarget , IVehicle
{
	private const float BICYCLE_JUMP_INTERVAL = 0.8f;

	private VehiclePhysicValue m_physicParam;

	private Transform m_trans;
	private WheelCollider [] m_wheelColliders;
	public WheelCollider[] WheelColliders
	{
		get{ return m_wheelColliders; }
	}

	private Rigidbody m_rigidbody;

	private float m_speed;
	public float Speed
	{
		get{ return m_speed; }
	}

	private VehicleControlData m_ctrlData = new VehicleControlData();
	public VehicleControlData CtrlData
	{
		get{ return m_ctrlData;}
		set{ m_ctrlData = value; }
	}
		
	private bool m_isOnGround = true;
	private bool m_isJumping = false;
	private float m_lastJumpTime = 0f;
	private bool m_lastBrakeState = false;

	void Awake () 
	{
		m_trans = gameObject.transform;
		m_wheelColliders = GetComponentsInChildren<WheelCollider> ();
		m_rigidbody = GetComponent<Rigidbody> ();
		m_physicParam = GetComponent<VehiclePhysicValue> ();
	}
		
	void Update () 
	{
		OnPhysicUpdate (Time.fixedDeltaTime);

		#if UNITY_EDITOR
		if( Input.GetKeyDown(KeyCode.R))
		{
			m_trans.position = new Vector3(100.0f , 0.2f, 100.0f );
			m_trans.localRotation = Quaternion.Euler(Vector3.zero);
			m_rigidbody.velocity = Vector3.zero;
			m_rigidbody.angularVelocity = Vector3.zero;
		}
		#endif
	}

	void FixedUpdate()
	{
	}
		
	#region IVehicle implementation

	public void OnPhysicUpdate (float deltaTime)
	{
		#if UNITY_EDITOR
		UpdatePhysicParam ();
		#endif
		bool isOnGround = IsOnGround ();

		if ( !m_isOnGround )
		{
			if ( m_isJumping && isOnGround)
			{
				m_isJumping = false;
			}
		}
		m_isOnGround = isOnGround;

		Boost();
		Jump();
		UpdateWheelPhysics ();
		AutoBrake ();
		LimitSpeed ();

		UpdateDrag();
	}

	#endregion
	private void LimitSpeed()
	{
		m_speed = m_rigidbody.velocity.magnitude;
		if ( m_speed > m_physicParam.MaxSpeed )
		{
			m_rigidbody.velocity *= (m_physicParam.MaxSpeed/m_speed);
		}
	}

	private void AddStableForce()
	{
		if ( !m_isOnGround )
		{
			return;
		}
		float stableForce = Mathf.Lerp (0, m_physicParam.StableForce, m_speed / m_physicParam.MaxSpeed);
		m_rigidbody.AddForce (-Vector3.up * stableForce * m_rigidbody.mass);
	}

	private void UpdateDrag()
	{
		if ( m_isOnGround )
		{
			m_rigidbody.drag = m_physicParam.Drag;
			m_rigidbody.angularDrag = m_physicParam.AngularDrag;
		}
		else
		{
			m_rigidbody.drag = m_physicParam.JumpDrag;
			m_rigidbody.angularDrag = m_physicParam.JumpAngularDrag;
		}
	}

	private void UpdatePhysicParam()
	{
		for (int i = 0; i < m_wheelColliders.Length; i++)
		{
			m_physicParam.UpdatePhysicParam (m_wheelColliders [i], m_rigidbody);
		}
	}
		
	private void AutoBrake()
	{
		if ( !m_isOnGround || m_isJumping || CtrlData.IsJump )
		{
			return;
		}

		float gas = m_ctrlData.Gas;
		if ( m_ctrlData.IsBoost )
		{
			gas = 1f;
		}

		if ( gas != 0 )
		{
			float angle = Vector3.Dot (m_rigidbody.velocity, m_trans.forward);
			if ( gas > 0 && angle < 0 || gas < 0 && angle > 0 )
			{
				SetWheelBrake (Mathf.Infinity);
			}
			else
			{
				if ( m_lastBrakeState )
				{
					SetWheelBrake (0);
				}

			}
			return;
		}

		if (!m_lastBrakeState || m_speed > m_physicParam.AutoBrakeThres)
		{
			SetWheelBrake (Mathf.Infinity);
		}

	}

	private void SetWheelBrake(float brake)
	{
		foreach (WheelCollider wheel in m_wheelColliders)
		{
			wheel.brakeTorque = brake;
		}
		m_lastBrakeState = brake == 0 ? false : true;
	}

	private void UpdateWheelPhysics()
	{
		float gas = m_ctrlData.Gas;
		if ( m_ctrlData.IsBoost )
		{
			gas = 1f;
		}
		float angle = m_physicParam.MaxAngle * m_ctrlData.TurnAxisX;
		float torque = m_physicParam.MaxTorque * gas;

		foreach (WheelCollider wheel in m_wheelColliders)
		{
			// A simple car where front wheels steer while rear ones drive.
			if (wheel.transform.localPosition.z > 0)
				wheel.steerAngle = angle;

			if (wheel.transform.localPosition.z < 0)
			{
				wheel.brakeTorque = m_ctrlData.HandBrake;
			}

			if (wheel.transform.localPosition.z < 0 && m_physicParam.DriveType != DriveType.FrontWheelDrive)
			{
				wheel.motorTorque = torque;
			}

			if (wheel.transform.localPosition.z >= 0 && m_physicParam.DriveType != DriveType.RearWheelDrive)
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

	private void Boost()
	{
		if ( !CtrlData.IsBoost )
		{
			return;
		}
		m_rigidbody.AddForce (m_rigidbody.mass * m_physicParam.BoostForce * m_trans.forward, ForceMode.Force);
	}

	private void Jump()
	{
		if ( m_isJumping )
		{
			BicycleJump ();
			return;
		}

		if ( CtrlData.IsJump )
		{
			m_lastJumpTime = Time.time;
			m_isJumping = true;
			m_rigidbody.AddForce (m_rigidbody.mass * m_physicParam.JumpForce * Vector3.up);
			CtrlData.IsJump = false;
		}
	}

	private void BicycleJump()
	{
		if ( !CtrlData.IsJump )
		{
			return;
		}

		float diffTime = Time.time - m_lastJumpTime;	
		if ( diffTime >= BICYCLE_JUMP_INTERVAL )
		{
			return;
		}

		m_rigidbody.angularVelocity *= 0.2f;
		if ( CtrlData.TurnAxisX == 0 )
		{
			m_rigidbody.AddRelativeTorque (m_rigidbody.mass * m_physicParam.BicycleJumpForce * Vector3.right , ForceMode.Acceleration);
			m_rigidbody.AddForce (m_rigidbody.mass * m_physicParam.BicycleJumpImpulse * m_trans.forward , ForceMode.Impulse);
		}
		else
		{
			float delta = CtrlData.TurnAxisX > 0 ? -1 : 1;
			m_rigidbody.AddRelativeTorque (delta * m_rigidbody.mass * m_physicParam.BicycleJumpForce * Vector3.forward , ForceMode.Acceleration);
			m_rigidbody.AddForce ( delta * m_rigidbody.mass * m_physicParam.BicycleJumpImpulse * m_trans.right, ForceMode.Impulse);
		}
		CtrlData.IsJump = false;
	}

	private bool IsOnGround()
	{
		foreach (WheelCollider wheel in m_wheelColliders)
		{
			if ( !wheel.isGrounded )
			{
				return false;
			}
		}
		return true;
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
