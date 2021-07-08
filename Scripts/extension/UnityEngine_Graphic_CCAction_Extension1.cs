
using ccAction;

namespace UnityEngine.UI
{
    /// <summary>
    /// 
    /// </summary>
    public static class UnityEngine_Graphic_CCAction_Extension
    {
        /// <summary>
        /// image fillAmount
        /// </summary>
        /// <param name="img">图片</param>
        /// <param name="duration">时间</param>
        /// <param name="startp">起始fillAmount值</param>
        /// <param name="to">结束fillAmount值</param>
        /// <returns>IFiniteTimeAction</returns>
        public static IFiniteTimeAction CCFillAmout(this Image img, float duration,float to = 1)
        {
#if UNITY_EDITOR
            if(img.type != Image.Type.Filled)
            {
                CCLog.LogError("image type is not Filled");
            }
#endif
            to = Mathf.Clamp01(to);
            var action = CCActionFloat.Create(duration, img.fillAmount, to);
            action.SetInitFunc(()=>{ return img.fillAmount; });
            action.SetUpdate((t, result) => {
                img.fillAmount = result;
            });
            return action;
        }

        /// <summary>
        /// 变色
        /// </summary>
        /// <param name="graphic"></param>
        /// <param name="duration">时间</param>
        /// <param name="to">目标</param>
        /// <returns>IFiniteTimeAction</returns>
        public static IFiniteTimeAction CCColorTo(this Graphic graphic, float duration,Color to)
        {
            var action = CCActionColor.Create(duration, graphic.color, to);
            action.SetInitFunc(()=>{ return graphic.color; });
            action.SetUpdate((t, result) => {
                graphic.color = result;
            });
            return action;
        }

        /// <summary>
        /// 渐变
        /// </summary>
        /// <param name="graphic"></param>
        /// <param name="duration"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static IFiniteTimeAction CCFadeTo(this Graphic graphic, float duration, float to = 1)
        {
            to = Mathf.Clamp01(to);
            return graphic.CCColorTo(duration, new Color(graphic.color.r, graphic.color.g, graphic.color.b, to));
        }

        /// <summary>
        /// 淡入
        /// </summary>
        /// <param name="graphic"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static IFiniteTimeAction CCFadeIn(this Graphic graphic, float duration)
        {
            return graphic.CCFadeTo(duration, 0);
        }

        /// <summary>
        /// 淡出
        /// </summary>
        /// <param name="graphic"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static IFiniteTimeAction CCFadeOut(this Graphic graphic, float duration)
        {
            return graphic.CCFadeTo(duration, 1);
        }

        /// <summary>
        /// 跳数字
        /// </summary>
        /// <param name="text"></param>
        /// <param name="duration"></param>
        /// <param name="format"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static IFiniteTimeAction CCNumberTo(this Text text, float duration,string format,float from,float to,string endformat = "")
        {
            var action = CCActionFloat.Create(duration, from, to);
            action.SetUpdate((t, result) => {
                if(t == 1 && !string.IsNullOrEmpty(endformat))
                    text.text = string.Format(endformat, result);
                else
                    text.text = string.Format(format,result);
            });
            return action;
        }

        /// <summary>
        /// 打印机效果
        /// </summary>
        /// <param name="text"></param>
        /// <param name="duration"></param>
        /// <param name="s"></param>
        /// <returns>IFiniteTimeAction</returns>
        public static IFiniteTimeAction CCTextPrint(this Text text, float duration, string s)
        {
            var action = CCActionFloat.Create(duration, 0, s.Length);
            action.SetUpdate((t, result) => {
                text.text = s.Substring(0, (int)result);
            });
            return action;
        }
    }
}
