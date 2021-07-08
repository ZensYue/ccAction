//------------------------------------------------------------------------------
//      Copyright (c) 2021 , ZensYue ZensYue@163.com
//      All rights reserved.
//      Use, modification and distribution are subject to the "MIT License"
//------------------------------------------------------------------------------

using ccAction;

namespace UnityEngine.UI
{
    /// <summary>
    /// 
    /// </summary>
    public static class UnityEngine_Graphic_CCAction_Extension
    {
        

        /// <summary>
        /// 变色
        /// </summary>
        /// <param name="graphic"></param>
        /// <param name="duration">时间</param>
        /// <param name="to">目标</param>
        /// <returns>ActionInterval</returns>
        public static ActionInterval CCColorTo(this Graphic graphic, float duration,Color to)
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
        public static ActionInterval CCFadeTo(this Graphic graphic, float duration, float to = 1)
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
        public static ActionInterval CCFadeIn(this Graphic graphic, float duration)
        {
            return graphic.CCFadeTo(duration, 1);
        }

        /// <summary>
        /// 淡出
        /// </summary>
        /// <param name="graphic"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static ActionInterval CCFadeOut(this Graphic graphic, float duration)
        {
            return graphic.CCFadeTo(duration, 0);
        }
    }
}
