using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZuEngine.Service;

public class LobbyView : MonoBehaviour 
{
	[SerializeField]
	private Button m_startBtn;

	public void OnLobbyStartBtnClick()
	{
		EventService.Instance.SendEvent (EventIDs.UI_LOBBY_START_CLICK);
	}
}
