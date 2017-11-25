using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZuEngine.GameState
{
	public class GameTaskData
	{
		public BaseTask Task;
		public long ActivateStates;
	}

	public class GameStateManager 
	{
		private IGameState m_currentState;

		private List<GameTaskData> m_tasks = new List<GameTaskData>();

		private int m_lastStateId = -1;

		public long GetTaskState()
		{
			m_lastStateId++;
			return 1 << m_lastStateId;
		}

		public void ChangeState(IGameState newState)
		{
			if ( m_currentState != null ) 
			{
				m_currentState.OnDestroy ();
			}

			m_currentState = newState;
			m_currentState.OnInit (this);
		}

		public void AddTask(BaseTask task, long taskState)
		{
			GameTaskData data = new GameTaskData ();
			data.Task = task;
			data.ActivateStates = taskState;
			m_tasks.Add (data);
		}

		public void ChangeTaskState(long taskState)
		{
			for (int i = 0; i < m_tasks.Count; i++)
			{
				m_tasks [i].Task.IsActive = m_tasks [i].ActivateStates == taskState;
			}
		}

		public void Update(float deltaTime)
		{
			if (m_currentState != null) 
			{
				m_currentState.OnUpdate (deltaTime);
			}

			for (int i = 0; i < m_tasks.Count; i++)
			{
				if ( m_tasks [i].Task.IsActive )
				{
					m_tasks [i].Task.Update (deltaTime);
				}
			}
		}

		public void Destroy()
		{
			for (int i = 0; i < m_tasks.Count; i++)
			{
				m_tasks [i].Task.Destroy ();
			}
			m_tasks.Clear ();
		}
	}
}
