using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.Utility;

public class BasicGun : MonoBehaviour , ILauncher
{
	public float RotateSpeed = 100f;
	public float ImpulseForce = 100f;
	public float LaunchTime = 2f;

	private float m_lastLaunchTime = 0f;
	private ILauncherTarget m_target;

	public void SetTarget(ILauncherTarget target)
	{
		m_target = target;
	}

	public void Fire()
	{
		if ( Time.time - m_lastLaunchTime < LaunchTime )
		{
			return;
		}
		m_lastLaunchTime = Time.time;
		GameObject bullet = GameObject.Instantiate( Resources.Load<GameObject>("Bullet/Bullet") );
		bullet.transform.position = transform.position;
		bullet.transform.rotation = transform.rotation;
		IBullet bulletComponent = bullet.GetComponent<IBullet> ();
		bulletComponent.AddImpulse (transform.forward * ImpulseForce);
	}
		
	void Update()
	{
		if ( null == m_target )
		{
			return;
		}
		Vector3 targetDir = m_target.GeLauncherTargetPos () - transform.position;
		Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, RotateSpeed * Time.deltaTime, 0f);
		transform.rotation = Quaternion.LookRotation (newDir);

		if ( Input.GetKeyDown (KeyCode.A) )
		{
			FireMissile ();
		}
	}

	void FireMissile()
	{
		GameObject missile = GameObject.Instantiate( Resources.Load<GameObject>("Bullet/Missile") );
		missile.transform.position = transform.position;
		missile.transform.rotation = transform.rotation;
		Missile missileComponent = missile.GetComponent<Missile> ();
		missileComponent.SetTarget (m_target);
	}
}
