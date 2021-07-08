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
			Debug.LogWarning($"[CCLog]{GetLogTag(obj)},message = {message}");
#else
			Debug.Log($"[CCLog]message = {message}");
#endif
        }
        public static void Log(string message = "")
        {
#if UNITY_EDITOR
            Debug.LogWarning($"[CCLog]message = {message}");
#else
			Debug.Log($"[CCLog]message = {message}");
#endif
        }


        public static void LogError(this object obj, string message = "")
		{
#if UNITY_EDITOR
			throw new ArgumentException($"[CCLog]{GetLogTag(obj)},message = {message}");
#else
			Debug.LogWarning($"[CCLog]message = {message}");
#endif
        }

        public static void LogError(string message)
        {
#if UNITY_EDITOR
            throw new ArgumentException($"[CCLog]message = {message}");
#else
			Debug.LogWarning($"[CCLog]message = {message}");
#endif
        }


        private static string GetLogTag(object obj)
		{
			return obj.GetType().Name;
		}
	}
}
