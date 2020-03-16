using UnityEngine;

namespace Tool
{
	public class MonoBehaviourInstance<T> : MonoBehaviour
	{
		public static T instance { get; private set; }
		
		void Awake()
		{
			if(instance != null)
			{
				//防止挂载了多个相同的组件
				DestroyImmediate(gameObject);
				return;
			}
			instance = GetComponent<T>();
		}
	}
}

