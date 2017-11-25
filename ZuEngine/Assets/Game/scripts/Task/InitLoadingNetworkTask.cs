using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.GameState;
using ZuEngine.Utility;
using ZuEngine.Service.Network;
using ZuEngine.Service;

public class InitLoadingNetworkTask : BaseTask 
{
	public InitLoadingNetworkTask()
	{
		
	}

	private EventResult OnConnectFinish(object eventData)
	{
		ZuLog.Log ("InitLoadingState OnConnectFinish");
		EventService.Instance.SendEvent (EventIDs.INIT_LOADING_NETWORK_FINISH);
		return null;
	}

	#region implemented abstract members of BaseTask
	public override void Resume ()
	{
		ZuLog.Log ("InitLoadingNetworkTask Resume");	
		if ( !NetworkService.Instance.IsConnected )
		{
			EventService.Instance.RegisterEvent (EventIDs.ON_NETWORK_CONNECT_FINISH, OnConnectFinish);
		}
		else
		{
			OnConnectFinish (null);
		}
	}
	public override void Pause ()
	{
	}
	public override void Update (float deltaTime)
	{
	}
	public override void Destroy ()
	{
	}
	#endregion
}
