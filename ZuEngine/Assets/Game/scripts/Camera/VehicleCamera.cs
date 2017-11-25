using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCamera : MonoBehaviour, ICamera 
{
	[SerializeField]
	private float m_dist = 3f;
	[SerializeField]
	private float m_height = 2f;

	private ICameraTarget m_target;

	private Transform m_camTrans;

	void Awake()
	{
		m_camTrans = gameObject.transform;
	}

	public void SetTarget( ICameraTarget target )
	{
		m_target = target;

	}

	public void SetParameters( float distance, float height )
	{
		m_dist = distance;
		m_height = height;
	}

	#region ICamera implementation
	public void OnUpdate (float deltaTime)
	{
		if (m_target == null) 
		{
			return;
		}

		Vector3 targetPos =  m_target.GetPosition ();

		Vector3 camPos = targetPos - Vector3.forward * m_dist;
		camPos.y = m_height;
		m_camTrans.position = camPos;
		m_camTrans.LookAt (targetPos);
	}
	#endregion
}
