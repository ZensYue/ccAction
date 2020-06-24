using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ccAction
{
	/// <summary>
	/// 为了避免和System.Action冲突，这里把名字改成CCAction
	/// </summary>
	public abstract class CCAction
	{
		protected static int Tag = 0;

		protected Transform m_originalTarget;
		#region OriginalTarget get set m_originalTarget
		public Transform OriginalTarget
		{
			get
			{
				return m_originalTarget;
			}
			set
			{
				m_originalTarget = value;
			}
		}
		#endregion Target

		protected Transform m_target;
		#region Target get set m_target
		public Transform Target { 
			get {
				return m_target;
			}
			set {
				m_target = value;
			} 
		}
		#endregion Target

		//protected Component m_component;

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

		public CCAction()
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
		public virtual void StartWithTarget(Transform target)
		{
			m_originalTarget = target;
			if (m_target != null)
				return;
			m_target =  target;
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

		public virtual CCAction Clone()
		{
			return null;
		}

		public virtual CCAction Reverse()
		{
			return null;
		}
	}

	public class FiniteTimeAction:CCAction
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

		public FiniteTimeAction()
		{
			m_duration = 0.0f;
		}		
	}

	//这两个action和 Transform无关，需要特殊处理。用于变速、跟随；后面添加
	#region Speed todo
	#endregion

	#region Follow todo
	#endregion
}

