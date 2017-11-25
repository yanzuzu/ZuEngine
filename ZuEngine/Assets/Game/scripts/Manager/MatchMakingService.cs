using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.Service;
using ZuEngine.Service.Network;

public class MatchMakingService : BaseService<MatchMakingService>
{
	public void Connect()
	{
		List<RoomData> rooms = NetworkService.Instance.GetRoomList ();
		if ( rooms.Count == 0 )
		{
			// create room
		}
		else
		{
			// join the room
		}
	}
}
