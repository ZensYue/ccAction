//------------------------------------------------------------------------------
//      Copyright (c) 2021 , ZensYue ZensYue@163.com
//      All rights reserved.
//      Use, modification and distribution are subject to the "MIT License"
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ccAction;

namespace UnityEngine
{
    public static class UnityEngine_GameObject_CCAction_Extension
    {
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="obj">GameObject</param>
        /// <returns></returns>
        public static ActionInterval CCShow(this GameObject obj)
        {
            var action = CCActionExecute.Create(() => { obj.SetActive(true); });
            return action;
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        /// <param name="obj">GameObject</param>
        /// <returns></returns>
        public static ActionInterval CCHide(this GameObject obj)
        {
            var action = CCActionExecute.Create(() => { obj.SetActive(false); });
            return action;
        }

        /// <summary>
        /// 删除自己
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static ActionInterval CCRemoveSelf(this GameObject obj, float t=0)
        {
            var action = CCActionExecute.Create(() => { GameObject.Destroy(obj, t); });
            return action;
        }

        /// <summary>
        /// 闪烁
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="duration">时间</param>
        /// <param name="time">闪烁次数</param>
        /// <returns></returns>
        public static ActionInterval CCBlink(this GameObject obj,float duration,uint time)
        {
            float oneTime = duration / time;
            var action = CCActionSequence.Create(
                obj.CCHide(),
                CCActionDelay.Create(oneTime*0.5f),
                obj.CCShow(),
                CCActionDelay.Create(oneTime*0.5f)
            );
            var repectAction = CCActionRepeat.Create(action, time);
            return repectAction;
        }
    }
}
