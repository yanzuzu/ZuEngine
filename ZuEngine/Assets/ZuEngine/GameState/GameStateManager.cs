using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZuEngine.GameState
{
	public class GameStateManager 
	{
		private IGameState m_currentState;

		public void ChangeState(IGameState newState)
		{
			if ( m_currentState != null ) 
			{
				m_currentState.OnDestroy ();
			}

			m_currentState = newState;
			m_currentState.OnInit (this);
		}

		public void Update(float deltaTime)
		{
			if (m_currentState != null) 
			{
				m_currentState.OnUpdate (deltaTime);
			}
		}
	}
}
