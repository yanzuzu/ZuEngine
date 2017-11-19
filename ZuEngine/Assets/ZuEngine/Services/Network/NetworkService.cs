using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.Utility;

namespace ZuEngine.Service.Network
{
	public class NetworkService : BaseService<NetworkService>
	{
		private INetwork m_networkObj;
		private System.Action m_initFinishCb;

		public void Init(INetwork network, System.Action finishCb)
		{
			ZuLog.Log ("NetworkService Init start");
			m_networkObj = network;
			m_initFinishCb = finishCb;

			m_networkObj.Init (m_initFinishCb);
		}
	}
}
