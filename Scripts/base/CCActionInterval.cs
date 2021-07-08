//------------------------------------------------------------------------------
//      Copyright (c) 2021 , ZensYue ZensYue@163.com
//      All rights reserved.
//      Use, modification and distribution are subject to the "MIT License"
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ccAction {

	/// <summary>
	/// 间隔Action
	/// </summary>
    #region ActionInterval
    public abstract class ActionInterval : IFiniteTimeAction
	{
		protected float m_elapsed;
		#region Elapsed get m_elapsed
		public float Elapsed
		{
			get
			{
				return m_elapsed;
			}
			set {; }
		}
		#endregion

		bool m_firstTick;
		#region FirstTick get m_firstTick
		public bool FirstTick
		{
			get
			{
				return m_firstTick;
			}
			set {; }
		}
		#endregion

		protected bool InitWithDuration(float d)
		{
			m_duration = d;
			m_elapsed = 0;
			m_firstTick = true;
			return true;
		}

		public override void StartWithTarget()
		{
			base.StartWithTarget();
			m_elapsed = 0.0f;
			m_firstTick = true;
		}

		public override bool IsDone()
		{
			return m_elapsed >= m_duration;
		}

		public override void Step(float dt)
		{
			if (m_firstTick)
			{ 
				m_firstTick = false;
				m_elapsed = float.Epsilon;
			}
			else
			{
				m_elapsed += dt;
			}
			float updateDt = Math.Max(0.0f, Math.Min(1.0f, m_elapsed / m_duration));
			Update(updateDt);
		}
	}
	#endregion

}


