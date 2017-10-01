using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZuEngine.GameState
{
	public interface IGameState 
	{
		void OnInit (GameStateManager stateMgr);
		void OnUpdate(float deltaTime);
		void OnDestroy();
	}
}
