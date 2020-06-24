using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ccAction 
{
	public class Repeat : ActionInterval
    {
        uint m_times;
        uint m_total;
        float m_nextDt;
        bool m_actionInstant;
        FiniteTimeAction m_innerAction;
        public static Repeat Create(FiniteTimeAction action, uint times)
        {
            Repeat repeat = new Repeat();
            if (repeat.InitWithAction(action, times))
                return repeat;
            return null;
        }

        public bool InitWithAction(FiniteTimeAction action, uint times)
        {
            if (action != null && base.InitWithDuration(action.Duration * times))
            {
                this.m_times = times;
                this.m_innerAction = action;
                m_actionInstant = action is ActionInstant;
                m_total = 0;
                return true;
            }
            return false;
        }

        public override CCAction Clone()
        {
            return Repeat.Create(m_innerAction.Clone() as FiniteTimeAction, m_times);
        }

        public override void StartWithTarget(Transform target)
        {
            m_total = 0;
            m_nextDt = m_innerAction.Duration / m_duration;
            base.StartWithTarget(target);
            m_innerAction.StartWithTarget(m_target);
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
                    m_innerAction.StartWithTarget(m_target);
                    m_nextDt = m_innerAction.Duration/m_duration * (m_total+1);
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
                        m_innerAction.Update(dt - (m_nextDt - m_innerAction.Duration/m_duration));
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

        public override CCAction Reverse()
        {
            return Repeat.Create(m_innerAction.Reverse() as FiniteTimeAction, m_times);
        }

        internal static object Create(Repeat repeat)
        {
            throw new NotImplementedException();
        }
    }

    //
    // RepeatForever
    //
    public class RepeatForever : ActionInterval
    {
        ActionInterval m_innerAction;
        public static RepeatForever Create(ActionInterval action)
        {
            RepeatForever ret = new RepeatForever();
            if (ret.InitWithAction(action))
                return ret;
            return null;
        }

        protected bool InitWithAction(ActionInterval action)
        {
            if (action == null)
            {
                this.LogError("RepeatForever::initWithAction error:action is nullptr!");
                return false;
            }

            //if(action.Duration>0)
            //    InitWithDuration(action.Duration);
            //else
            //    InitWithDuration(0.1f);

            m_innerAction = action;
            return true;
        }

        public override CCAction Clone()
        {
            return Create(m_innerAction.Clone() as ActionInterval);
        }

        public override void StartWithTarget(Transform target)
        {
            base.StartWithTarget(target);
            m_innerAction.StartWithTarget(m_target);
        }

        public override void Step(float dt)
        {
            m_innerAction.Step(dt);
            if (m_innerAction.IsDone() && m_innerAction.Duration > 0)
            {
                float diff = m_innerAction.Elapsed - m_innerAction.Duration;
                if (diff > m_innerAction.Duration)
                    diff %= m_innerAction.Duration;
                m_innerAction.StartWithTarget(m_target);
                m_innerAction.Step(0.0f);
                m_innerAction.Step(diff);
            }
        }

        public override bool IsDone()
        {
            return false;
        }

        public override CCAction Reverse()
        {
            return Create(m_innerAction.Reverse() as ActionInterval);
        }
    }
}
