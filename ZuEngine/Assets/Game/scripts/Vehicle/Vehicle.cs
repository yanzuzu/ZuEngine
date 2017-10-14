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
	private Rigidbody m_rigidbody;

	private float m_turnAxisX = 0f;
	private float m_gas = 0f;
	private float m_handBrake = 0f;
	private bool m_isBoost = false;

	private bool m_isOnGround = true;
	private bool m_isJump = false;
	private bool m_isJumping = false;

	private float m_lastJumpTime = 0f;

	void Start () 
	{
		m_trans = gameObject.transform;
		m_wheelColliders = GetComponentsInChildren<WheelCollider> ();
		m_rigidbody = GetComponent<Rigidbody> ();
		m_physicParam = GetComponent<VehiclePhysicValue> ();

		RegisterEvent ();
	}
		
	void Update () {
		UpdateController ();

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
		OnPhysicUpdate (Time.fixedDeltaTime);
	}

	private void RegisterEvent()
	{
		EventService.Instance.RegisterEvent (EventIDs.UI_CONTROLLER_LEFT_ENTER, OnLeftBtnEnter);
		EventService.Instance.RegisterEvent (EventIDs.UI_CONTROLLER_RIGHT_ENTER, OnRightBtnEnter);
		EventService.Instance.RegisterEvent (EventIDs.UI_CONTROLLER_GAS_ENTER, OnGasBtnEnter);
		EventService.Instance.RegisterEvent (EventIDs.UI_CONTROLLER_BACK_ENTER, OnBackBtnEnter);
	}

	EventResult OnLeftBtnEnter(object eventData)
	{
		m_turnAxisX = (bool)eventData ? -1f : 0f;
		return null;
	}

	EventResult OnRightBtnEnter(object eventData)
	{
		m_turnAxisX = (bool)eventData ? 1f : 0f;
		return null;
	}

	EventResult OnGasBtnEnter(object eventData)
	{
		m_gas = (bool)eventData ? 1f : 0f;
		return null;
	}

	EventResult OnBackBtnEnter(object eventData)
	{
		m_gas = (bool)eventData ? -1f : 0f;
		return null;
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

		UpdateWheelPhysics (m_gas,m_turnAxisX,m_handBrake);
		Jump();
		Boost();

		m_isOnGround = isOnGround;
	}

	#endregion
	private void UpdatePhysicParam()
	{
		for (int i = 0; i < m_wheelColliders.Length; i++)
		{
			m_physicParam.UpdateWheelPhysicParam (m_wheelColliders [i], m_rigidbody);
		}
	}

	private void UpdateController()
	{
		#if UNITY_EDITOR
		m_turnAxisX = Input.GetAxis("Horizontal");
		m_gas = Input.GetAxis("Vertical");
		m_handBrake = Input.GetKey(KeyCode.X) ? m_physicParam.brakeTorque : 0;

		if(UnityEngine.Input.GetKeyDown(KeyCode.Space))
		{
			m_isJump = true;
		}else
		{
			m_isJump = false;
		}

		m_isBoost = Input.GetKey(KeyCode.B);
		#endif


	}

	private void UpdateWheelPhysics(float gas, float axisX, float handBrake)
	{
		float angle = m_physicParam.MaxAngle * axisX;
		float torque = m_physicParam.MaxTorque * gas;

		foreach (WheelCollider wheel in m_wheelColliders)
		{
			// A simple car where front wheels steer while rear ones drive.
			if (wheel.transform.localPosition.z > 0)
				wheel.steerAngle = angle;

			if (wheel.transform.localPosition.z < 0)
			{
				wheel.brakeTorque = handBrake;
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
		if ( !m_isBoost )
		{
			return;
		}
		m_rigidbody.AddForce (m_rigidbody.mass * m_physicParam.BoostForce * m_trans.forward, ForceMode.Impulse);
	}

	private void Jump()
	{
		if ( m_isJumping )
		{
			BicycleJump ();
			return;
		}

		if ( m_isJump )
		{
			m_lastJumpTime = Time.time;
			m_isJumping = true;
			m_rigidbody.AddForce (m_rigidbody.mass * m_physicParam.JumpForce * Vector3.up);
			m_isJump = false;
		}
	}

	private void BicycleJump()
	{
		if ( !m_isJump )
		{
			return;
		}

		float diffTime = Time.time - m_lastJumpTime;	
		if ( diffTime >= BICYCLE_JUMP_INTERVAL )
		{
			return;
		}

		if ( m_turnAxisX == 0 )
		{
			m_rigidbody.AddRelativeTorque (m_rigidbody.mass * m_physicParam.BicycleJumpForce * Vector3.right , ForceMode.Impulse);
			m_rigidbody.AddForce (m_rigidbody.mass * m_physicParam.BicycleJumpImpulse * m_trans.forward , ForceMode.Impulse);
		}
		else
		{
			float delta = m_turnAxisX > 0 ? -1 : 1;
			m_rigidbody.AddRelativeTorque (delta * m_rigidbody.mass * m_physicParam.BicycleJumpForce * Vector3.forward , ForceMode.Impulse);
			m_rigidbody.AddForce ( delta * m_rigidbody.mass * m_physicParam.BicycleJumpImpulse * m_trans.right, ForceMode.Impulse);
		}
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
