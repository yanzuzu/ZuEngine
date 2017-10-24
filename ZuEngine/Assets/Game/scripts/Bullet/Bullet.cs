using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour , IBullet , IDestroy
{
	public float LifeTime = 3f;
	
	private Rigidbody m_rigid;
	private float m_spawnTime = 0f;

	void Awake () 
	{
		m_rigid = GetComponent<Rigidbody> ();
		m_spawnTime = Time.time;
	}

	void Update () 
	{
		if ( Time.time - m_spawnTime > LifeTime )
		{
			DestroyObj ();
		}
	}

	public void DestroyObj()
	{
		Destroy (gameObject);
	}

	#region IBullet implementation
	public void AddImpulse (Vector3 impulse)
	{
		m_rigid.AddForce (impulse, ForceMode.Impulse);
	}
	#endregion
}
