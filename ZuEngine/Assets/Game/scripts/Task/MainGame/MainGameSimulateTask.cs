using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.GameState;
using ZuEngine.Service;
using ZuEngine.Utility;

public class MainGameSimulateTask : BaseTask 
{
	private float m_updateInteval = 1;
	private float m_currentUpdate = 0;

	public MainGameSimulateTask()
	{

	}

	#region implemented abstract members of BaseTask
	public override void Resume ()
	{
	}
	public override void Pause ()
	{
	}
	public override void Destroy ()
	{
	}

	public override void Update (float deltaTime)
	{
		m_currentUpdate += deltaTime;
		if ( m_currentUpdate < m_updateInteval )
		{
			return;
		}
		m_currentUpdate = 0;


		InputMsg inputData = new InputMsg ();
		inputData.PlayerId = 2;
		inputData.Input = new VehicleControlData ();
		inputData.Input.Gas = UnityEngine.Random.Range (-1, 2);
		inputData.Input.TurnAxisX = UnityEngine.Random.Range (-1f, 1f);
		EventService.Instance.SendEvent (EventIDs.MSG_INPUT_DATA, inputData);	
	}
	#endregion


}
