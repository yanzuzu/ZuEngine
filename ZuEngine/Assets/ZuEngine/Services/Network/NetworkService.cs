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

		public List<RoomData> GetRoomList()
		{
			return m_networkObj.GetRoomList ();
		}
	}
}
