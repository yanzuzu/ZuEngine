using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBallSystem : MonoBehaviour 
{
	public float ShootBallTimer = 3f;

	private Vehicle m_vehicle;

	private float m_attachTime = 0f;
	private Ball m_ball;

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

		if ( null == m_ball )
		{
			return;
		}

		if ( Time.time - m_attachTime > ShootBallTimer )
		{
			ShootBall ();
		}

		if ( m_vehicle.CtrlData.IsShoot )
		{
			m_vehicle.CtrlData.IsShoot = false;
			ShootBall ();
		}
	}

	private void ShootBall()
	{
		if ( m_ball == null )
		{
			return;
		}
		m_ball.Shoot ();
		m_ball = null;
	}

	public void AttachBall(Ball ball)
	{
		m_attachTime = Time.time;
		m_ball = ball;
	}
}
