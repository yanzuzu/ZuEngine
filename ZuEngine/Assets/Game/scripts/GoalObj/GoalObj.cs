using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.Utility;
using ZuEngine.Service;

public class GoalObj : MonoBehaviour 
{

	void OnTriggerEnter(Collider other)
	{
		if ( other.gameObject.layer != LayerMask.NameToLayer ("Ball") )
		{
			return;
		}
		ZuLog.Log ("Goal!!!");
		EventService.Instance.SendEvent (EventIDs.ON_GOAL);
	}
}
