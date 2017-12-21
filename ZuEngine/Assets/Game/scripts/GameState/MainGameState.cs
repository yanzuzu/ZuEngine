using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.GameState;
using ZuEngine.Utility;
using ZuEngine.Service;

public class MainGameState : BaseGameState 
{
	private VehicleCamera m_camera;

	private long INIT_STATE;

	#region IGameState implementation
	public override void OnInit(GameStateManager stateMgr)
	{
		ZuLog.Log ("MainGameState OnInit!");
		base.OnInit (stateMgr);
		MainGameService.Instance.Camera.SetTarget (MainGameService.Instance.Vehicles [0]);

		INIT_STATE = GetTaskState ();
		AddTask (new MainGameSimulateTask (),INIT_STATE);

		ChangeTaskState (INIT_STATE);
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
		base.OnDestroy ();

		GameObject.Destroy (MainGameService.Instance.Scene);
		MainGameService.Instance.Scene = null;

		GameObject.Destroy (MainGameService.Instance.Camera.gameObject);
		MainGameService.Instance.Camera = null;

		for (int i = 0; i < MainGameService.Instance.Vehicles.Count; i++)
		{
			GameObject.Destroy (MainGameService.Instance.Vehicles[i].gameObject);
		}
		MainGameService.Instance.Vehicles.Clear ();
		MainGameService.Instance.Vehicles = null;

		GameObject.Destroy (MainGameService.Instance.Ball .gameObject);
		MainGameService.Instance.Ball = null;
	}
	#endregion
}
