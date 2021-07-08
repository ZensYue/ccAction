//------------------------------------------------------------------------------
//      Copyright (c) 2021 , ZensYue ZensYue@163.com
//      All rights reserved.
//      Use, modification and distribution are subject to the "MIT License"
//------------------------------------------------------------------------------

using ccAction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.UI
{
    public static class UnityEngine_Text_CCAction_Extension
    {
        /// <summary>
        /// 跳数字
        /// </summary>
        /// <param name="text"></param>
        /// <param name="duration"></param>
        /// <param name="format"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static ActionInterval CCNumberTo(this Text text, float duration, string format, float from, float to, string endformat = "")
        {
            var action = CCActionFloat.Create(duration, from, to);
            action.SetUpdate((t, result) => {
                if (t == 1 && !string.IsNullOrEmpty(endformat))
                    text.text = string.Format(endformat, result);
                else
                    text.text = string.Format(format, result);
            });
            return action;
        }

        /// <summary>
        /// 打印机效果
        /// </summary>
        /// <param name="text"></param>
        /// <param name="duration"></param>
        /// <param name="s"></param>
        /// <returns>ActionInterval</returns>
        public static ActionInterval CCTextPrint(this Text text, float duration, string s)
        {
            var action = CCActionFloat.Create(duration, 0, s.Length);
            action.SetUpdate((t, result) => {
                text.text = s.Substring(0, (int)result);
            });
            return action;
        }
    }
}
