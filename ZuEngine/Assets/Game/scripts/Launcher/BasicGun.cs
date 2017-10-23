using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.Utility;

public class BasicGun : MonoBehaviour
{
	public float RotateSpeed = 100f;

	private ILauncherTarget m_target;

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
		Vector3 targetDir = m_target.GeLauncherTargetPos () - transform.position;
		Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, RotateSpeed * Time.deltaTime, 0f);
		transform.rotation = Quaternion.LookRotation (newDir);
	}
}
