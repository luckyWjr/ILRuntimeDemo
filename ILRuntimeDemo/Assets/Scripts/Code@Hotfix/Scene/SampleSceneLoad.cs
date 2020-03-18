using System;
using System.Collections;
using Hotfix.Manager;
using Hotfix.UI;
using Tool;

namespace Hotfix
{
	[SceneLoad(GlobalDefine.SAMPLE_SCENE_NAME)]
	public class SampleSceneLoad : SceneLoad
	{
		public SampleSceneLoad(string sceneName) : base(sceneName)
		{
		}

		protected override void RegisterAllLoadTask()
		{
			base.RegisterAllLoadTask();
			RegisterLoadTask(LoadTask1);
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
		
		protected override void OnLoadFinish()
		{
			base.OnLoadFinish();
			UIHelper.ShowPanel<LoginPanel>();
		}
	}
}