using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLauncherTarget : MonoBehaviour , ILauncherTarget {
	#region ILauncherTarget implementation
	public Vector3 GeLauncherTargetPos ()
	{
		return transform.position;
	}
	#endregion

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
