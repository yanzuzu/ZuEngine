using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.GameState;
using ZuEngine.Service;

public class MainGameInitResTask : BaseTask 
{
	#region implemented abstract members of BaseTask
	public override void Resume ()
	{
		// Load scene
		GameObject sceneObj = GameObject.Instantiate( ResourceService.Instance.Load("Scene/Scene1") ) as GameObject;
		MainGameService.Instance.Scene = sceneObj;
		// Load UI
		GameObject uiRoot = GameObject.Find("UI");
		GameObject uiCamera = GameObject.Find("UICamera");

		GameObject controlUI = GameObject.Instantiate( ResourceService.Instance.Load("UI/ControllerUI") ) as GameObject;
		controlUI.transform.SetParent (uiRoot.transform, false);
		controlUI.GetComponent<Canvas> ().worldCamera = uiCamera.GetComponent<Camera>();
		// load camera
		GameObject cameraObj = GameObject.Instantiate( ResourceService.Instance.Load("Camera/VehicleCamera") ) as GameObject;
		MainGameService.Instance.Camera = cameraObj.GetComponent<VehicleCamera> ();
		// Load vehicle
		List<Vehicle> vehicles = new List<Vehicle>();
		GameObject vehicle1 = GameObject.Instantiate( ResourceService.Instance.Load("Vehicle/Vehicle_1") ) as GameObject;
		GameObject vehicle2 = GameObject.Instantiate( ResourceService.Instance.Load("Vehicle/Vehicle_2") ) as GameObject;
		vehicles.Add (vehicle1.GetComponent<Vehicle> ());
		vehicles.Add (vehicle2.GetComponent<Vehicle> ());
		MainGameService.Instance.Vehicles = vehicles;
		// Load ball
		GameObject ballObj = GameObject.Instantiate( ResourceService.Instance.Load("Ball/Ball") ) as GameObject;
		MainGameService.Instance.Ball = ballObj.GetComponent<Ball> ();

		EventService.Instance.SendEvent (EventIDs.MAINGAME_LOAD_RES_FINISH);

	}
	public override void Pause ()
	{
		
	}
	public override void Update (float deltaTime)
	{
		
	}
	#endregion
}
