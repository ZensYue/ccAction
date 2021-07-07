using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ccAction 
{
    /// <summary>
    /// Ease actions are created from other interval actions.The ease action will change the timeline of the inner action.
    /// 变速 
    /// </summary>
    #region ActionEase
    public class ActionEase : ActionInterval
    {
        protected ActionInterval m_inner;

        Func<float> m_easeFunc0 = null;
        Func<float, float> m_easeFunc1 = null;
        Func<float, float, float> m_easeFunc2 = null;
        Func<float, float, float, float> m_easeFunc3 = null;
        Func<float, float, float, float, float> m_easeFunc4 = null;
        Func<float, float, float, float, float, float> m_easeFunc5 = null;

        bool m_hasFunc = false;

        float f1;
        float f2;
        float f3;
        float f4;

        public static ActionEase Create(ActionInterval action)
        {
            var ret = new ActionEase();
            if (ret.InitWithAction(action))
            {
                return ret;
            }
            return null;
        }

        public void SetEase(Func<float> func0)
        {
            if (m_hasFunc) return;
            m_easeFunc0 = func0;
            m_hasFunc = true;
        }

        public void SetEase(Func<float,float> func,float a)
        {
            if (m_hasFunc) return;
            m_easeFunc1 = func;
            f1 = a;
            m_hasFunc = true;
        }

        public void SetEase(Func<float, float,float> func, float a,float b)
        {
            if (m_hasFunc) return;
            m_easeFunc2 = func;
            f1 = a;
            f2 = b;
            m_hasFunc = true;
        }

        public void SetEase(Func<float, float, float,float> func, float a, float b,float c)
        {
            if (m_hasFunc) return;
            m_easeFunc3 = func;
            f1 = a;
            f2 = b;
            f3 = c;
            m_hasFunc = true;
        }

        public void SetEase(Func<float, float, float, float,float> func, float a, float b, float c)
        {
            if (m_hasFunc) return;
            m_easeFunc4 = func;
            f1 = a;
            f2 = b;
            f3 = c;
            m_hasFunc = true;
        }

        public void SetEase(Func<float, float, float, float, float,float> func, float a, float b, float c, float d)
        {
            if (m_hasFunc) return;
            m_easeFunc5 = func;
            f1 = a;
            f2 = b;
            f3 = c;
            f4 = d;
            m_hasFunc = true;
        }

        public override IAction Clone()
        {
            this.LogError("ActionEase can be Cloned");
            return null;
        }

        public override IAction Reverse()
        {
            this.LogError("ActionEase can be Reversed");
            return null;
        }

        public bool InitWithAction(ActionInterval action)
        {
            if (base.InitWithDuration(action.Duration))
            {
                m_inner = action;
                return true;
            }
            return false;
        }

        public override void StartWithTarget()
        {
            if(!m_hasFunc)
                this.LogError("ActionEase not set ease func");

            base.StartWithTarget();
            m_inner.StartWithTarget();
        }

        public override void Stop()
        {
            m_inner.Stop();
            base.Stop();
        }

        public override void Update(float time)
        {
            if (m_easeFunc0 != null) time = m_easeFunc0();
            if (m_easeFunc1 != null) time = m_easeFunc1(time);
            if (m_easeFunc2 != null) time = m_easeFunc2(time,f1);
            if (m_easeFunc3 != null) time = m_easeFunc3(time,f1,f2);
            if (m_easeFunc4 != null) time = m_easeFunc4(time,f1,f2,f3);
            if (m_easeFunc5 != null) time = m_easeFunc5(time,f1,f2,f3,f4);
            m_inner.Update(time);
        }
    }
    #endregion
}

