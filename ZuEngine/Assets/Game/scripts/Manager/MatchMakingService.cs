using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.Service;
using ZuEngine.Service.Network;
using ZuEngine.Utility;

public class MatchMakingService : BaseService<MatchMakingService>
{
	public delegate void JoinRoomFinish (bool isOK);

	private JoinRoomFinish m_joinRoomFinishCb;

	private const byte MAX_PLAYERS = 1;

	public MatchMakingService()
	{
		EventService.Instance.RegisterEvent (EventIDs.ON_NETWORK_PLAYER_CONNECT, OnPlayerConnect);
		EventService.Instance.RegisterEvent (EventIDs.ON_NETWORK_ROOM_CREATED, OnRoomCreated);
	}

	public void JoinRoom(JoinRoomFinish joinRoomCb)
	{
		m_joinRoomFinishCb = joinRoomCb;

		List<RoomData> rooms = NetworkService.Instance.GetRoomList ();
		if ( rooms.Count == 0 )
		{
			// create room
			NetworkService.Instance.CreateRoom(string.Empty,MAX_PLAYERS);
		}
		else
		{
			// join the room
			for (int i = 0; i < rooms.Count; i++)
			{
				NetworkService.Instance.JoinRoom (rooms [i].Name);
				break;
			}
		}
	}

	private EventResult OnRoomCreated( object data )
	{
		CheckRoomFull ();
		return null;
	}

	private EventResult OnPlayerConnect( object data )
	{
		CheckRoomFull ();
		return null;
	}

	private void CheckRoomFull()
	{
		if ( PhotonNetwork.room == null )
		{
			ZuLog.LogWarning ("the Photon room is null");
			return;
		}

		ZuLog.Log (string.Format ("room playerCount: {0}, MaxCount: {1}", PhotonNetwork.room.PlayerCount, PhotonNetwork.room.MaxPlayers));
		if ( PhotonNetwork.room.PlayerCount >= PhotonNetwork.room.MaxPlayers )
		{
			if ( m_joinRoomFinishCb != null )
			{
				m_joinRoomFinishCb (true);
			}
		}
	}
}
