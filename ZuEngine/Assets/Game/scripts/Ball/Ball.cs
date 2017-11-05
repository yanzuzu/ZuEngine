using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.Utility;

public class Ball : MonoBehaviour 
{
	public float ShootForce = 3f;

	private Transform m_attachTarget;
	private Rigidbody m_rigid;
	// Use this for initialization
	void Start () 
	{
		m_rigid = GetComponent<Rigidbody> ();
		if ( m_rigid == null )
		{
			ZuLog.LogError ("ball needs the rigidBody component!");
		}

		MainGameService.Instance.Ball = this;
	}
		
	void Update()
	{
		#if UNITY_EDITOR
		if( Input.GetKeyDown(KeyCode.R))
		{
			transform.position = new Vector3(100.0f , 0.2f, 120.0f );
			transform.localRotation = Quaternion.Euler(Vector3.zero);
			m_rigid.velocity = Vector3.zero;
			m_rigid.angularVelocity = Vector3.zero;
		}
		#endif

		if ( null == m_attachTarget )
		{
			return;
		}

		transform.position = m_attachTarget.position + new Vector3(0,2f,0) + m_attachTarget.forward * 6f;
	}

	public void Shoot()
	{
		if ( null == m_attachTarget )
		{
			ZuLog.LogWarning ("no attach target can shoot");
			return;
		}
		m_rigid.isKinematic = false;

		Vector3 forward =  m_attachTarget.forward;
		forward.y += 0.5f;
		m_rigid.AddForce (forward * ShootForce, ForceMode.Impulse);
		m_attachTarget = null;
	}

	void OnTriggerEnter(Collider other)
	{
		if ( other.gameObject.layer != LayerMask.NameToLayer ("Vehicle") )
		{
			return;
		}

		m_attachTarget = other.gameObject.transform;
		m_rigid.velocity = Vector3.zero;
		m_rigid.angularVelocity = Vector3.zero;
		m_rigid.isKinematic = true;
		ShootBallSystem shootSystem = m_attachTarget.GetComponent<ShootBallSystem> ();
		if ( shootSystem != null )
		{
			shootSystem.AttachBall (this);
		}
	}
}
