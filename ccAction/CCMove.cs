using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ccAction
{
    class MoveBy : ActionInterval
    {
        protected Vector3 m_positionDelta;
        protected Vector3 m_startPosition;
        protected Vector3 m_previousPosition;

        public static MoveBy Create(float d, float x,float y,float z = 0)
        {
            return Create(d, new Vector3(x, y, z));
        }

        public static MoveBy Create(float d, Vector3 deltaPosition)
        {
            var ret = new MoveBy();
            if(ret.InitWithDuration(d,deltaPosition))
            {
                return ret;
            }
            return null;
        }

        public bool InitWithDuration(float d,Vector3 deltaPosition)
        {
            if(base.InitWithDuration(d))
            {
                m_positionDelta = deltaPosition;
                return true;
            }
            return false;
        }

        public override void StartWithTarget(Transform target)
        {
            base.StartWithTarget(target);
            m_previousPosition = m_startPosition = m_target.localPosition;
        }

        public override CCAction Clone()
        {
            return Create(m_duration, m_positionDelta);
        }

        public override CCAction Reverse()
        {
            return Create(m_duration, -m_positionDelta);
        }

        public override void Update(float t)
        {
            // CC_ENABLE_STACKABLE_ACTIONS
            var currentPos = m_target.localPosition;
            var diff = currentPos - m_previousPosition;
            m_startPosition = m_startPosition + diff;
            var newPos = m_startPosition + (m_positionDelta * t);
            m_target.localPosition = newPos;
            m_previousPosition = newPos;
        }
    }

    class MoveTo:MoveBy
    {
        protected Vector3 m_endPosition;

        public static new MoveTo Create(float d, float x, float y, float z = 0)
        {
            return Create(d, new Vector3(x, y, z));
        }

        public static new MoveTo Create(float d, Vector3 endPosition)
        {
            var ret = new MoveTo();
            if (ret.InitWithDuration(d, endPosition))
            {
                return ret;
            }
            return null;
        }

        public new bool InitWithDuration(float d, Vector3 endPosition)
        {
            if (base.InitWithDuration(d))
            {
                m_endPosition = endPosition;
                return true;
            }
            return false;
        }

        public override void StartWithTarget(Transform target)
        {
            base.StartWithTarget(target);
            m_positionDelta = m_endPosition - m_target.localPosition;
        }

        public override CCAction Clone()
        {
            //CCASSERT(false, "reverse() not supported in MoveTo");
            return null;
        }

        public override CCAction Reverse()
        {
            //CCASSERT(false, "reverse() not supported in MoveTo");
            return null;
        }
    }
}
