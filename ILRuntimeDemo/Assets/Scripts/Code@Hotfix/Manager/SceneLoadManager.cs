using System;
using System.Collections;
using System.Collections.Generic;
using Hotfix.Manager;
using UnityEngine;

namespace Hotfix.Manager
{
	public class SceneLoadManager : ManagerBaseWithAttr<SceneLoadManager, SceneLoadAttribute>
	{
		Dictionary<string, SceneLoad> m_sceneLoadDic;
		public override void Init()
		{
			base.Init();

			m_sceneLoadDic = new Dictionary<string, SceneLoad>();
			foreach (var data in m_atrributeDataDic.Values)
			{
				var attr = data.attribute as SceneLoadAttribute;
				var sceneLoad = Activator.CreateInstance(data.type, new object[] { attr.value }) as SceneLoad;
				m_sceneLoadDic.Add(attr.value, sceneLoad);
			}
		}
		
		public void LoadScene(string scene)
		{
			var sceneLoad = GetSceneLoad(scene);
			sceneLoad.Start();
		}
		
		SceneLoad GetSceneLoad(string scene)
		{
			if(!m_sceneLoadDic.TryGetValue(scene, out SceneLoad sceneLoad))
			{
				Debug.LogError($"[SceneLoadManager] Cannot found scene({scene}) loader");
			}
			return sceneLoad;
		}
	}
}