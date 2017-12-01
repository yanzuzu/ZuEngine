using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.GameState;
using ZuEngine.Utility;
using ZuEngine.Service;

public class MainGameState : BaseGameState 
{
	private VehicleCamera m_camera;

	#region IGameState implementation
	public override void OnInit(GameStateManager stateMgr)
	{
		ZuLog.Log ("MainGameState OnInit!");
		base.OnInit (stateMgr);
		MainGameService.Instance.Camera.SetTarget (MainGameService.Instance.Vehicles [0]);
	}

	public override void OnUpdate (float deltaTime)
	{
		if ( MainGameService.Instance.Camera != null ) 
		{
			MainGameService.Instance.Camera.OnUpdate (deltaTime);
		}
	}

	public override void OnDestroy ()
	{
		
	}
	#endregion
}
