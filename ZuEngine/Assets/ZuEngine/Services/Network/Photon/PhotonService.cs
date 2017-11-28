using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.Utility;

namespace ZuEngine.Service.Network
{
	public class PhotonService : MonoBehaviour, INetwork {
		private string GAME_VERSION = "1.0";
		private System.Action m_initFinishCb;

		#region INetwork implementation
		public void Init (System.Action finishCb)
		{
			m_initFinishCb = finishCb;

			PhotonNetwork.ConnectUsingSettings (GAME_VERSION);
		}

		public List<RoomData> GetRoomList()
		{
			List<RoomData> result = new List<RoomData> ();
			RoomInfo [] rooms =  PhotonNetwork.GetRoomList ();
			for (int i = 0; i < rooms.Length; i++)
			{
				RoomData data = new RoomData ();
				data.Name = rooms [i].Name;
				data.PlayerCount = rooms [i].PlayerCount;
				data.MaxPlayerCount = rooms [i].MaxPlayers;
				result.Add (data);
			}
			return result;
		}

		public bool CreateRoom(string roomName, byte maxPlayers)
		{
			RoomOptions roomOption = new RoomOptions ();
			roomOption.MaxPlayers = maxPlayers;
			return PhotonNetwork.CreateRoom (roomName,roomOption,TypedLobby.Default);
		}

		public bool JoinRoom(string roomName)
		{
			return PhotonNetwork.JoinRoom (roomName);
		}

		#endregion

		public void OnConnectedToMaster()
		{
			ZuLog.Log ("OnConnectedToMaster start");
			if ( m_initFinishCb != null )
			{
				m_initFinishCb ();
			}
		}

		public void OnPhotonPlayerConnected( PhotonPlayer otherPlayer )
		{
			ZuLog.Log ("OnPhotonPlayerConnected otherPlayer = " + otherPlayer);
			EventService.Instance.SendEvent (EventIDs.ON_NETWORK_PLAYER_CONNECT, otherPlayer);
		}

		public void OnCreatedRoom()
		{
			ZuLog.Log ("OnCreatedRoom start");
			EventService.Instance.SendEvent (EventIDs.ON_NETWORK_ROOM_CREATED);
		}
	}
}
