using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine;
using ZuEngine.Utility;
using ZuEngine.Service;

public class GameEngine : ZuEngineGameObj
{

	// Use this for initialization
	public override void Start () {
		base.Start ();
		m_gameStateMgr.ChangeState (new InitLoadingState ());
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update ();	
	}
}
