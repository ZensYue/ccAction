//------------------------------------------------------------------------------
//      Copyright (c) 2021 , ZensYue ZensYue@163.com
//      All rights reserved.
//      Use, modification and distribution are subject to the "MIT License"
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ccAction;

namespace UnityEngine
{
    public static class UnityEngine_RectTransform_CCAction_Extension
    {
        /// <summary>
        /// AnchoredPosition移动
        /// </summary>
        /// <param name="obj">RectTransform</param>
        /// <param name="duration">时间</param>
        /// <param name="to">目标值</param>
        /// <returns></returns>
        public static ActionInterval CCAnchoredPositionTo(this RectTransform obj, float duration, Vector3 to)
        {
            var action = CCActionVector3.Create(duration, obj.anchoredPosition, to);
            action.SetInitFunc(() => { return obj.anchoredPosition; });
            action.SetUpdate((t, result) => {
                obj.anchoredPosition = result;
            });
            return action;
        }

        /// <summary>
        /// 大小
        /// </summary>
        /// <param name="obj">RectTransform</param>
        /// <param name="duration">时间</param>
        /// <param name="to">目标值</param>
        /// <returns></returns>
        public static ActionInterval CCSizeTo(this RectTransform obj, float duration, Vector2 to)
        {
            var action = CCActionVector2.Create(duration, obj.sizeDelta, to);
            action.SetInitFunc(() => { return obj.sizeDelta; });
            action.SetUpdate((t, result) => {
                obj.sizeDelta = result;
            });
            return action;
        }
    }
}