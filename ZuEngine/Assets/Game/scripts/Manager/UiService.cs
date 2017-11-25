using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.Service;
using ZuEngine.Utility;

public class UiService : BaseService< UiService >
{
	private GameObject m_uiRoot;
	private Camera m_uiCamera;

	public void Init(GameObject uiRoot, Camera uiCamera)
	{
		m_uiRoot = uiRoot;
		m_uiCamera = uiCamera;
	}

	public T LoadUI<T>(string uiPath) where T: Component
	{
		if ( null == m_uiRoot || null == m_uiCamera )
		{
			ZuLog.LogError(string.Format("m_uiRoot : {0} or m_uiCamera : {1} is null",m_uiRoot,m_uiCamera));
			return default(T);
		}

		GameObject uiObj =  GameObject.Instantiate( ResourceService.Instance.Load (uiPath) ) as GameObject;
		uiObj.transform.SetParent (m_uiRoot.transform, false);
		uiObj.GetComponent<Canvas> ().worldCamera = m_uiCamera.GetComponent<Camera>();
		return uiObj.GetComponent<T> ();
	}
}
