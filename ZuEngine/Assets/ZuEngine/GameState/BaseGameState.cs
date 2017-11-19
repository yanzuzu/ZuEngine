using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZuEngine.GameState
{
	public class BaseGameState : IGameState 
	{
		private GameStateManager m_stateMgr;

		public void ChangeState(IGameState gameState)
		{
			m_stateMgr.ChangeState (gameState);
		}

		public long GetTaskState()
		{
			return m_stateMgr.GetTaskState ();
		}

		public void AddTask(BaseTask task, long taskState)
		{
			m_stateMgr.AddTask (task, taskState);
		}

		public void ChangeTaskState(long taskState)
		{
			m_stateMgr.ChangeTaskState (taskState);
		}

		#region IGameState implementation

		public virtual void OnInit (GameStateManager stateMgr)
		{
			m_stateMgr = stateMgr;
		}

		public virtual void OnUpdate (float deltaTime)
		{
		}

		public virtual void OnDestroy ()
		{
		}

		#endregion
	}
}
