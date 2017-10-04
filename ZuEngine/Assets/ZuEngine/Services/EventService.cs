using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.Utility;

namespace ZuEngine.Service
{
	public class EventService : BaseService<EventService>
	{
		public EventService()
		{
			ZuLog.Log ("EventService start");
		}

		public void Test()
		{
			ZuLog.Log ("test!!");
		}
	}
}
