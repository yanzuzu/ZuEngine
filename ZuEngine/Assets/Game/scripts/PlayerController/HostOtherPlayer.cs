using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.Service;

public class HostOtherPlayer : MonoBehaviour 
{
	private Vehicle m_vehicle;

	private int m_playerId;
	public int PlayerId
	{
		get{ return m_playerId; }
		set{ m_playerId = value; }
	}

	void Start () 
	{
		m_vehicle = GetComponent<Vehicle> ();
		EventService.Instance.RegisterEvent (EventIDs.MSG_INPUT_DATA, OnMsgInput);	
	}

	void Update ()
	{
		
	}

	private EventResult OnMsgInput( object data )
	{
		InputMsg inputData = data as InputMsg;
		if ( inputData.PlayerId != m_playerId )
		{
			return null;
		}
		m_vehicle.CtrlData = inputData.Input;
		return null;
	}
}
