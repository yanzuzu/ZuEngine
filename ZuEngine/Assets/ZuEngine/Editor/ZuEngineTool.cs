using UnityEditor;
using UnityEngine;
using ZuEngine.Utility;

namespace ZuEngine.Tool
{
	public class ZuEngineTool: MonoBehaviour
	{
		private const string ZU_ENGINE_OBJ_NAME = "ZuEngine";

		[MenuItem("ZuEngine/Create ZuEngine")]
		static void CreateZuEngine()
		{
			GameObject engineObj = GameObject.Find (ZU_ENGINE_OBJ_NAME);
			if ( engineObj != null ) 
			{
				ZuLog.LogWarning ("find the zuEngine obj!");
				return;
			}
			GameObject newEngineObj = new GameObject ();
			newEngineObj.AddComponent<ZuEngine> ();
			newEngineObj.name = ZU_ENGINE_OBJ_NAME;
			ZuLog.Log ("Create ZuEngine obj complete!");
		}

	}
}
