using System;
using System.Collections;
using Hotfix.Manager;
using Hotfix.UI;
using Tool;

namespace Hotfix
{
	[SceneLoad(GlobalDefine.GAME_SCENE_NAME)]
	public class GameSceneLoad : SceneLoad
	{
		public GameSceneLoad(string sceneName) : base(sceneName)
		{
		}

		protected override void RegisterAllLoadTask()
		{
			base.RegisterAllLoadTask();
			RegisterLoadTask(LoadTask1);
			RegisterLoadTask(LoadTask2);
		}

		void LoadTask1(Action<float> callback)
		{
			IEnumeratorTool.instance.StartCoroutine(Task1(callback));
		}

		IEnumerator Task1(Action<float> callback)
		{
			for (int i = 1; i < 6; i++)
			{
				yield return IEnumeratorTool.instance.waitForHalfSecond;
				callback(0.2f * i);
			}
		}
		
		void LoadTask2(Action<float> callback)
		{
			IEnumeratorTool.instance.StartCoroutine(Task2(callback));
		}
		
		IEnumerator Task2(Action<float> callback)
		{
			yield return IEnumeratorTool.instance.waitForOneSecond;
			callback(0.3f);
			yield return IEnumeratorTool.instance.waitForOneSecond;
			callback(0.5f);
			yield return IEnumeratorTool.instance.waitForOneSecond;
			callback(0.8f);
			yield return IEnumeratorTool.instance.waitForOneSecond;
			callback(1);
		}
		
		protected override void OnLoadFinish()
		{
			base.OnLoadFinish();
			UIHelper.ShowPanel<GamePanel>();
		}
	}
}