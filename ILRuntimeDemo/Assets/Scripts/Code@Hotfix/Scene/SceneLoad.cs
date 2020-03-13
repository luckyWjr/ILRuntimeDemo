using System;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hotfix
{
	public class SceneLoad
	{
		string m_sceneName;
		
		public SceneLoad(string sceneName)
		{
			m_sceneName = sceneName;
		}
		
		public virtual void Start()
		{
			IEnumeratorTool.instance.StartCoroutine(LoadSceneLevel(null));
		}
		
		protected virtual void OnPreLoadScene()
		{
		}
		
		protected virtual void UpdateProgress(float progress)
		{
			Debug.Log("UpdateProgress:"+progress);
		}
		
		public virtual IEnumerator LoadSceneLevel(Action callback)
		{
			OnPreLoadScene();

			// var clearResult = SceneManager.LoadSceneAsync(GlobalDefine.SCENE_BASE_PATH + "Clear.unity");
			// clearResult.OnStateChangedCallback(st =>{
			//
			// 	if(st == LoadState.Completed)
			// 	{
					// GC.Collect();

					Debug.Log("start load scene: " + m_sceneName);
					var result = SceneManager.LoadSceneAsync(GlobalDefine.SCENE_PATH + m_sceneName);
					// When allowSceneActivation is set to false then progress is stopped at 0.9. The isDone is then maintained at false.
					// When allowSceneActivation is set to true isDone can complete.
					result.allowSceneActivation = false;

					while (result.progress < 0.9f)
					{
						UpdateProgress(result.progress);
						
						yield return null;
					}
					
					Debug.Log($"Loads scene '{m_sceneName}' completed.");
					result.allowSceneActivation = true;
					callback?.Invoke();
				// }
			// });
		}
	}
}