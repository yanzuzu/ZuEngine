using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZuEngine.Service;

public class MainGameService :  BaseService<MainGameService>
{
	private Ball m_ball;
	public Ball Ball
	{
		get{ return m_ball; }
		set{ m_ball = value; }
	}
}
