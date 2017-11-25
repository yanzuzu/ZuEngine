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
		UiService.Instance.LoadUI<ControllerView>("UI/ControllerUI");
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
