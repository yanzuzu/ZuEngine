using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.GameState;

namespace ZuEngine
{
	public class ZuEngine : MonoBehaviour 
	{
		private GameStateManager m_gameStateMgr;

		void Start () 
		{
			m_gameStateMgr = new GameStateManager ();
			m_gameStateMgr.ChangeState (new InitMainMenuState ());
		}

		void Update () 
		{
			m_gameStateMgr.Update (Time.deltaTime);
		}
	}
}
