using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ccAction
{
	/// <summary>
	/// 容器Action 顺序 并行 重复 无限重复
	/// </summary>
    public abstract class ActionContainer: ActionInterval
    {
    }

	#region Sequence 顺序执行action
	public class CCActionSequence : ActionContainer
	{
		IFiniteTimeAction[] m_actions = new IFiniteTimeAction[2];
		float m_split;
		int m_last;

		public static CCActionSequence Create(IFiniteTimeAction action1, params IFiniteTimeAction[] args)
		{
			IFiniteTimeAction now;
			IFiniteTimeAction prev = action1;
			for (int i = 0; i < args.Length; i++)
			{
				now = args[i];
				prev = CreateWithTwoActions(prev, now);
			}
			if (args.Length == 0)
				prev = CreateWithTwoActions(prev, ExtraAction.Create());
			return prev as CCActionSequence;
		}

		public static CCActionSequence CreateWithTwoActions(IFiniteTimeAction actionOne, IFiniteTimeAction actionTwo)
		{
			CCActionSequence sequence = new CCActionSequence();
			if (sequence != null && sequence.InitWithTwoActions(actionOne, actionTwo))
				return sequence;
			return null;
		}

		public bool InitWithTwoActions(IFiniteTimeAction actionOne, IFiniteTimeAction actionTwo)
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

		public override IAction Clone()
		{
			if (m_actions[0] != null && m_actions[1] != null)
				return Create(m_actions[0].Clone() as IFiniteTimeAction, m_actions[1].Clone() as IFiniteTimeAction);
			return null;
		}

		protected CCActionSequence()
		{
			m_split = 0;
			m_actions[0] = null;
			m_actions[1] = null;
		}

		public override void StartWithTarget()
		{
			if (m_duration > float.Epsilon)
				m_split = m_actions[0].Duration > float.Epsilon ? m_actions[0].Duration / m_duration : 0;

			base.StartWithTarget();
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
					m_actions[0].StartWithTarget();
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
				m_actions[found].StartWithTarget();
			}
			m_actions[found].Update(new_t);
			m_last = found;
		}

		public override IAction Reverse()
		{
			if (m_actions[0] != null && m_actions[1] != null)
				return CCActionSequence.CreateWithTwoActions(m_actions[1].Reverse() as IFiniteTimeAction, m_actions[0].Reverse() as IFiniteTimeAction);
			return null;
		}
	}
	#endregion

	#region Spawn 并行执行
	public class CCActionSpawn : ActionContainer
	{
		IFiniteTimeAction m_one;
		IFiniteTimeAction m_two;
		public static CCActionSpawn Create(IFiniteTimeAction action1, params IFiniteTimeAction[] args)
		{
			IFiniteTimeAction now;
			IFiniteTimeAction prev = action1;
			for (int i = 0; i < args.Length; i++)
			{
				now = args[i];
				prev = CreateWithTwoActions(prev, now);
			}
			if (args.Length == 0)
				prev = CreateWithTwoActions(prev, ExtraAction.Create());
			return prev as CCActionSpawn;
		}

		public static CCActionSpawn CreateWithTwoActions(IFiniteTimeAction actionOne, IFiniteTimeAction actionTwo)
		{
			CCActionSpawn spawn = new CCActionSpawn();
			if (spawn != null && spawn.InitWithTwoActions(actionOne, actionTwo))
				return spawn;
			return null;
		}

		public bool InitWithTwoActions(IFiniteTimeAction action1, IFiniteTimeAction action2)
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
					m_two = CCActionSequence.CreateWithTwoActions(action2, CCActionDelay.Create(d1 - d2));
				}
				else if (d1 < d2)
				{
					m_one = CCActionSequence.CreateWithTwoActions(action1, CCActionDelay.Create(d2 - d1));
				}
				ret = true;
			}
			return ret;
		}

		public override IAction Clone()
		{
			if (m_one != null && m_two != null)
				return CreateWithTwoActions(m_one.Clone() as IFiniteTimeAction, m_two.Clone() as IFiniteTimeAction);
			return null;
		}

		public override void StartWithTarget()
		{
			base.StartWithTarget();
			m_one.StartWithTarget();
			m_two.StartWithTarget();
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

		public override IAction Reverse()
		{
			if (m_one != null && m_two != null)
				return CCActionSpawn.CreateWithTwoActions(m_one.Reverse() as IFiniteTimeAction, m_two.Reverse() as IFiniteTimeAction);
			return null;
		}
	}
	#endregion

	#region Repeat 重复
	public class CCActionRepeat : ActionContainer
	{
		uint m_times;
		uint m_total;
		float m_nextDt;
		bool m_actionInstant;
		IFiniteTimeAction m_innerAction;
		public static CCActionRepeat Create(IFiniteTimeAction action, uint times)
		{
			CCActionRepeat repeat = new CCActionRepeat();
			if (repeat.InitWithAction(action, times))
				return repeat;
			return null;
		}

		public bool InitWithAction(IFiniteTimeAction action, uint times)
		{
			if (action != null && base.InitWithDuration(action.Duration * times))
			{
				this.m_times = times;
				this.m_innerAction = action;
				m_actionInstant = action is CCActionExecute;
				m_total = 0;
				return true;
			}
			return false;
		}

		public override IAction Clone()
		{
			return CCActionRepeat.Create(m_innerAction.Clone() as IFiniteTimeAction, m_times);
		}

		public override void StartWithTarget()
		{
			m_total = 0;
			m_nextDt = m_innerAction.Duration / m_duration;
			base.StartWithTarget();
			m_innerAction.StartWithTarget();
		}

		public override void Stop()
		{
			m_innerAction.Stop();
			base.Stop();
		}

		public override void Update(float dt)
		{
			if (dt >= m_nextDt)
			{
				while (dt >= m_nextDt && m_total < m_times)
				{
					m_innerAction.Update(1.0f);
					m_total++;

					m_innerAction.Stop();
					m_innerAction.StartWithTarget();
					m_nextDt = m_innerAction.Duration / m_duration * (m_total + 1);
				}

				if (Math.Abs(dt - 1.0f) < float.Epsilon && m_total < m_times)
				{
					m_innerAction.Update(1.0f);

					m_total++;
				}

				if (!m_actionInstant)
				{
					if (m_total == m_times)
					{
						m_innerAction.Stop();
					}
					else
					{
						m_innerAction.Update(dt - (m_nextDt - m_innerAction.Duration / m_duration));
					}
				}
			}
			else
			{
				var progress = (dt * m_times) % 1.0f;
				m_innerAction.Update(progress);
			}
		}

		public override bool IsDone()
		{
			return m_total == m_times;
		}

		public override IAction Reverse()
		{
			return CCActionRepeat.Create(m_innerAction.Reverse() as IFiniteTimeAction, m_times);
		}

		internal static object Create(CCActionRepeat repeat)
		{
			throw new NotImplementedException();
		}
	}
	#endregion

	#region RepeatForever 无限重复
	public class CCActionRepeatForever : ActionContainer
	{
		ActionInterval m_innerAction;
		public static CCActionRepeatForever Create(ActionInterval action)
		{
			CCActionRepeatForever ret = new CCActionRepeatForever();
			if (ret.InitWithAction(action))
				return ret;
			return null;
		}

		protected bool InitWithAction(ActionInterval action)
		{
			if (action == null)
			{
				this.LogError("RepeatForever:initWithAction error:action is nullptr!");
				return false;
			}

			//if(action.Duration>0)
			//    InitWithDuration(action.Duration);
			//else
			//    InitWithDuration(0.1f);

			m_innerAction = action;
			return true;
		}

		public override IAction Clone()
		{
			return Create(m_innerAction.Clone() as ActionInterval);
		}

		public override void StartWithTarget()
		{
			base.StartWithTarget();
			m_innerAction.StartWithTarget();
		}

		public override void Step(float dt)
		{
			m_innerAction.Step(dt);
			if (m_innerAction.IsDone() && m_innerAction.Duration > 0)
			{
				float diff = m_innerAction.Elapsed - m_innerAction.Duration;
				if (diff > m_innerAction.Duration)
					diff %= m_innerAction.Duration;
				m_innerAction.StartWithTarget();
				m_innerAction.Step(0.0f);
				m_innerAction.Step(diff);
			}
		}

		public override bool IsDone()
		{
			return false;
		}

		public override IAction Reverse()
		{
			return Create(m_innerAction.Reverse() as ActionInterval);
		}
	}
	#endregion
}
