using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour , IBullet 
{
	public float Speed = 10f;
	public float homingSensitivity = 0.1f;

	ILauncherTarget m_target;

	public void SetTarget(ILauncherTarget target)
	{
		m_target = target;
	}

	void Update()
	{
		if ( null == m_target )
		{
			return;
		}
		Vector3 relativePos = m_target.GeLauncherTargetPos () - transform.position;
		Quaternion rota = Quaternion.LookRotation (relativePos);
		transform.rotation = Quaternion.Slerp (transform.rotation, rota,homingSensitivity);
		transform.Translate (0, 0, Speed * Time.deltaTime, Space.Self);
		//transform.position = Vector3.Lerp (transform.position, m_target.GeLauncherTargetPos (), Time.deltaTime);
	}

	#region IBullet implementation
	public void AddImpulse (Vector3 impulse)
	{
		
	}
	#endregion
}
