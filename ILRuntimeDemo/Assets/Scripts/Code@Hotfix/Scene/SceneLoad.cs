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
		//加载场景时，其他需要执行的任务。每个任务的进度为0-1
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
				//执行任务
				m_loadTask.Invoke((p) => {
					//更新进度
					progress = Mathf.Clamp01(p);
					m_progressAction?.Invoke();
				});
			}
		}
		
		string m_sceneName;
		LoadingPanel m_loadingPanel;
		List<LoadTask> m_loadTaskList;//任务列表
		int m_totalSceneLoadProgress;//加载场景所占的任务数
		int m_totalProgress;//总任务数（加载场景所占的任务数+其他任务的数量，用于计算loading百分比）
		bool m_isLoadFinish;

		protected SceneLoad(string sceneName)
		{
			m_sceneName = sceneName;
			m_loadTaskList = new List<LoadTask>();
			RegisterAllLoadTask();
			m_totalSceneLoadProgress = 1;
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
			IEnumeratorTool.instance.StartCoroutine(LoadScene());
		}
		
		//注册所有需要执行的其他任务
		protected virtual void RegisterAllLoadTask()
		{
		}
		
		//注册一个新任务
		protected virtual void RegisterLoadTask(LoadTaskDelegate task)
		{
			m_loadTaskList.Add(new LoadTask(task, UpdateLoadTaskProgress));
		}

		//更新任务进度
		protected virtual void UpdateLoadTaskProgress()
		{
			float progress = m_totalSceneLoadProgress;
			foreach (var task in m_loadTaskList)
				progress += task.progress;
			UpdateProgress(progress);
		}
		
		//加载场景前执行，主要做一些内存清理的工作
		protected virtual void OnPreLoadScene()
		{
			UIPanelManager.instance.UnLoadPanelOnLoadScene();
			UIViewManager.instance.DestroyViewOnLoadScene();
		}
		
		//更新总进度
		protected virtual void UpdateProgress(float progress)
		{
			float progressPercent = Mathf.Clamp01(progress / m_totalProgress);
			m_loadingPanel.SetProgress(progressPercent);
			
			//所有任务进度为1时，即加载完成
			if (progress >= m_totalProgress && !m_isLoadFinish)
				IEnumeratorTool.instance.StartCoroutine(LoadFinish());
		}

		//所有任务加载完成
		IEnumerator LoadFinish()
		{
			Debug.Log($"Loads scene '{m_sceneName}' completed.");
			OnLoadFinish();
			
			//等待0.5s，这样不会进度显示100%的时候瞬间界面消失。
			yield return IEnumeratorTool.instance.waitForHalfSecond;
			m_isLoadFinish = true;
			m_loadingPanel.Hide();
		}

		//加载完成时执行
		protected virtual void OnLoadFinish()
		{
		}

		//加载场景
		IEnumerator LoadScene()
		{
			//先跳转空场景，进行内存的清理
			var clearSceneOperation = SceneManager.LoadSceneAsync(GlobalDefine.SCENE_PATH + GlobalDefine.CLEAR_SCENE_NAME);
			while (!clearSceneOperation.isDone)
				yield return null;
			
			OnPreLoadScene();
			GC.Collect();

			Debug.Log("start load scene: " + m_sceneName);
			var sceneOperation = SceneManager.LoadSceneAsync(GlobalDefine.SCENE_PATH + m_sceneName);
			// When allowSceneActivation is set to false then progress is stopped at 0.9. The isDone is then maintained at false.
			// When allowSceneActivation is set to true isDone can complete.
			sceneOperation.allowSceneActivation = false;

			while (sceneOperation.progress < 0.9f)
			{
				UpdateProgress(sceneOperation.progress);
				yield return null;
			}

			UpdateProgress(1);
			//为true时，场景切换
			sceneOperation.allowSceneActivation = true;
			StartLoadTask();
		}

		//执行其他加载任务
		protected virtual void StartLoadTask()
		{
			if(m_loadTaskList.Count == 0)
				return;

			foreach (var task in m_loadTaskList)
				task.Start();
		}
	}
}