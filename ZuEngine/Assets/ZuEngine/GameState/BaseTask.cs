using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZuEngine.GameState
{
	public abstract class BaseTask 
	{
		private bool m_isActive;
		public bool IsActive
		{
			get{ return m_isActive; }
			set{ 
				m_isActive = value;
				if ( m_isActive )
				{
					Resume ();
				}
				else
				{
					Pause ();
				}
			}
		}

		#region ITask implementation

		abstract public void Resume ();

		abstract public void Pause ();

		abstract public void Update(float deltaTime);

		abstract public void Destroy();
		#endregion
	}
}