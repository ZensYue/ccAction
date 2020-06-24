using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ccAction
{
    /// <summary>
    /// Ease actions are created from other interval actions.The ease action will change the timeline of the inner action.
    /// </summary>
    #region ActionEase
    public abstract class ActionEase:ActionInterval
    {
        protected ActionInterval m_inner;
        public override CCAction Clone()
        {
            this.LogError("ActionEase can be Cloned");
            return null;
        }

        public override CCAction Reverse()
        {
            this.LogError("ActionEase can be Reversed");
            return null;
        }

        public bool InitWithAction(ActionInterval action)
        {
            if(base.InitWithDuration(action.Duration))
            {
                m_inner = action;
                return true;
            }
            return false;
        }

        public override void StartWithTarget(Transform target)
        {
            base.StartWithTarget(target);
            m_inner.StartWithTarget(m_target);
        }

        public override void Stop()
        {
            m_inner.Stop();
            base.Stop();
        }

        public override void Update(float time)
        {
            m_inner.Update(time);
        }
    }
    #endregion

    /// <summary>
    /// Ease the inner action with specified rate.
    /// </summary>
    #region EaseRateAction
    public class EaseRateAction : ActionEase
    {
        protected float m_rate;
        #region rate get set
        public float Rate
        {
            get
            {
                return m_rate;
            }
            set
            {
                m_rate = value;
            }
        }
        #endregion

        public EaseRateAction Create(ActionInterval action,float rate)
        {
            var ret = new EaseRateAction();
            if(ret.InitWithAction(action, rate))
            {
                return ret;
            }
            return null;
        }

        public bool InitWithAction(ActionInterval action,float rate)
        {
            base.InitWithAction(action);
            m_rate = rate;
            return true;
        }
    }
    #endregion

    /// <summary>
    /// The timeline of inner action will be changed by:\f${ time}^{ rate }\f$.
    /// </summary>
    #region EaseIn rate >1 先慢后快；0-1相反
    public class EaseIn : EaseRateAction
    {
        public static new EaseIn Create(ActionInterval action,float rate)
        {
            var ret = new EaseIn();
            if(ret.InitWithAction(action,rate))
            {
                return ret;
            }
            return null;
        }

        public override void Update(float time)
        {
            m_inner.Update(TweenFunc.EaseIn(time,m_rate));
        }

        public override CCAction Clone()
        {
            return Create(m_inner,m_rate);
        }

        public override CCAction Reverse()
        {
            return Create(m_inner, 1/m_rate);
        }
    }
    #endregion

    /// <summary>
    /// The timeline of inner action will be changed by:\f${ time}^{ 1/rate }\f$.
    /// </summary>
    #region EaseOut rate >1 先快后慢；0-1相反
    public class EaseOut : EaseRateAction
    {
        public static new EaseOut Create(ActionInterval action, float rate)
        {
            var ret = new EaseOut();
            if (ret.InitWithAction(action, rate))
            {
                return ret;
            }
            return null;
        }

        public override void Update(float time)
        {
            m_inner.Update(TweenFunc.EaseOut(time, m_rate));
        }

        public override CCAction Clone()
        {
            return Create(m_inner, m_rate);
        }

        public override CCAction Reverse()
        {
            return Create(m_inner, 1 / m_rate);
        }
    }
    #endregion

    #region EaseInOut rate >1 慢快慢，正态分布；0-1相反
    public class EaseInOut : EaseRateAction
    {
        public static new EaseOut Create(ActionInterval action, float rate)
        {
            var ret = new EaseOut();
            if (ret.InitWithAction(action, rate))
            {
                return ret;
            }
            return null;
        }

        public override void Update(float time)
        {
            m_inner.Update(TweenFunc.EaseInOut(time, m_rate));
        }

        public override CCAction Clone()
        {
            return Create(m_inner, m_rate);
        }

        public override CCAction Reverse()
        {
            return Create(m_inner, 1 / m_rate);
        }
    }
    #endregion
}
