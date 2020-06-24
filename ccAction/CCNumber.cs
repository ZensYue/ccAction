using UnityEngine.UI;

namespace ccAction
{
    public class NumberBy:ActionInterval
    {
        string m_format;
        float m_start_value;
        protected float m_delta_value;
        Text m_text;

        public static NumberBy Create(float d,string format,float start_value, float delta_value)
        {
            var ret = new NumberBy();
            if(ret.InitWithDuration(d, format, start_value, delta_value))
            {
                return ret;
            }
            return null;
        }
        
        protected bool InitWithDuration(float d,string format,float start_value, float delta_value)
        {
            if(base.InitWithDuration(d))
            {
                m_format = format;
                m_start_value = start_value;
                m_delta_value = delta_value;
                return true;
            }
            return false;
        }

        public NumberBy InitSubjectComponent(Text text)
        {
            m_text = text;
            return this;
        }

        public override void Update(float time)
        {
            if (m_text)
            {
                var new_value = m_start_value + m_delta_value * time;
                m_text.text = string.Format(m_format, new_value);
            }
        }
    }

    public class NumberTo:NumberBy
    {
        public static new NumberTo Create(float d, string format, float start_value, float end_value)
        {
            var ret = new NumberTo();
            if (ret.InitWithDuration(d, format, start_value, end_value))
            {
                ret.m_delta_value = end_value - start_value;
                return ret;
            }
            return null;
        }
    }
}