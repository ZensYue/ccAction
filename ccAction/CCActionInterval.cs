using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ccAction {
	#region ExtraAction 空action，特殊情况用 外部基本不用
	public class ExtraAction : FiniteTimeAction
	{
		public static ExtraAction Create()
		{
			ExtraAction ret = new ExtraAction();
			return ret;
		}

		public override CCAction Clone()
		{
			return Create();
		}

		public override CCAction Reverse()
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

    #region ActionInterval
    public class ActionInterval : FiniteTimeAction
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

		public override void StartWithTarget(Transform target)
		{
			base.StartWithTarget(target);
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

		/// <summary>
		/// 主体有可能用到 Component 里面的属性 在具体的派生类里面实现，基类只提供接口案例
		/// </summary>
		/// <typeparam name="Component"></typeparam>
		/// <returns></returns>
		public virtual ActionInterval InitSubjectComponent(Component component)
		{
			if (component == null)
				this.LogError("InitSubjectComponent component is null");
			return this;
		}

		/// <summary>
		/// 用于同一个action，不同片段对应不同的transform
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public ActionInterval InitSubjectTransform(Transform target)
		{
			if (target == null)
				this.LogError("InitSubjectTransform target is null");
			m_target = target;
			return this;
		}
	}
	#endregion

    #region Sequence 顺序执行action
    public class Sequence : ActionInterval
	{
		FiniteTimeAction[] m_actions = new FiniteTimeAction[2];
		float m_split;
		int m_last;

		public static Sequence Create(FiniteTimeAction action1, params FiniteTimeAction[] args)
		{
			FiniteTimeAction now;
			FiniteTimeAction prev = action1;
			for (int i = 0; i < args.Length; i++)
			{
				now = args[i];
				prev = CreateWithTwoActions(prev, now);
			}
			if (args.Length==0)
				prev = CreateWithTwoActions(prev, ExtraAction.Create());
			return prev as Sequence;
		}

		public static Sequence CreateWithTwoActions(FiniteTimeAction actionOne, FiniteTimeAction actionTwo)
		{
			Sequence sequence = new Sequence();
			if (sequence != null && sequence.InitWithTwoActions(actionOne, actionTwo))
				return sequence;
			return null;
		}

		public bool InitWithTwoActions(FiniteTimeAction actionOne, FiniteTimeAction actionTwo)
		{
			if (actionOne == null || actionTwo == null)
			{
				this.LogError("InitWithTwoActions action is null");
			}
			float d = actionOne.Duration + actionTwo.Duration;
			base.InitWithDuration(d);
			m_actions[0] = actionOne;
			m_actions[1] = actionTwo;
			return true;
		}

		public override CCAction Clone()
		{
			if (m_actions[0] != null && m_actions[1] != null)
				return Create(m_actions[0].Clone() as FiniteTimeAction, m_actions[1].Clone() as FiniteTimeAction);
			return null;
		}

		protected Sequence()
		{
			m_split = 0;
			m_actions[0] = null;
			m_actions[1] = null;
		}

		public override void StartWithTarget(Transform target)
		{
			if (m_duration > float.Epsilon)
				m_split = m_actions[0].Duration > float.Epsilon ? m_actions[0].Duration / m_duration : 0;

			base.StartWithTarget(target);
			m_last = -1;
		}

		public override void Stop()
		{
			if (m_last != -1 && m_actions[m_last] != null)
				m_actions[m_last].Stop();
			base.Stop();
		}

		public override void Update(float time)
		{
			int found = 0;
			float new_t = 0.0f;
			if (time < m_split)
			{
				// action[0]
				found = 0;
				if (m_split != 0)
					new_t = time / m_split;
				else
					new_t = 1;

			}
			else
			{
				// action[1]
				found = 1;
				if (m_split == 1)
					new_t = 1;
				else
					new_t = (time - m_split) / (1 - m_split);
			}

			if (found == 1)
			{
				if (m_last == -1)
				{
					m_actions[0].StartWithTarget(m_target);
					m_actions[0].Update(1.0f);
					m_actions[0].Stop();
				}
				else if (m_last == 0)
				{
					m_actions[0].Update(1.0f);
					m_actions[0].Stop();
				}
			}
			else if (found == 0 && m_last == 1)
			{
				m_actions[1].Update(0);
				m_actions[1].Stop();
			}
			// Last action found and it is done.
			if (found == m_last && m_actions[found].IsDone())
			{
				return;
			}

			// Last action found and it is done
			if (found != m_last)
			{
				m_actions[found].StartWithTarget(m_target);
			}
			m_actions[found].Update(new_t);
			m_last = found;
		}

		public override CCAction Reverse()
		{
			if (m_actions[0] != null && m_actions[1] != null)
				return Sequence.CreateWithTwoActions(m_actions[1].Reverse() as FiniteTimeAction, m_actions[0].Reverse() as FiniteTimeAction);
			return null;
		}
	}
	#endregion

	#region Spawn 并行执行
	public class Spawn : ActionInterval
	{
		FiniteTimeAction m_one;
		FiniteTimeAction m_two;
		public static Spawn Create(FiniteTimeAction action1, params FiniteTimeAction[] args)
		{
			FiniteTimeAction now;
			FiniteTimeAction prev = action1;
			for (int i = 0; i < args.Length; i++)
			{
				now = args[i];
				prev = CreateWithTwoActions(prev, now);
			}
			if (args.Length == 0)
				prev = CreateWithTwoActions(prev, ExtraAction.Create());
			return prev as Spawn;
		}

		public static Spawn CreateWithTwoActions(FiniteTimeAction actionOne, FiniteTimeAction actionTwo)
		{
			Spawn spawn = new Spawn();
			if (spawn != null && spawn.InitWithTwoActions(actionOne, actionTwo))
				return spawn;
			return null;
		}

		public bool InitWithTwoActions(FiniteTimeAction action1, FiniteTimeAction action2)
		{
			if (action1 == null || action2 == null)
			{
				this.LogError("initWithTwoActions action is null!!");
			}
			bool ret = false;
			var d1 = action1.Duration;
			var d2 = action2.Duration;
			if (base.InitWithDuration(Math.Max(d1, d2)))
			{
				m_one = action1;
				m_two = action2;
				if (d1 > d2)
				{
					m_two = Sequence.CreateWithTwoActions(action2, DelayTime.Create(d1 - d2));
				}
				else if (d1 < d2)
				{
					m_one = Sequence.CreateWithTwoActions(action1, DelayTime.Create(d2 - d1));
				}
				ret = true;
			}
			return ret;
		}

		public override CCAction Clone()
		{
			if (m_one != null && m_two != null)
				return CreateWithTwoActions(m_one.Clone() as FiniteTimeAction, m_two.Clone() as FiniteTimeAction);
			return null;
		}

		public override void StartWithTarget(Transform target)
		{
			base.StartWithTarget(target);
			m_one.StartWithTarget(m_target);
			m_two.StartWithTarget(m_target);
		}

		public override void Stop()
		{
			if (m_one != null)
				m_one.Stop();
			if (m_two != null)
				m_two.Stop();
			base.Stop();
		}

		public override void Update(float time)
		{
			if (m_one != null)
				m_one.Update(time);
			if (m_two != null)
				m_two.Update(time);
		}

		public override CCAction Reverse()
		{
			if (m_one != null && m_two != null)
				return Spawn.CreateWithTwoActions(m_one.Reverse() as FiniteTimeAction, m_two.Reverse() as FiniteTimeAction);
			return null;
		}
	}
	#endregion

	#region DelayTime 延迟执行
	public class DelayTime : ActionInterval
	{
		public static DelayTime Create(float d)
		{
			var action = new DelayTime();
			if (action != null && action.InitWithDuration(d))
				return action;
			return null;
		}

		public override CCAction Clone()
		{
			return Create(m_duration);
		}

		public override void Update(float time)
		{
			//Debug.Log($"Elapsed = {Elapsed},m_duration = {m_duration},IsDone = {IsDone()},cur_time = {Time.time}");
		}

		public override CCAction Reverse()
		{
			return Create(m_duration);
		}
	}
    #endregion
}


