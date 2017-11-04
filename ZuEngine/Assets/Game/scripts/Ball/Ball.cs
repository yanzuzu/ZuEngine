using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.Utility;

public class Ball : MonoBehaviour 
{
	private Transform m_attachTarget;

	// Use this for initialization
	void Start () 
	{
		
	}

	void Update()
	{
		if ( null == m_attachTarget )
		{
			return;
		}

		transform.position = m_attachTarget.position + new Vector3(0,2f,0) + m_attachTarget.forward * 6f;
	}


	void OnTriggerEnter(Collider other)
	{
		if ( other.gameObject.layer != LayerMask.NameToLayer ("Vehicle") )
		{
			return;
		}

		m_attachTarget = other.gameObject.transform;
	}
}
