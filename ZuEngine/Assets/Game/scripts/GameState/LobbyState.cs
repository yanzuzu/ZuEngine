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
		MatchMakingService.Instance.JoinRoom (OnJoinRoomFinish);
		return null;
	}

	private void OnJoinRoomFinish(bool isOK)
	{
		ZuLog.Log ("join Room isOK = " + isOK);

		MatchInitMsg initData = new MatchInitMsg ();
		initData.BallId = 1;
		initData.SceneId = 1;
		initData.Vehicles = new MatchInitMsg.VehicleData[2];
		initData.Vehicles[0] = MainGameService.Instance.CreateVehicleData (TeamType.Blue, 1, new Vector3 (129, 0.07f, 103), Quaternion.identity);
		initData.Vehicles[1] = MainGameService.Instance.CreateVehicleData (TeamType.Red, 2, new Vector3 (129, 0.07f, 143),Quaternion.identity);

		ChangeState (new PreloadMainGameState (initData));
	}
}
