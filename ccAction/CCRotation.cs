using UnityEngine;

namespace ccAction
{
    class RotationBy : ActionInterval
    {
        protected Vector3 m_rotationDelta;
        protected Vector3 m_startRotation;
        protected Vector3 m_previousRotation;

        public static RotationBy Create(float d, float x, float y, float z = 0f)
        {
            return Create(d, new Vector3(x, y, z));
        }

        public static RotationBy Create(float d, Vector3 deltaRotation)
        {
            var ret = new RotationBy();
            if (ret.InitWithDuration(d, deltaRotation))
            {
                return ret;
            }
            return null;
        }

        public bool InitWithDuration(float d, Vector3 deltaRotation)
        {
            if (base.InitWithDuration(d))
            {
                m_rotationDelta = deltaRotation;
                return true;
            }
            return false;
        }

        public override void StartWithTarget(Transform target)
        {
            base.StartWithTarget(target);
            m_previousRotation = m_startRotation = m_target.eulerAngles;
        }

        public override CCAction Clone()
        {
            return Create(m_duration, m_rotationDelta);
        }

        public override CCAction Reverse()
        {
            return Create(m_duration, -m_rotationDelta);
        }

        public override void Update(float t)
        {
            // CC_ENABLE_STACKABLE_ACTIONS
            var currentRotation = m_target.eulerAngles;
            var diff = currentRotation - m_previousRotation;
            m_startRotation = m_startRotation + diff;
            var newRotation = m_startRotation + (m_rotationDelta * t);
            m_target.eulerAngles = newRotation;
            m_previousRotation = newRotation;
        }
    }

    class RotationTo : ActionInterval
    {
        protected Vector3 m_startAngle;
        protected Vector3 m_dstAngle;
        protected Vector3 m_diffAngle;

        bool m_isZ = false;

        public static RotationTo Create(float d, float x, float y, float z = 0f)
        {
            return Create(d, new Vector3(x, y, z));
        }

        public static RotationTo Create(float d,float z)
        {
            var ret = new RotationTo();
            if (ret.InitWithDuration(d, new Vector3() { z = z}))
            {
                ret.m_isZ = true;
                return ret;
            }
            return null;
        }

        public static RotationTo Create(float d, Vector3 endRotation)
        {
            var ret = new RotationTo();
            if (ret.InitWithDuration(d, endRotation))
            {
                return ret;
            }
            return null;
        }

        public bool InitWithDuration(float d, Vector3 endRotation)
        {
            if (base.InitWithDuration(d))
            {
                m_dstAngle = endRotation;
                return true;
            }
            return false;
        }

        void calculateAngles(ref float startAngle, ref float diffAngle, ref float dstAngle)
        {
            if (startAngle > 0)
            {
                startAngle %= 360.0f;
            }
            else
            {
                startAngle %= -360.0f;
            }

            diffAngle = dstAngle - startAngle;
            if (diffAngle > 180)
            {
                diffAngle -= 360;
            }
            if (diffAngle < -180)
            {
                diffAngle += 360;
            }
        }
        public override void StartWithTarget(Transform target)
        {
            base.StartWithTarget(target);

            m_startAngle = m_target.eulerAngles;
            if(m_isZ)
            {
                m_dstAngle.x = m_startAngle.x;
                m_dstAngle.y = m_startAngle.y;
            }
            calculateAngles(ref m_startAngle.x,ref m_diffAngle.x,ref m_dstAngle.x);
            calculateAngles(ref m_startAngle.y,ref m_diffAngle.y,ref m_dstAngle.y);
            calculateAngles(ref m_startAngle.z,ref m_diffAngle.z,ref m_dstAngle.z);

            //_startAngle = m_endRotation - m_target.eulerAngles;
        }

        public override void Update(float time)
        {
            m_target.eulerAngles = new Vector3()
            {
                x = m_startAngle.x + m_diffAngle.x * time,
                y = m_startAngle.y + m_diffAngle.y * time,
                z = m_startAngle.z + m_diffAngle.z * time
            };
        }

        public override CCAction Clone()
        {
            return Create(m_duration, m_dstAngle);
        }

        public override CCAction Reverse()
        {
            //CCASSERT(false, "reverse() not supported in RotationTo");
            return null;
        }
    }
}
