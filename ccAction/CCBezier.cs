using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ccAction
{
    public struct CCBezierConfig
    {
        //! end position of the bezier
        public Vector3 m_endPosition;
        //! Bezier control point 1
        public Vector3 m_controlPoint_1;
        //! Bezier control point 2
        public Vector3 m_controlPoint_2;
    }

    /// <summary>
    /// 二段贝塞尔曲线
    /// </summary>
    class BezierBy:ActionInterval
    {
        static float bezierat(float a, float b, float c, float d, float t)
        {
            return (Mathf.Pow(1 - t, 3) * a +
                    3 * t * (Mathf.Pow(1 - t, 2)) * b +
                    3 * Mathf.Pow(t, 2) * (1 - t) * c +
                    Mathf.Pow(t, 3) * d);
        }

        protected CCBezierConfig m_bezierConfig;
        protected Vector3 m_startPosition;
        protected Vector3 m_previousPosition;

        public static BezierBy Create(float d,Vector3 endPosition,Vector3 controlPoint_1, Vector3 controlPoint_2)
        {
            CCBezierConfig bezierConfig;
            bezierConfig.m_endPosition = endPosition;
            bezierConfig.m_controlPoint_1 = controlPoint_1;
            bezierConfig.m_controlPoint_2 = controlPoint_2;
            return Create(d, bezierConfig);
        }

        public static BezierBy Create(float d, CCBezierConfig bezierConfig)
        {
            var ret = new BezierBy();
            if(ret.InitWithDuration(d,bezierConfig))
            {
                return ret;
            }
            return null;
        }

        public bool InitWithDuration(float d, CCBezierConfig bezierConfig)
        {
            if(base.InitWithDuration(d))
            {
                m_bezierConfig = bezierConfig;
                return true;
            }
            return false;
        }

        public override void StartWithTarget(Transform target)
        {
            base.StartWithTarget(target);
            m_previousPosition = m_startPosition = m_target.localPosition;
        }

        public override void Update(float time)
        {
            if (m_target)
            {
                float xa = 0;
                float xb = m_bezierConfig.m_controlPoint_1.x;
                float xc = m_bezierConfig.m_controlPoint_2.x;
                float xd = m_bezierConfig.m_endPosition.x;

                float ya = 0;
                float yb = m_bezierConfig.m_controlPoint_1.y;
                float yc = m_bezierConfig.m_controlPoint_2.y;
                float yd = m_bezierConfig.m_endPosition.y;

                float za = 0;
                float zb = m_bezierConfig.m_controlPoint_1.z;
                float zc = m_bezierConfig.m_controlPoint_2.z;
                float zd = m_bezierConfig.m_endPosition.z;

                float x = bezierat(xa, xb, xc, xd, time);
                float y = bezierat(ya, yb, yc, yd, time);
                float z = bezierat(za, zb, zc, zd, time);

                Vector3 currentPos = m_target.localPosition;
                Vector3 diff = currentPos - m_previousPosition;
                m_startPosition = m_startPosition + diff;

                Vector3 newPos = m_startPosition + new Vector3(x,y,z);
                m_target.localPosition = newPos;
                m_previousPosition = newPos;
            }
        }

        public override CCAction Clone()
        {
            return Create(m_duration, m_bezierConfig);
        }

        public override CCAction Reverse()
        {
            CCBezierConfig bezierConfig;
            bezierConfig.m_endPosition = -m_bezierConfig.m_endPosition;
            bezierConfig.m_controlPoint_1 = m_bezierConfig.m_controlPoint_1 - m_startPosition;
            bezierConfig.m_controlPoint_2 = m_bezierConfig.m_controlPoint_2 - m_startPosition;
            return Create(m_duration, bezierConfig);
        }
    }

    class BezierTo:BezierBy
    {
        protected CCBezierConfig m_toConfig;

        /// <summary>
        /// 二阶贝塞尔曲线
        /// </summary>
        /// <param name="d"></param>
        /// <param name="endPosition">结束点</param>
        /// <param name="controlPoint_1">控制点1</param>
        /// <param name="controlPoint_2">控制点2</param>
        /// <returns></returns>
        public static new BezierTo Create(float d, Vector3 endPosition, Vector3 controlPoint_1, Vector3 controlPoint_2)
        {
            CCBezierConfig bezierConfig;
            bezierConfig.m_endPosition = endPosition;
            bezierConfig.m_controlPoint_1 = controlPoint_1;
            bezierConfig.m_controlPoint_2 = controlPoint_2;
            return Create(d, bezierConfig);
        }

        public static new BezierTo Create(float d, CCBezierConfig bezierConfig)
        {
            var ret = new BezierTo();
            if (ret.InitWithDuration(d, bezierConfig))
            {
                return ret;
            }
            return null;
        }

        public new bool InitWithDuration(float d, CCBezierConfig bezierConfig)
        {
            if (base.InitWithDuration(d))
            {
                m_toConfig = bezierConfig;
                return true;
            }
            return false;
        }

        public override void StartWithTarget(Transform target)
        {
            base.StartWithTarget(target);
            m_bezierConfig.m_controlPoint_1 = m_toConfig.m_controlPoint_1 - m_startPosition;
            m_bezierConfig.m_controlPoint_2 = m_toConfig.m_controlPoint_2 - m_startPosition;
            m_bezierConfig.m_endPosition = m_toConfig.m_endPosition - m_startPosition;
        }
        public override CCAction Clone()
        {
            return Create(m_duration, m_bezierConfig);
        }

        public override CCAction Reverse()
        {
            //CCASSERT(false, "CCBezierTo doesn't support the 'reverse' method");
            return null;
        }
    }
}

