using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBallSystem : MonoBehaviour 
{

	private Vehicle m_vehicle;

	void Start () 
	{
		m_vehicle = GetComponent<Vehicle> ();
	}

	void Update () 
	{
		if ( null == m_vehicle )
		{
			return;
		}

		if ( m_vehicle.CtrlData.IsShoot )
		{
			m_vehicle.CtrlData.IsShoot = false;
			MainGameService.Instance.Ball.Shoot ();
		}
	}
}
