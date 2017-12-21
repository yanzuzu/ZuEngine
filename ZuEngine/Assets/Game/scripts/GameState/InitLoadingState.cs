using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.GameState;
using ZuEngine.Service;
using ZuEngine.Utility;
using ZuEngine.Service.Network;

public class InitLoadingState : BaseGameState 
{
	long NETWORK_TASK_STATE;

	#region IGameState implementation
	public override void OnInit(GameStateManager stateMgr)
	{
		ZuLog.Log ("InitLoadingState OnInit");
		base.OnInit (stateMgr);

		NETWORK_TASK_STATE = GetTaskState ();

		AddTask (new InitLoadingNetworkTask(), NETWORK_TASK_STATE);
		ChangeTaskState(NETWORK_TASK_STATE);

		EventService.Instance.RegisterEvent (EventIDs.INIT_LOADING_NETWORK_FINISH, OnNetworkFinish);
	}
	public override void OnUpdate (float deltaTime)
	{
	}
	public override void OnDestroy ()
	{
		base.OnDestroy ();
	}
	#endregion

	private EventResult OnNetworkFinish(object eventData)
	{
		ChangeState (new LobbyState ());
		return null;
	}
}
