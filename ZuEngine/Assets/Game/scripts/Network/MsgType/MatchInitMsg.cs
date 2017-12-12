using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchInitMsg 
{
	public class VehicleData
	{
		public int PlayerId;
		public TeamType Team;
		public int Id = 0;
		public Vector3 StartPos;
		public Quaternion StartRotate;
	}

	public int SceneId = 0;
	public VehicleData [] Vehicles;
	public int BallId = 0;
}
