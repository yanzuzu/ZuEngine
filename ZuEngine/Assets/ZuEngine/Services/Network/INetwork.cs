using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZuEngine.Service.Network
{
	public interface INetwork 
	{
		void Init(System.Action finishCb);
		List<RoomData> GetRoomList();
		bool CreateRoom (string roomName, byte maxPlayers);
		bool JoinRoom(string roomName);
	}
}
