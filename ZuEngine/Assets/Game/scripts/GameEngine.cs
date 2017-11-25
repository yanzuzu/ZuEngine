using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine;
using ZuEngine.Utility;
using ZuEngine.Service;

public class GameEngine : ZuEngineGameObj
{
	[SerializeField]
	private GameObject m_uiRoot;
	[SerializeField]
	private Camera m_uiCamera;

	// Use this for initialization
	public override void Start () {
		base.Start ();

		UiService.Instance.Init (m_uiRoot,m_uiCamera);

		m_gameStateMgr.ChangeState (new InitLoadingState ());
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update ();	
	}
}
