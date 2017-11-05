using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.Service;

public class VehicleControl : MonoBehaviour 
{
	private Vehicle m_vehicle;

	private VehicleControlData m_ctrlData = new VehicleControlData();
		
	void Start () 
	{
		m_vehicle = GetComponent<Vehicle> ();
		RegisterEvent ();
	}

	private void RegisterEvent()
	{
		EventService.Instance.RegisterEvent (EventIDs.UI_CONTROLLER_LEFT_ENTER, OnLeftBtnEnter);
		EventService.Instance.RegisterEvent (EventIDs.UI_CONTROLLER_RIGHT_ENTER, OnRightBtnEnter);
		EventService.Instance.RegisterEvent (EventIDs.UI_CONTROLLER_GAS_ENTER, OnGasBtnEnter);
		EventService.Instance.RegisterEvent (EventIDs.UI_CONTROLLER_BACK_ENTER, OnBackBtnEnter);
		EventService.Instance.RegisterEvent (EventIDs.UI_CONTROLLER_BOOST_ENTER, OnBoostBtnEnter);
		EventService.Instance.RegisterEvent (EventIDs.UI_CONTROLLER_JUMP_CLICK, OnJumpBtnClick);
		EventService.Instance.RegisterEvent (EventIDs.UI_CONTROLLER_SHOOT, OnShootClick);
	}

	EventResult OnLeftBtnEnter(object eventData)
	{
		m_ctrlData.TurnAxisX = (bool)eventData ? -1f : 0f;
		return null;
	}

	EventResult OnRightBtnEnter(object eventData)
	{
		m_ctrlData.TurnAxisX = (bool)eventData ? 1f : 0f;
		return null;
	}

	EventResult OnGasBtnEnter(object eventData)
	{
		m_ctrlData.Gas = (bool)eventData ? 1f : 0f;
		return null;
	}

	EventResult OnBackBtnEnter(object eventData)
	{
		m_ctrlData.Gas = (bool)eventData ? -1f : 0f;
		return null;
	}

	EventResult OnBoostBtnEnter(object eventData)
	{
		m_ctrlData.IsBoost = (bool)eventData;
		return null;
	}


	EventResult OnJumpBtnClick(object eventData)
	{
		m_ctrlData.IsJump = true;
		return null;
	}

	EventResult OnShootClick(object eventData)
	{
		m_ctrlData.IsShoot = (bool)eventData;
		return null;
	}

	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR
		UpdateController ();
		#endif
		m_vehicle.CtrlData = m_ctrlData;
	}

	private void UpdateController()
	{
		m_ctrlData.IsBoost = Input.GetKey(KeyCode.B);

		m_ctrlData.TurnAxisX = Input.GetAxis("Horizontal");
		m_ctrlData.Gas = Input.GetAxis("Vertical");

		if(UnityEngine.Input.GetKeyDown(KeyCode.Space))
		{
			m_ctrlData.IsJump = true;
		}else
		{
			m_ctrlData.IsJump = false;
		}

		m_ctrlData.IsShoot = UnityEngine.Input.GetKeyDown (KeyCode.S);
	}
}
