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
    public static class UnityEngine_CanvasGroup_CCAction_Extension
    {
        /// <summary>
        /// 渐变
        /// </summary>
        /// <param name="graphic"></param>
        /// <param name="duration"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static ActionInterval CCFadeTo(this CanvasGroup canvasGroup, float duration, float to = 1)
        {
            to = Mathf.Clamp01(to);
            var action = CCActionFloat.Create(duration, canvasGroup.alpha, to);
            action.SetInitFunc(() => { return canvasGroup.alpha; });
            action.SetUpdate((t, result) => {
                canvasGroup.alpha = result;
            });
            return action;
        }

        /// <summary>
        /// 淡入
        /// </summary>
        /// <param name="graphic"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static ActionInterval CCFadeIn(this CanvasGroup canvasGroup, float duration)
        {
            return canvasGroup.CCFadeTo(duration, 1);
        }

        /// <summary>
        /// 淡出
        /// </summary>
        /// <param name="graphic"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static ActionInterval CCFadeOut(this CanvasGroup canvasGroup, float duration)
        {
            return canvasGroup.CCFadeTo(duration, 0);
        }
    }
}
