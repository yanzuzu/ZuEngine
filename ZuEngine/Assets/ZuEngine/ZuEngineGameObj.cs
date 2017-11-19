using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.GameState;
using ZuEngine.Service;
using ZuEngine.Service.Network;

namespace ZuEngine
{
	public class ZuEngineGameObj : MonoBehaviour 
	{
		protected GameStateManager m_gameStateMgr;
		protected PhotonService m_photonService;

		public virtual void Awake()
		{
			GameObject photonObj = new GameObject ("PhotonNetworkObj");
			m_photonService = photonObj.AddComponent<PhotonService> ();
		}

		public virtual void Start () 
		{
			m_gameStateMgr = new GameStateManager ();
			NetworkService.Instance.Init (m_photonService,OnNetworkInitFinish);
		}

		public virtual void Update () 
		{
			m_gameStateMgr.Update (Time.deltaTime);
			EventService.Instance.SendEvent (EventIDs.ON_UPDATE, Time.deltaTime);
		}

		private void OnNetworkInitFinish()
		{
			EventService.Instance.SendEvent (EventIDs.ON_NETWORK_CONNECT_FINISH);
		}
	}
}
