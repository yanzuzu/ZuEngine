using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.Service;

public class MainGameService :  BaseService<MainGameService>
{
	private VehicleCamera m_camera;
	public VehicleCamera Camera
	{
		get{ return m_camera; }
		set{ m_camera = value; }
	}

	private List<Vehicle> m_vehicles;
	public List<Vehicle> Vehicles
	{
		get{ return m_vehicles; }
		set{ m_vehicles = value; }
	}

	private Ball m_ball;
	public Ball Ball
	{
		get{ return m_ball; }
		set{ m_ball = value; }
	}

	private GameObject m_scene;
	public GameObject Scene
	{
		get{ return m_scene; }
		set{ m_scene = value; }
	}
}
