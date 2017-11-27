using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.Utility;
using ZuEngine.Service;

namespace ZuEngine.Service.Network
{
	public class NetworkService : BaseService<NetworkService>
	{
		private bool m_isConnected = false;
		public bool IsConnected
		{
			get{ return m_isConnected; }
		}

		private INetwork m_networkObj;
		private System.Action m_initFinishCb;

		public void Init(INetwork network, System.Action finishCb)
		{
			ZuLog.Log ("NetworkService Init start");
			m_networkObj = network;
			m_initFinishCb = finishCb;

			m_networkObj.Init (m_initFinishCb);

			EventService.Instance.RegisterEvent (EventIDs.ON_NETWORK_CONNECT_FINISH, OnNetworkConnectFinish);
		}

		private EventResult OnNetworkConnectFinish(object eventData)
		{
			m_isConnected = true;
			return null;
		}

		public List<RoomData> GetRoomList(bool onlyAvaliable = true)
		{
			List<RoomData> rooms = m_networkObj.GetRoomList ();
			if ( !onlyAvaliable )
			{
				return rooms;
			}

			List<RoomData> avaliableRooms = new List<RoomData> ();
			for (int i = 0; i < rooms.Count; i++)
			{
				if ( rooms [i].MaxPlayerCount > rooms [i].PlayerCount )
				{
					avaliableRooms.Add (rooms [i]);
				}
			}
			return avaliableRooms;
		}

		public bool CreateRoom(string roomName, byte maxPlayerCount = 0 )
		{
			ZuLog.Log (string.Format("Create Room name:{0}, maxPlayerCount:{1}" ,roomName, maxPlayerCount) );
			return m_networkObj.CreateRoom (roomName, maxPlayerCount);
		}

		public bool JoinRoom(string roomName)
		{
			ZuLog.Log ("Join Room: " + roomName);
			return m_networkObj.JoinRoom (roomName);
		}
	}
}
