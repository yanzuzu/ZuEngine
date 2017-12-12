using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.GameState;
using ZuEngine.Service;
using ZuEngine.Utility;

public class MainGameInitResTask : BaseTask 
{
	private MatchInitMsg m_initData;

	public MainGameInitResTask(MatchInitMsg initData)
	{
		m_initData = initData;
	}

	#region implemented abstract members of BaseTask
	public override void Resume ()
	{
		if ( m_initData == null )
		{
			ZuLog.LogError ("init data is null");
			return;
		}

		// Load scene
		GameObject sceneObj = GameObject.Instantiate( ResourceService.Instance.Load(string.Format("Scene/Scene{0}",m_initData.SceneId) ) ) as GameObject;
		MainGameService.Instance.Scene = sceneObj;
		// Load UI
		UiService.Instance.LoadUI<ControllerView>("UI/ControllerUI");
		// load camera
		GameObject cameraObj = GameObject.Instantiate( ResourceService.Instance.Load("Camera/VehicleCamera") ) as GameObject;
		MainGameService.Instance.Camera = cameraObj.GetComponent<VehicleCamera> ();
		// Load vehicle
		List<Vehicle> vehicles = new List<Vehicle>();
		for (int i = 0; i < m_initData.Vehicles.Length; i++)
		{
			GameObject vehicle = GameObject.Instantiate( ResourceService.Instance.Load( string.Format("Vehicle/Vehicle_{0}",m_initData.Vehicles[i].Id)) ) as GameObject;
			vehicle.transform.position = m_initData.Vehicles [i].StartPos;
			vehicle.transform.rotation = m_initData.Vehicles [i].StartRotate;
			MainGameService.Instance.AddController (vehicle, m_initData.Vehicles [i].PlayerId);
			vehicles.Add (vehicle.GetComponent<Vehicle> ());
		}
		MainGameService.Instance.Vehicles = vehicles;
		// Load ball
		GameObject ballObj = GameObject.Instantiate( ResourceService.Instance.Load(string.Format("Ball/Ball_{0}",m_initData.BallId)) ) as GameObject;
		MainGameService.Instance.Ball = ballObj.GetComponent<Ball> ();

		EventService.Instance.SendEvent (EventIDs.MAINGAME_LOAD_RES_FINISH);

	}
	public override void Pause ()
	{
		
	}
	public override void Update (float deltaTime)
	{
		
	}

	public override void Destroy ()
	{
		GameObject.Destroy (MainGameService.Instance.Scene);
		MainGameService.Instance.Scene = null;

		GameObject.Destroy (MainGameService.Instance.Camera.gameObject);
		MainGameService.Instance.Camera = null;

		for (int i = 0; i < MainGameService.Instance.Vehicles.Count; i++)
		{
			GameObject.Destroy (MainGameService.Instance.Vehicles[i].gameObject);
		}
		MainGameService.Instance.Vehicles.Clear ();
		MainGameService.Instance.Vehicles = null;

		GameObject.Destroy (MainGameService.Instance.Ball .gameObject);
		MainGameService.Instance.Ball = null;
	}
	#endregion
}
