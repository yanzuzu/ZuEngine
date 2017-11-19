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
		#endregion

		public void OnConnectedToMaster()
		{
			ZuLog.Log ("OnConnectedToMaster start");
			if ( m_initFinishCb != null )
			{
				m_initFinishCb ();
			}
		}
	}
}
