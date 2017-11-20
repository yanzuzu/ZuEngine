using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.GameState;
using ZuEngine.Utility;
using ZuEngine.Service;

public class MainGameState : BaseGameState {

	private VehicleCamera m_camera;

	private long INIT_STATE = 0;

	#region IGameState implementation
	public override void OnInit(GameStateManager stateMgr)
	{
		ZuLog.Log ("MainGameState OnInit!");
		base.OnInit (stateMgr);
		INIT_STATE = GetTaskState ();

		AddTask (new MainGameInitResTask (), INIT_STATE);

		EventService.Instance.RegisterEvent (EventIDs.MAINGAME_LOAD_RES_FINISH, OnResLoadedFinish);

		ChangeTaskState (INIT_STATE);
	}

	public override void OnUpdate (float deltaTime)
	{
		if ( m_camera != null ) 
		{
			m_camera.OnUpdate (deltaTime);
		}
	}

	public override void OnDestroy ()
	{
		
	}
	#endregion

	private EventResult OnResLoadedFinish( object eventData )
	{
		m_camera = MainGameService.Instance.Camera;
		m_camera.SetTarget (MainGameService.Instance.Vehicles [0]);
		return null;
	}
}
