﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZuEngine.Service;
using UnityEngine.EventSystems;
using ZuEngine.Utility;

public class ControllerView : MonoBehaviour 
{
	[SerializeField]
	private Button m_leftBtn;
	[SerializeField]
	private Button m_rightBtn;
	[SerializeField]
	private Button m_gasBtn;
	[SerializeField]
	private Button m_backBtn;
	[SerializeField]
	private Button m_jumpBtn;
	[SerializeField]
	private Button m_boostBtn;

	void Start()
	{
		RegisterEvent ();
	}

	private void RegisterEvent()
	{
		m_leftBtn.gameObject.AddEventTrigger (EventTriggerType.PointerEnter, OnLeftBtnEnter , true);
		m_leftBtn.gameObject.AddEventTrigger (EventTriggerType.PointerExit, OnLeftBtnEnter , false);

		m_rightBtn.gameObject.AddEventTrigger (EventTriggerType.PointerEnter, OnRightBtnEnter , true);
		m_rightBtn.gameObject.AddEventTrigger (EventTriggerType.PointerExit, OnRightBtnEnter , false);

		m_gasBtn.gameObject.AddEventTrigger (EventTriggerType.PointerEnter, OnGasBtnEnter , true);
		m_gasBtn.gameObject.AddEventTrigger (EventTriggerType.PointerExit, OnGasBtnEnter , false);

		m_backBtn.gameObject.AddEventTrigger (EventTriggerType.PointerEnter, OnBackBtnEnter , true);
		m_backBtn.gameObject.AddEventTrigger (EventTriggerType.PointerExit, OnBackBtnEnter , false);

		m_jumpBtn.gameObject.AddEventTrigger (EventTriggerType.PointerClick, OnJumpBtnClick);

		m_boostBtn.gameObject.AddEventTrigger (EventTriggerType.PointerEnter, OnBoostBtnEnter , true);
		m_boostBtn.gameObject.AddEventTrigger (EventTriggerType.PointerExit, OnBoostBtnEnter , false);
	}

	private void OnLeftBtnEnter( GameObjTriggerData data)
	{
		EventService.Instance.SendEvent (EventIDs.UI_CONTROLLER_LEFT_ENTER, (bool)data.userData);
	}

	private void OnRightBtnEnter( GameObjTriggerData data)
	{
		EventService.Instance.SendEvent (EventIDs.UI_CONTROLLER_RIGHT_ENTER, (bool)data.userData);
	}

	private void OnGasBtnEnter( GameObjTriggerData data)
	{
		EventService.Instance.SendEvent (EventIDs.UI_CONTROLLER_GAS_ENTER, (bool)data.userData);
	}

	private void OnBackBtnEnter( GameObjTriggerData data)
	{
		EventService.Instance.SendEvent (EventIDs.UI_CONTROLLER_BACK_ENTER, (bool)data.userData);
	}

	private void OnJumpBtnClick( GameObjTriggerData data )
	{
		EventService.Instance.SendEvent (EventIDs.UI_CONTROLLER_JUMP_CLICK);
	}

	private void OnBoostBtnEnter( GameObjTriggerData data)
	{
		EventService.Instance.SendEvent (EventIDs.UI_CONTROLLER_BOOST_ENTER, (bool)data.userData);
	}
}
