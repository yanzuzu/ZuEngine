using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine;
using ZuEngine.Utility;
using ZuEngine.Service;

public class GameEngine : ZuEngineGameObj
{

	// Use this for initialization
	void Start () {
		base.Start ();
		m_gameStateMgr.ChangeState (new MainGameState ());

		EventService.Instance.Test ();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();	
	}
}
