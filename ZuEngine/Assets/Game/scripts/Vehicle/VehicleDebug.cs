using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleDebug : MonoBehaviour
{
	[SerializeField]
	private Text m_debugTxt;

	private Vehicle m_vehicle;

	void Start()
	{
		m_vehicle = GetComponent<Vehicle> ();
	}

	void Update()
	{
		if ( m_debugTxt == null )
		{
			return;
		}
		string inputTxt = string.Empty;
		inputTxt = string.Format ("axisX = {0}\nGas = {1}\nWheel torque = {2}" +
			"\nSpeed = {3}",
			m_vehicle.CtrlData.TurnAxisX, m_vehicle.CtrlData.Gas, m_vehicle.WheelColliders[2].motorTorque,
			m_vehicle.Speed);
		m_debugTxt.text = inputTxt;
	}
}
