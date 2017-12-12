using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.GameState;
using ZuEngine.Utility;
using ZuEngine.Service;
using ZuEngine.Service.Network;

public class PreloadMainGameState : BaseGameState 
{
	private long INIT_STATE = 0;

	public PreloadMainGameState(MatchInitMsg matchInitData)
	{
		MainGameService.Instance.InitData = matchInitData;
	}

	#region IGameState implementation
	public override void OnInit(GameStateManager stateMgr)
	{
		ZuLog.Log ("PreloadMainGameState OnInit!");
		base.OnInit (stateMgr);

		INIT_STATE = GetTaskState ();

		AddTask (new MainGameInitResTask (MainGameService.Instance.InitData), INIT_STATE);

		EventService.Instance.RegisterEvent (EventIDs.MAINGAME_LOAD_RES_FINISH, OnResLoadedFinish);

		ChangeTaskState (INIT_STATE);
	}

	public override void OnUpdate (float deltaTime)
	{
	}

	public override void OnDestroy ()
	{

	}
	#endregion

	private EventResult OnResLoadedFinish( object eventData )
	{
		ChangeState (new MainGameState ());
		return null;
	}
}
