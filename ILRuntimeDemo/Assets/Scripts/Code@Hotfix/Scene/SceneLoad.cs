using System;
using System.Collections;
using System.Collections.Generic;
using Hotfix.Manager;
using Hotfix.UI;
using Tool;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hotfix
{
	public class SceneLoad
	{
		protected delegate void LoadTaskDelegate(Action<float> callback);
		protected class LoadTask
		{
			public float progress;
			LoadTaskDelegate m_loadTask;
			Action m_progressAction;

			//加载任务和进度更新
			public LoadTask(LoadTaskDelegate task, Action action)
			{
				m_loadTask = task;
				m_progressAction = action;
			}

			public void Start()
			{
				progress = 0;
				m_loadTask.Invoke((p) => {
					progress = p;
					m_progressAction?.Invoke();
				});
			}
		}
		
		string m_sceneName;
		LoadingPanel m_loadingPanel;
		List<LoadTask> m_loadTaskList;
		int m_totalSceneLoadProgress;
		int m_totalProgress;
		bool m_isLoadFinish;

		protected SceneLoad(string sceneName)
		{
			m_sceneName = sceneName;
			m_loadTaskList = new List<LoadTask>();
			RegisterAllLoadTask();
			m_totalProgress = m_loadTaskList.Count + m_totalSceneLoadProgress;
		}
		
		public virtual void Start()
		{
			m_isLoadFinish = false;
			m_loadingPanel = null;
			UIHelper.ShowPanel<LoadingPanel>(OnLoadingPanelLoaded);
		}
		
		protected virtual void OnLoadingPanelLoaded(LoadingPanel panel)
		{
			m_loadingPanel = panel;
			IEnumeratorTool.instance.StartCoroutine(LoadSceneLevel());
		}
		
		protected virtual void RegisterAllLoadTask()
		{
		}
		
		protected virtual void RegisterLoadTask(LoadTaskDelegate task)
		{
			m_loadTaskList.Add(new LoadTask(task, UpdateLoadTaskProgress));
		}

		protected virtual void UpdateLoadTaskProgress()
		{
			float progress = m_totalSceneLoadProgress;
			foreach (var task in m_loadTaskList)
				progress += task.progress;
			UpdateProgress(progress);
		}
		
		protected virtual void OnPreLoadScene()
		{
			UIPanelManager.instance.UnLoadPanelOnLoadScene();
		}
		
		protected virtual void UpdateProgress(float progress)
		{
			float progressPercent = Mathf.Clamp01(progress / m_totalProgress);
			Debug.Log("UpdateProgress:" + progressPercent);
			m_loadingPanel.SetProgress(progressPercent);
			if (progress >= m_totalProgress && !m_isLoadFinish)
				IEnumeratorTool.instance.StartCoroutine(LoadFinish());
		}

		IEnumerator LoadFinish()
		{
			OnLoadFinish();
			yield return IEnumeratorTool.instance.waitForHalfSecond;
			m_isLoadFinish = true;
			m_loadingPanel.Hide();
		}

		protected virtual void OnLoadFinish()
		{
		}
		
		public virtual IEnumerator LoadSceneLevel()
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

					UpdateProgress(1);
					result.allowSceneActivation = true;
					StartLoadTask();
					// Debug.Log($"Loads scene '{m_sceneName}' completed.");
					// }
					// });
		}
		
		protected virtual void StartLoadTask()
		{
			if(m_loadTaskList.Count == 0)
				return;

			foreach (var task in m_loadTaskList)
				task.Start();
		}
	}
}