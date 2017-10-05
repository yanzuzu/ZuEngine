using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.GameState;
using ZuEngine.Service;

namespace ZuEngine
{
	public class ZuEngineGameObj : MonoBehaviour 
	{
		protected GameStateManager m_gameStateMgr;

		public virtual void Start () 
		{
			m_gameStateMgr = new GameStateManager ();
		}

		public virtual void Update () 
		{
			m_gameStateMgr.Update (Time.deltaTime);
			EventService.Instance.SendEvent (EventIDs.ON_UPDATE, Time.deltaTime);
		}
	}
}
