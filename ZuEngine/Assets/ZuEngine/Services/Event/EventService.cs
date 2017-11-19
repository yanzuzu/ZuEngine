using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.Utility;

namespace ZuEngine.Service
{
	public class EventResult
	{
		public object ResultObj;
		public bool EatReturn = false;

		public EventResult(bool eatReturn, object resultObj = null)
		{
			EatReturn = eatReturn;
			ResultObj = resultObj;
		}
	}

	public class EventService : BaseService<EventService>
	{
		public delegate EventResult eventCallBack(object userData);
		
		private List<eventCallBack>[] m_eventCbs;

		public EventService()
		{
			m_eventCbs = new List<eventCallBack>[System.Enum.GetNames (typeof(EventIDs)).Length];
		}

		public object SendEvent(EventIDs eventId, object userData = null)
		{
			int eventIdx = (int)eventId;
			if ( m_eventCbs [eventIdx] == null ) 
			{
				return null;
			}

			object returnObj = null;
			for (int i = m_eventCbs [eventIdx].Count - 1; i >= 0; i--) 
			{
				eventCallBack eventCb = m_eventCbs [eventIdx] [i];
				if ( eventCb == null )
				{
					m_eventCbs [eventIdx].RemoveAt (i);
					continue;
				}
				EventResult result = eventCb (userData);
				if ( result != null )
				{
					returnObj = result.ResultObj;
					if ( result.EatReturn )
					{
						break;
					}
				}
			}
			return returnObj;
		}

		public void RegisterEvent(EventIDs eventId, eventCallBack cb)
		{
			int eventIdx = (int)eventId;
			if ( m_eventCbs [eventIdx] == null ) 
			{
				m_eventCbs [eventIdx] = new List<eventCallBack> ();
			}
			m_eventCbs [eventIdx].Add (cb);
		}

	}
}
