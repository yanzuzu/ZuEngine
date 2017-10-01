using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.GameState;
using ZuEngine.Utility;

public class MainGameState : BaseGameState {
	#region IGameState implementation
	public override void OnInit(GameStateManager stateMgr)
	{
		base.OnInit (stateMgr);
		ZuLog.LogError ("MainGameState OnInit statrt");
	}
	public override void OnUpdate (float deltaTime)
	{
		ZuLog.Log ("MainGameState OnUpdate");
	}
	public override void OnDestroy ()
	{
		ZuLog.LogError ("MainGameState OnDestroy statrt");
	}
	#endregion
}
