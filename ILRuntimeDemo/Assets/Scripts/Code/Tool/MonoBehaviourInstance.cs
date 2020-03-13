using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool
{
	public class MonoBehaviourInstance<T> : MonoBehaviour
	{
		public static T instance { get; private set; }
		// static T m_instance;

		// public static T instance
		// {
		// 	get{
		// 		if (m_instance == null)
		// 		{
		// 			m_instance = GetComponent<T>();
		// 		}
		// 		return m_instance;
		// 	}
		// }
		void Awake()
		{
			instance = GetComponent<T>();
		}
	}
}

