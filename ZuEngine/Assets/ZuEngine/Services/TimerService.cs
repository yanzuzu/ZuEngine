using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.Service;
using ZuEngine.Utility;

namespace ZuEngine.Service
{
	public class TimerService : BaseService<TimerService> 
	{
		public struct TimerData
		{
			public int TimerId;
			public object UserData;
			public float FinishTime;
			public float Time;
			public bool IsLoop;
			public TimerService.OnTimerFinish CallBack;
		}

		public delegate void OnTimerFinish(object data);

		private int m_timerId = 0;
		List<TimerData> m_timers = new List<TimerData>();

		public int Schedule(float time, OnTimerFinish callBack, object userData = null, bool isLoop = false)
		{
			TimerData data = new TimerData ();
			data.TimerId = m_timerId;
			data.Time = time;
			data.FinishTime = Time.time + time;
			data.UserData = userData;
			data.IsLoop = isLoop;
			data.CallBack = callBack;
			m_timers.Add (data);
			return m_timerId++;
		}

		public void Remove(int timerId)
		{
			for (int i = 0; i < m_timers.Count; i++)
			{
				if ( m_timers [i].TimerId == timerId )
				{
					m_timers.RemoveAt (i);
					break;
				}
			}
		}

		public void OnUpdate(float deltaTime)
		{
			float currentTime = Time.time;
			TimerData data;
			for (int i = m_timers.Count - 1; i >= 0; i--)
			{
				data = m_timers [i];
				if ( data.CallBack == null )
				{
					m_timers.RemoveAt (i);
					break;
				}
				if (data.FinishTime < currentTime )
				{
					if ( data.IsLoop )
					{
						float newTime = currentTime + data.Time;
						data.FinishTime = newTime;
						m_timers [i] = data;
						data.CallBack (m_timers[i].UserData);
					}
					else
					{
						data.CallBack (m_timers[i].UserData);
						m_timers.RemoveAt (i);
					}
				}
			}
		}
	}
}
