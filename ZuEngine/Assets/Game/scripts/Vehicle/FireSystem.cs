using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSystem : MonoBehaviour 
{
	public float TriggerRadius = 100f;
	[SerializeField]
	private BasicGun m_launcher;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Collider [] colliders = Physics.OverlapSphere (transform.position, TriggerRadius);
		for (int i = 0; i < colliders.Length; i++)
		{
			ILauncherTarget target = colliders [i].gameObject.GetComponent<ILauncherTarget> ();
			if ( null == target )
			{
				continue;
			}
			m_launcher.SetTarget (target);
			return;
		}
		m_launcher.SetTarget (null);
	}
}
