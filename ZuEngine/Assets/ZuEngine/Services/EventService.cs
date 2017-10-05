using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.Utility;

namespace ZuEngine.Service
{
	public class EventService : BaseService<EventService>
	{
		public delegate void eventCallBack(object userData);

		public struct ListenEventData
		{
			public eventCallBack CB;
			public object UserData;
		}

		private List<ListenEventData>[] m_eventCbs;

		public EventService()
		{
			m_eventCbs = new List<ListenEventData>[System.Enum.GetNames (typeof(EventIDs)).Length];
		}

		public void SendEvent(EventIDs eventId, object userData)
		{
			int eventIdx = (int)eventId;
			if ( m_eventCbs [eventIdx] == null ) 
			{
				return;
			}

			for (int i = 0; i < m_eventCbs [eventIdx].Count; i++) 
			{
				ListenEventData data = m_eventCbs [eventIdx] [i];
				data.CB (data.UserData);
			}
		}

		public void RegisterEvent(EventIDs eventId, eventCallBack cb , object userData = null)
		{
			int eventIdx = (int)eventId;
			if ( m_eventCbs [eventIdx] == null ) 
			{
				m_eventCbs [eventIdx] = new List<ListenEventData> ();
			}
			ListenEventData data = new ListenEventData ();
			data.CB = cb;
			data.UserData = userData;
			m_eventCbs [eventIdx].Add (data);
		}
	}
}
