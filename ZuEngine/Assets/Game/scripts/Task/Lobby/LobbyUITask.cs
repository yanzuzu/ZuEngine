using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.GameState;
using ZuEngine.Service;

public class LobbyUITask : BaseTask 
{
	private LobbyView m_uiView;

	public LobbyUITask()
	{
		m_uiView = UiService.Instance.LoadUI<LobbyView> ("UI/LobbyUI");
	}

	#region implemented abstract members of BaseTask
	public override void Resume ()
	{
	}

	public override void Pause ()
	{
	}

	public override void Update (float deltaTime)
	{
	}

	public override void Destroy()
	{
		if ( m_uiView != null )
		{
			GameObject.Destroy (m_uiView.gameObject);
		}
	}
	#endregion
}
