using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.GameState;
using ZuEngine.Utility;

public class InitMainMenuState : BaseGameState 
{
	#region IGameState implementation
	public override void OnInit (GameStateManager stateMgr)
	{
		base.OnInit (stateMgr);
		ZuLog.LogError ("MainMenu OnInit");
	}
	public override void OnUpdate (float deltaTime)
	{
		base.OnUpdate (deltaTime);
		ZuLog.Log ("MainMenu OnUpdate");
		if (UnityEngine.Input.GetKeyDown (KeyCode.Z))
		{
			ChangeState (new MainGameState ());
		}
	}
	public override void OnDestroy ()
	{
		base.OnDestroy ();
		ZuLog.LogError ("MainMenu OnDestroy");
	}
	#endregion
}
