using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.Service;
using ZuEngine.Utility;

public class JoyStickController
{

	public JoyStickController()
	{
		EventService.Instance.RegisterEvent (EventIDs.UI_CONTROLLER_LEFT_ENTER, OnLeftBtnEnter);
		EventService.Instance.RegisterEvent (EventIDs.UI_CONTROLLER_RIGHT_ENTER, OnRightBtnEnter);
		EventService.Instance.RegisterEvent (EventIDs.UI_CONTROLLER_GAS_ENTER, OnGasBtnEnter);
		EventService.Instance.RegisterEvent (EventIDs.UI_CONTROLLER_BACK_ENTER, OnBackBtnEnter);
	}

	EventResult OnLeftBtnEnter(object eventData)
	{
		return null;
	}

	EventResult OnRightBtnEnter(object eventData)
	{
		return null;
	}

	EventResult OnGasBtnEnter(object eventData)
	{
		return null;
	}

	EventResult OnBackBtnEnter(object eventData)
	{
		return null;
	}

}
