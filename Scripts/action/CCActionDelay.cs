//------------------------------------------------------------------------------
//      Copyright (c) 2021 , ZensYue ZensYue@163.com
//      All rights reserved.
//      Use, modification and distribution are subject to the "MIT License"
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ccAction
{
	#region DelayTime 延迟执行
	public class CCActionDelay : ActionInterval
	{
		public static CCActionDelay Create(float d)
		{
			var action = new CCActionDelay();
			if (action != null && action.InitWithDuration(d))
				return action;
			return null;
		}

		public override IAction Clone()
		{
			return Create(m_duration);
		}

		public override void Update(float time)
		{
			//Debug.Log($"Elapsed = {Elapsed},m_duration = {m_duration},IsDone = {IsDone()},cur_time = {Time.time}");
		}

		public override IAction Reverse()
		{
			return Create(m_duration);
		}
	}
	#endregion
}
