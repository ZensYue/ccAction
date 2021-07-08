//------------------------------------------------------------------------------
//      Copyright (c) 2021 , ZensYue ZensYue@163.com
//      All rights reserved.
//      Use, modification and distribution are subject to the "MIT License"
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ccAction
{
	/// <summary>
	/// 为了避免和System.Action冲突，这里把名字改成CCAction
	/// </summary>
	public abstract class IAction
	{
		protected static int Tag = 0;

        protected int m_tag;

		protected int m_flags;
		#region Flags get set m_flags
		public int Flags
		{
			get
			{
				return m_flags;
			}
			set
			{
				m_flags = value;
			}
		}
		#endregion Target

		/// <summary>
		/// 只有最外层的Action才有分组概念
		/// </summary>
		public int GroupID { get; set; } = 0;

		public IAction()
		{
			Tag++;
			m_tag = Tag;
			m_flags = 0;
		}

		public int GetTag() { return m_tag;}

		/// <summary>
		/// 派生类调用完父类方法后，如果有需要获取transform属性作为初始值，必须用m_target获取
		/// </summary>
		/// <param name="target"></param>
		public virtual void StartWithTarget()
		{
		}

		public virtual void Update(float time)
		{
		}
		public virtual void Step(float dt)
		{
		}
		public virtual void Stop()
		{
		}
		public virtual bool IsDone()
		{
			return true;
		}

		public virtual IAction Clone()
		{
			return null;
		}

		public virtual IAction Reverse()
		{
			return null;
		}
	}

	/// <summary>
	/// 基础Action
	/// </summary>
	public abstract class IFiniteTimeAction:IAction
	{
		protected float m_duration;
		#region Duration get set m_duration
		public float Duration
		{
			get
			{
				return m_duration;
			}
			set
			{
				m_duration = value;
			}
		}
        #endregion

		//protected Func<float,float,float,float,float> m_easeFun;

		public IFiniteTimeAction()
		{
			m_duration = 0.0f;
		}
	}


	/// <summary>
	/// 空action，特殊情况用 外部基本不用
	/// </summary>
	#region ExtraAction
	internal class ExtraAction : IFiniteTimeAction
	{
		public static ExtraAction Create()
		{
			ExtraAction ret = new ExtraAction();
			return ret;
		}

		public override IAction Clone()
		{
			return Create();
		}

		public override IAction Reverse()
		{
			return Create();
		}

		public override void Update(float time)
		{
		}

		public override void Step(float dt)
		{
		}
	}
	#endregion
}

