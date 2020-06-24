using System.Collections;
using System;
using UnityEngine;

namespace ccAction
{
	public static class CCLog
	{
		public static void Log(this object obj,string message = "")
		{
#if UNITY_EDITOR
			Debug.LogWarning($"{GetLogTag(obj)},message = {message}");
#else
			Debug.Log($"message = {message}");
#endif
		}

		public static void LogError(this object obj, string message = "")
		{
#if UNITY_EDITOR
			throw new ArgumentNullException($"{GetLogTag(obj)},message = {message}");
#else
			Debug.LogWarning($"message = {message}");
#endif
		}

		private static string GetLogTag(object obj)
		{
			return obj.GetType().Name;
		}
	}
}
