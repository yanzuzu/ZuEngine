using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.GameState;
using ZuEngine.Service;
using ZuEngine.Utility;

public class InitLoadingState : BaseGameState 
{

	#region IGameState implementation
	public override void OnInit(GameStateManager stateMgr)
	{
		ZuLog.Log ("InitLoadingState OnInit");
		base.OnInit (stateMgr);
		EventService.Instance.RegisterEvent (EventIDs.ON_NETWORK_CONNECT_FINISH, OnConnectFinish);
	}
	public override void OnUpdate (float deltaTime)
	{
		
	}
	public override void OnDestroy ()
	{

	}
	#endregion

	private EventResult OnConnectFinish(object eventData)
	{
		ZuLog.Log ("InitLoadingState OnConnectFinish");
		ChangeState (new MainGameState ());
		return null;
	}
}
