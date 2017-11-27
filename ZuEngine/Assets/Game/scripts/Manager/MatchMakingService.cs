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

	private const byte MAX_PLAYERS = 2;

	public void JoinRoom(JoinRoomFinish joinRoomCb)
	{
		m_joinRoomFinishCb = joinRoomCb;

		bool isConnectRoom = false;
		List<RoomData> rooms = NetworkService.Instance.GetRoomList ();
		if ( rooms.Count == 0 )
		{
			// create room
			isConnectRoom = NetworkService.Instance.CreateRoom(string.Empty,MAX_PLAYERS);
		}
		else
		{
			// join the room
			for (int i = 0; i < rooms.Count; i++)
			{
				isConnectRoom = NetworkService.Instance.JoinRoom (rooms [i].Name);
				break;
			}
		}

		if ( m_joinRoomFinishCb != null )
		{
			m_joinRoomFinishCb (isConnectRoom);
		}
	}
}
