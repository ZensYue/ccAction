using UnityEngine;

namespace ccAction
{
    class ScaleBy : ActionInterval
    {
        protected Vector3 m_scaleDelta;
        protected Vector3 m_startScale;
        protected Vector3 m_previousScale;

        public static ScaleBy Create(float d, float x, float y, float z = 0)
        {
            return Create(d, new Vector3(x, y, z));
        }

        public static ScaleBy Create(float d, Vector3 deltaScale)
        {
            var ret = new ScaleBy();
            if (ret.InitWithDuration(d, deltaScale))
            {
                return ret;
            }
            return null;
        }

        public bool InitWithDuration(float d, Vector3 deltaScale)
        {
            if (base.InitWithDuration(d))
            {
                m_scaleDelta = deltaScale;
                return true;
            }
            return false;
        }

        public override void StartWithTarget(Transform target)
        {
            base.StartWithTarget(target);
            m_previousScale = m_startScale = m_target.localPosition;
        }

        public override CCAction Clone()
        {
            return Create(m_duration, m_scaleDelta);
        }

        public override CCAction Reverse()
        {
            return Create(m_duration, -m_scaleDelta);
        }

        public override void Update(float t)
        {
            // CC_ENABLE_STACKABLE_ACTIONS
            var currentScale = m_target.localScale;
            var diff = currentScale - m_previousScale;
            m_startScale = m_startScale + diff;
            var newScale = m_startScale + (m_scaleDelta * t);
            m_target.localScale = newScale;
            m_previousScale = newScale;
        }
    }

    class ScaleTo : ScaleBy
    {
        protected Vector3 m_endScale;

        public static new ScaleTo Create(float d, float x, float y, float z = 1f)
        {
            return Create(d, new Vector3(x, y, z));
        }

        public static new ScaleTo Create(float d, Vector3 endScale)
        {
            var ret = new ScaleTo();
            if (ret.InitWithDuration(d, endScale))
            {
                return ret;
            }
            return null;
        }

        public new bool InitWithDuration(float d, Vector3 endScale)
        {
            if (base.InitWithDuration(d))
            {
                m_endScale = endScale;
                return true;
            }
            return false;
        }

        public override void StartWithTarget(Transform target)
        {
            base.StartWithTarget(target);
            m_scaleDelta = m_endScale - m_target.localScale;
        }

        public override CCAction Clone()
        {
            //CCASSERT(false, "reverse() not supported in ScaleTo");
            return null;
        }

        public override CCAction Reverse()
        {
            //CCASSERT(false, "reverse() not supported in ScaleTo");
            return null;
        }
    }
}
