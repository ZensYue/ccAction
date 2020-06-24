using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ccAction
{
    public class ColorBy : ActionInterval
    {
        float m_delta_r = 0f;
        float m_delta_g = 0f;
        float m_delta_b = 0f;
        float m_delta_a = 0f;

        protected Color m_colorDelta;
        protected Color m_startColor;
        protected Color m_previousColor;

        Graphic m_graphic; //image text
        Material m_material; //Material Render
        Outline  m_outline;
        CanvasGroup m_canvasGroup;

        public ColorBy InitSubjectComponent(Graphic graphic)
        {
            m_graphic = graphic;
            return this;
        }

        public ColorBy InitSubjectComponent(Button button)
        {
            if (button == null)
                this.LogError("Button is null");
            var image = button.gameObject.GetComponent<Image>();
            if (image == null)
                this.LogError("image is null");
            return InitSubjectComponent(image);
        }

        public ColorBy InitSubjectComponent(Material material)
        {
            m_material = material;
            return this;
        }

        public ColorBy InitSubjectComponent(MeshRenderer meshRenderer)
        {
            if (meshRenderer == null || meshRenderer.material == null)
                this.LogError("MeshRenderer is null");
            return InitSubjectComponent(meshRenderer.material);
        }

        public ColorBy InitSubjectComponent(Outline outline)
        {
            m_outline = outline;
            return this;
        }

        public ColorBy InitSubjectComponent(CanvasGroup canvasGroup)
        {
            m_canvasGroup = canvasGroup;
            return this;
        }

        /// <summary>
        /// 创建RGB渐变
        /// </summary>
        /// <param name="d">时间</param>
        /// <param name="r">0-1</param>
        /// <param name="g">0-1</param>
        /// <param name="b">0-1</param>
        protected static ColorBy Create(float d, float r, float g, float b)
        {
            var ret = new ColorBy();
            if (ret.InitWithDuration(d))
            {
                ret.m_delta_r = r;
                ret.m_delta_g = g;
                ret.m_delta_b = b;
                return ret;
            }
            return null;
        }

        /// <summary>
        /// 创建alpha渐变
        /// </summary>
        /// <param name="d">时间</param>
        /// <param name="a">0-1</param>
        /// <returns></returns>
        protected static ColorBy Create(float d, float a)
        {
            var ret = new ColorBy();
            if (ret.InitWithDuration(d))
            {
                ret.m_delta_a = a;
                return ret;
            }
            return null;
        }

        /// <summary>
        /// 创建RGBA渐变
        /// </summary>
        /// <param name="d">时间</param>
        /// <param name="r">0-1</param>
        /// <param name="g">0-1</param>
        /// <param name="b">0-1</param>
        /// <param name="a">0-1</param>
        /// <returns></returns>
        public static ColorBy Create(float d, float r, float g, float b, float a)
        {
            return Create(d,new Color(r,g,b,a));
        }

        protected static ColorBy Create(float d, Color deltaColor)
        {
            var ret = new ColorBy();
            if (ret.InitWithDuration(d))
            {
                ret.m_delta_r = deltaColor.r;
                ret.m_delta_g = deltaColor.g;
                ret.m_delta_b = deltaColor.b;
                ret.m_delta_a = deltaColor.a;
                return ret;
            }
            return null;
        }

        protected virtual void SetColorDelta(Color color)
        {
            m_startColor = m_previousColor = color;
            m_colorDelta = new Color(m_delta_r, m_delta_g, m_delta_b, m_delta_a);
        }
        public override void StartWithTarget(Transform target)
        {
            base.StartWithTarget(target);
            if(m_graphic == null && m_material == null && m_outline == null && m_canvasGroup == null)
            {
                this.LogError("Color target is null");
            }

            if(m_graphic)
            {
                SetColorDelta(m_graphic.color);
            }
            else if (m_material)
            {
                SetColorDelta(m_material.color);
            }
            else if(m_outline)
            {
                SetColorDelta(m_outline.effectColor);
            }
            else if(m_canvasGroup)
            {
                var color = new Color(0, 0, 0, 0)
                {
                    a = m_canvasGroup.alpha
                };
                SetColorDelta(color);
            }
        }

        public override CCAction Clone()
        {
            return Create(m_duration, m_colorDelta);
        }

        public override CCAction Reverse()
        {
            return null;
        }

        public override void Update(float t)
        {
            //Graphic m_graphic; //image text
            //Material m_material; //Material Render
            //Outline m_outline;
            //CanvasGroup m_canvasGroup;

            Color currentColor;
            if (m_graphic != null)
                currentColor = m_graphic.color;
            else if(m_material != null)
                currentColor = m_material.color;
            else if(m_outline != null)
                currentColor = m_outline.effectColor;
            else if(m_canvasGroup != null)
            {
                currentColor = new Color
                {
                    a = m_canvasGroup.alpha
                };
            }
            else
                currentColor = new Color();

            // CC_ENABLE_STACKABLE_ACTIONS
            var diff = currentColor - m_previousColor;
            m_startColor += diff;
            var newColor = m_startColor + m_colorDelta*t;

            if (m_graphic != null)
                m_graphic.color = newColor;
            else if (m_material != null)
                m_material.color = newColor;
            else if (m_outline != null)
                m_outline.effectColor = newColor;
            else if (m_canvasGroup != null)
                m_canvasGroup.alpha = newColor.a;
            m_previousColor = newColor;
        }
    }

    public class ColorTo:ColorBy
    {
        const float DefalutValue = -100.0101f;
        float m_to_r = DefalutValue;
        float m_to_g = DefalutValue;
        float m_to_b = DefalutValue;
        float m_to_a = DefalutValue;

        /// <summary>
        /// 颜色渐变
        /// </summary>
        /// <param name="d">0-1</param>
        /// <param name="r">0-1</param>
        /// <param name="g">0-1</param>
        /// <param name="b">0-1</param>
        /// <param name="a">0-1</param>
        /// <returns></returns>
        public static new ColorTo Create(float d,float r,float g,float b,float a = DefalutValue)
        {
            var ret = new ColorTo();
            if(ret.InitWithDuration(d))
            {
                ret.m_to_r = r;
                ret.m_to_g = g;
                ret.m_to_b = b;
                ret.m_to_a = a;
                return ret;
            }
            return null;
        }

        protected static new ColorTo Create(float d, float a = DefalutValue)
        {
            var ret = new ColorTo();
            if (ret.InitWithDuration(d))
            {
                ret.m_to_a = a;
                return ret;
            }
            return null;
        }

        protected override void SetColorDelta(Color color)
        {
            m_startColor = m_previousColor = color;
            m_colorDelta = new Color
            {
                r = m_to_r == DefalutValue?0: m_to_r - color.r,
                g = m_to_g == DefalutValue?0: m_to_g - color.g,
                b = m_to_b == DefalutValue?0: m_to_b - color.b,
                a = m_to_a == DefalutValue?0: m_to_a - color.a,
            };
        }
    }

    public class FadeIn: ColorTo
    {
        public static ColorTo Create(float d)
        {
            return ColorTo.Create(d, 1.0f);
        }
    }

    public class FadeOut: ColorTo
    {
        public static ColorTo Create(float d)
        {
            return ColorTo.Create(d, 0f);
        }
    }
}
