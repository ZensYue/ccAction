using UnityEngine;
using UnityEngine.UI;

namespace ccAction
{
    public class FillAmountBy : ActionInterval
    {
        protected float m_start_value;
        protected float m_delta_value;
        protected Image m_image;

        public static FillAmountBy Create(float d, float delta_value)
        {
            var ret = new FillAmountBy();
            if (ret.InitWithDuration(d, delta_value))
            {
                return ret;
            }
            return null;
        }

        protected bool InitWithDuration(float d, float delta_value)
        {
            if (base.InitWithDuration(d))
            {
                m_delta_value = delta_value;
                return true;
            }
            return false;
        }

        public override void StartWithTarget(Transform target)
        {
            base.StartWithTarget(target);
            if (m_image)
                m_start_value = m_image.fillAmount;
        }

        public FillAmountBy InitSubjectComponent(Image image)
        {
            if(image.type != Image.Type.Filled)
            {
                this.LogError("image type is not Filled");
                return this;
            }
            m_image = image;
            m_start_value = m_image.fillAmount;
            return this;
        }

        public override void Update(float time)
        {
            if (m_image)
            {
                var new_value = m_start_value + m_delta_value * time;
                m_image.fillAmount = new_value;
            }
        }
    }

    public class FillAmountTo : FillAmountBy
    {
        float m_end_value;
        public static new FillAmountTo Create(float d, float end_value)
        {
            var ret = new FillAmountTo();
            if (ret.InitWithDuration(d, end_value))
            {
                ret.m_end_value = end_value;
                return ret;
            }
            return null;
        }

        public override void StartWithTarget(Transform target)
        {
            base.StartWithTarget(target);
            if (m_image)
            {
                m_start_value = m_image.fillAmount;
                m_delta_value = m_end_value - m_image.fillAmount;
            }
        }

        public new FillAmountBy InitSubjectComponent(Image image)
        {
            m_image = image;
            m_start_value = m_image.fillAmount;
            m_delta_value = m_end_value - m_image.fillAmount;
            return this;
        }
    }
}