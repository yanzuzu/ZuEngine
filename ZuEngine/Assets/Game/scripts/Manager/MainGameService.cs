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

	private MatchInitMsg m_initDatas = new MatchInitMsg();
	public MatchInitMsg InitData
	{
		get{ return m_initDatas; }
		set{ m_initDatas = value; }
	}

	public void CreateMatchIniData(int sceneID, int ballID, MatchInitMsg.VehicleData [] vehicles)
	{
		m_initDatas.BallId = ballID;
		m_initDatas.SceneId = sceneID;
		m_initDatas.Vehicles = vehicles;
	}

	public MatchInitMsg.VehicleData CreateVehicleData(TeamType team, int vehicleId, Vector3 startPos, Quaternion startRotate)
	{
		MatchInitMsg.VehicleData data = new MatchInitMsg.VehicleData ();
		data.Team = team;
		data.Id = vehicleId;
		data.StartPos = startPos;
		data.StartRotate = startRotate;
		return data;
	}
}
