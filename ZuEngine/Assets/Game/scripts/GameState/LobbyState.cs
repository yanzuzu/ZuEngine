using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.GameState;
using ZuEngine.Utility;
using ZuEngine.Service;

public class LobbyState : BaseGameState 
{
	private long LOBBY_UI;

	#region IGameState implementation
	public override void OnInit (GameStateManager stateMgr)
	{
		base.OnInit (stateMgr);
		LOBBY_UI = GetTaskState ();
		AddTask (new LobbyUITask (), LOBBY_UI);

		ChangeTaskState (LOBBY_UI);

		EventService.Instance.RegisterEvent (EventIDs.UI_LOBBY_START_CLICK, OnStartBtnClick);
	}
	public override void OnUpdate (float deltaTime)
	{
		base.OnUpdate (deltaTime);
	}
	public override void OnDestroy ()
	{
		base.OnDestroy ();
	}
	#endregion

	private EventResult OnStartBtnClick(object eventData)
	{
		ChangeState (new MainGameState ());
		return null;
	}
}
