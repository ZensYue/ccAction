using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ccAction 
{
    public abstract class CCActionStruct<T>: ActionInterval where T:struct
    {
        protected T m_from;
        //protected T m_delta;
        protected T m_to;
        protected T m_previous;

        Func<T> m_initFunc = null;
        Action<float,T> m_UpdateFunc = null;

        public bool InitWithDuration(float d,T from,T to)
        {
            if (base.InitWithDuration(d))
            {
                m_from = from;
                m_to = to;
                return true;
            }
            return false;
        }

        public CCActionStruct<T> SetInitFunc(Func<T> func)
        {
            m_initFunc = func;
            return this;
        }

        public CCActionStruct<T> SetUpdate(Action<float,T> func)
        {
            m_UpdateFunc = func;
            return this;
        }

        public override void StartWithTarget()
        {
            base.StartWithTarget();
            if (m_initFunc != null)
                m_from = m_initFunc();
            m_previous = m_from;
        }

        public override void Update(float t)
        {
            var result = UpdateResultValue(t);
            m_UpdateFunc?.Invoke(t, result);
        }

        protected abstract T UpdateResultValue(float t);
    }

    /// <summary>
    /// float
    /// </summary>
    public sealed class CCActionFloat : CCActionStruct<float>
    {
        public static CCActionFloat Create(float d, float from, float to)
        {
            var ret = new CCActionFloat();
            ret.InitWithDuration(d, from, to);
            return ret;
        }
        protected override float UpdateResultValue(float t)
        {
            return Mathf.Lerp(m_from, m_to, t);
        }
    }

    /// <summary>
    /// Vector2
    /// </summary>
    public sealed class CCActionVector2 : CCActionStruct<Vector2>
    {
        public static CCActionVector2 Create(float d, Vector2 from, Vector2 to)
        {
            var ret = new CCActionVector2();
            ret.InitWithDuration(d, from, to);
            return ret;
        }
        protected override Vector2 UpdateResultValue(float t)
        {
            return Vector2.Lerp(m_from, m_to, t);
        }
    }

    /// <summary>
    /// Vector3
    /// </summary>
    public sealed class CCActionVector3 : CCActionStruct<Vector3>
    {
        public static CCActionVector3 Create(float d, Vector3 from, Vector3 to)
        {
            var ret = new CCActionVector3();
            ret.InitWithDuration(d, from, to);
            return ret;
        }
        protected override Vector3 UpdateResultValue(float t)
        {
            return Vector3.Lerp(m_from, m_to, t);
        }
    }

    /// <summary>
    /// Vector4
    /// </summary>
    public sealed class CCActionVector4 : CCActionStruct<Vector4>
    {
        public static CCActionVector4 Create(float d, Vector4 from, Vector4 to)
        {
            var ret = new CCActionVector4();
            ret.InitWithDuration(d, from, to);
            return ret;
        }
        protected override Vector4 UpdateResultValue(float t)
        {
            return Vector4.Lerp(m_from, m_to, t);
        }
    }

    /// <summary>
    /// Color
    /// </summary>
    public sealed class CCActionColor : CCActionStruct<Color>
    {
        public static CCActionColor Create(float d, Color from, Color to)
        {
            var ret = new CCActionColor();
            ret.InitWithDuration(d, from, to);
            return ret;
        }
        protected override Color UpdateResultValue(float t)
        {
            return Color.Lerp(m_from, m_to, t);
        }
    }

    /// <summary>
    /// Quaternion
    /// </summary>
    public sealed class CCActionQuaternion : CCActionStruct<Quaternion>
    {
        public static CCActionQuaternion Create(float d, Quaternion from, Quaternion to)
        {
            var ret = new CCActionQuaternion();
            ret.InitWithDuration(d, from, to);
            return ret;
        }
        protected override Quaternion UpdateResultValue(float t)
        {
            return Quaternion.Lerp(m_from, m_to, t);
        }
    }
}

