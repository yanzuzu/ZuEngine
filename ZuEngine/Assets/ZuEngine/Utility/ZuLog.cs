using UnityEngine;
using System;

namespace ZuEngine.Utility
{
	public class ZuLog  
	{
		#if UNITY_EDITOR
		public static Action<object> Log = Debug.Log;
		public static Action<object> LogWarning = Debug.LogWarning;
		public static Action<object> LogError = Debug.LogError;
		#else
		public static void Log(object message)
		{
			Debug.Log (message);
		}

		public static void LogWarning(object message)
		{
			Debug.LogWarning (message);
		}

		public static void LogError(object message)
		{
			Debug.LogError (message);
		}
		#endif
	}
}
