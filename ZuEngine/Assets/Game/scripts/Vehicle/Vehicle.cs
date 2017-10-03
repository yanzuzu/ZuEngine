using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour , ICameraTarget , IVehicle
{
	private Transform m_trans;

	void Start () 
	{
		m_trans = gameObject.transform;
	}
		
	void Update () {

	}

	#region IVehicle implementation

	public void OnPhysicUpdate (float deltaTime)
	{
		
	}

	#endregion

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
