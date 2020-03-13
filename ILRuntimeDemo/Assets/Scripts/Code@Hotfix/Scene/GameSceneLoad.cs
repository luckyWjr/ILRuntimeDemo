using System.Collections;
using System.Collections.Generic;
using Hotfix.Manager;

namespace Hotfix
{
	[SceneLoad(GlobalDefine.GAME_SCENE_NAME)]
	public class GameSceneLoad : SceneLoad
	{
		public GameSceneLoad(string sceneName) : base(sceneName)
		{
		}
	}
}

