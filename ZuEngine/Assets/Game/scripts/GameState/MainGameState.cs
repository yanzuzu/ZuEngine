using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.GameState;
using ZuEngine.Utility;

public class MainGameState : BaseGameState {

	private VehicleCamera m_camera;
	private Vehicle m_vehicle;

	private JoyStickController m_controllerUI;

	#region IGameState implementation
	public override void OnInit(GameStateManager stateMgr)
	{
		base.OnInit (stateMgr);
		if ( Camera.main == null ) 
		{
			ZuLog.LogError ("can't get the main camera");	
			return;
		}
		m_camera = Camera.main.GetComponent<VehicleCamera> ();

		GameObject vehicle = GameObject.Find ("Vehicle_1");
		if ( vehicle == null ) 
		{
			ZuLog.LogError ("can't get the vehicle");
			return;
		}
		m_vehicle = vehicle.GetComponent<Vehicle> ();

		m_camera.SetTarget (m_vehicle);

		m_controllerUI = new JoyStickController ();
	}
	public override void OnUpdate (float deltaTime)
	{
		if ( m_camera != null ) 
		{
			m_camera.OnUpdate (deltaTime);
		}
	}
	public override void OnDestroy ()
	{
		
	}
	#endregion
}
