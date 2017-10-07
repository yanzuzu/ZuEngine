using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace ZuEngine.Utility
{
	public class GameObjTriggerData
	{
		public PointerEventData pointerEventData;
		public object userData;
	}

	public static class GameObjectUtility 
	{
		static public void AddEventTrigger(this GameObject root, EventTriggerType type, UnityAction<GameObjTriggerData> cb = null, object userData = null)
		{
			EventTrigger trigger = root.GetComponent<EventTrigger> ();
			if ( trigger == null )
			{
				trigger = root.AddComponent<EventTrigger> ();
			}
			EventTrigger.Entry entry = new EventTrigger.Entry ();
			entry.eventID = type;
			entry.callback.AddListener ((data) => {
				if( cb != null )
				{
					GameObjTriggerData triggerData = new GameObjTriggerData();
					triggerData.pointerEventData = (PointerEventData)data;
					triggerData.userData = userData;
					cb(triggerData);
				}
			});
			trigger.triggers.Add (entry);
		}
	}
}
