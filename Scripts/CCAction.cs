//------------------------------------------------------------------------------
//      Copyright (c) 2021 , ZensYue ZensYue@163.com
//      All rights reserved.
//      Use, modification and distribution are subject to the "MIT License"
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ActionObject = UnityEngine.Object;

namespace ccAction
{
    public static class CCAction
    {
        #region 基础Action gameObject transform RectTransform component
        #region CCShow
        public static ActionInterval CCShow(GameObject obj)
        {
            return obj.CCShow();
        }
        public static ActionInterval CCShow(Transform obj)
        {
            return obj.gameObject.CCShow();
        }
        public static ActionInterval CCShow(Component obj)
        {
            return obj.gameObject.CCShow();
        }
        #endregion

        #region CCHide
        public static ActionInterval CCHide(GameObject obj)
        {
            return obj.CCHide();
        }
        public static ActionInterval CCHide(Transform obj)
        {
            return obj.gameObject.CCHide();
        }
        public static ActionInterval CCHide(Component obj)
        {
            return obj.gameObject.CCHide();
        }
        #endregion

        #region CCRemoveSelf
        public static ActionInterval CCRemoveSelf(GameObject obj,float t = 0)
        {
            return obj.CCRemoveSelf(t);
        }
        public static ActionInterval CCRemoveSelf(Transform obj, float t = 0)
        {
            return obj.gameObject.CCRemoveSelf(t);
        }
        public static ActionInterval CCRemoveSelf(Component obj, float t = 0)
        {
            return obj.gameObject.CCRemoveSelf(t);
        }
        #endregion

        #region CCRemoveSelf
        public static ActionInterval CCBlink(GameObject obj, float duration, uint time)
        {
            return obj.CCBlink(duration,time);
        }
        public static ActionInterval CCBlink(Transform obj, float duration, uint time)
        {
            return obj.gameObject.CCBlink(duration, time);
        }
        public static ActionInterval CCBlink(Component obj, float duration, uint time)
        {
            return obj.gameObject.CCBlink(duration, time);
        }
        #endregion

        #region CCScaleTo
        public static ActionInterval CCScaleTo(GameObject obj, float duration, Vector3 to)
        {
            return obj.transform.CCScaleTo(duration, to);
        }
        public static ActionInterval CCScaleTo(Transform obj, float duration, Vector3 to)
        {
            return obj.CCScaleTo(duration, to);
        }
        public static ActionInterval CCScaleTo(Component obj, float duration, Vector3 to)
        {
            return obj.transform.CCScaleTo(duration, to);
        }
        #endregion

        #region CCMoveTo
        public static ActionInterval CCMoveTo(GameObject obj, float duration, Vector3 to, bool relativeWorld = false)
        {
            return obj.transform.CCMoveTo(duration, to, relativeWorld);
        }
        public static ActionInterval CCMoveTo(Transform obj, float duration, Vector3 to, bool relativeWorld = false)
        {
            return obj.CCMoveTo(duration, to, relativeWorld);
        }
        public static ActionInterval CCMoveTo(Component obj, float duration, Vector3 to, bool relativeWorld = false)
        {
            return obj.transform.CCMoveTo(duration, to, relativeWorld);
        }
        #endregion

        #region CCAnglesTo
        public static ActionInterval CCAnglesTo(GameObject obj, float duration, Vector3 to, bool relativeWorld = false)
        {
            return obj.transform.CCAnglesTo(duration, to, relativeWorld);
        }
        public static ActionInterval CCAnglesTo(Transform obj, float duration, Vector3 to, bool relativeWorld = false)
        {
            return obj.CCAnglesTo(duration, to, relativeWorld);
        }
        public static ActionInterval CCAnglesTo(Component obj, float duration, Vector3 to, bool relativeWorld = false)
        {
            return obj.transform.CCAnglesTo(duration, to, relativeWorld);
        }
        #endregion

        #region CCRotationTo
        public static ActionInterval CCRotationTo(GameObject obj, float duration, Quaternion to)
        {
            return obj.transform.CCRotationTo(duration, to);
        }
        public static ActionInterval CCRotationTo(Transform obj, float duration, Quaternion to)
        {
            return obj.CCRotationTo(duration, to);
        }
        public static ActionInterval CCRotationTo(Component obj, float duration, Quaternion to)
        {
            return obj.transform.CCRotationTo(duration, to);
        }
        #endregion

        #region CCQuadBezierTo 二阶贝塞尔
        public static ActionInterval CCQuadBezierTo(GameObject obj, float duration, Vector3 c, Vector3 p2, bool relativeWorld = false)
        {
            return obj.transform.CCQuadBezierTo(duration, c, p2, relativeWorld);
        }
        public static ActionInterval CCQuadBezierTo(Transform obj, float duration, Vector3 c, Vector3 p2, bool relativeWorld = false)
        {
            return obj.CCQuadBezierTo(duration,c,p2, relativeWorld);
        }
        public static ActionInterval CCQuadBezierTo(Component obj, float duration, Vector3 c, Vector3 p2, bool relativeWorld = false)
        {
            return obj.transform.CCQuadBezierTo(duration, c, p2, relativeWorld);
        }
        #endregion

        #region CCCubicBezierTo 三阶贝塞尔
        public static ActionInterval CCCubicBezierTo(GameObject obj, float duration, Vector3 c1,Vector3 c2, Vector3 p2, bool relativeWorld = false)
        {
            return obj.transform.CCCubicBezierTo(duration, c1,c2, p2, relativeWorld);
        }
        public static ActionInterval CCCubicBezierTo(Transform obj, float duration, Vector3 c1, Vector3 c2, Vector3 p2, bool relativeWorld = false)
        {
            return obj.CCCubicBezierTo(duration, c1,c2, p2, relativeWorld);
        }
        public static ActionInterval CCCubicBezierTo(Component obj, float duration, Vector3 c1, Vector3 c2, Vector3 p2, bool relativeWorld = false)
        {
            return obj.transform.CCCubicBezierTo(duration, c1,c2, p2, relativeWorld);
        }
        #endregion

        #region CCCatmullRoomTo 样条曲线
        public static ActionInterval CCCatmullRoomTo(GameObject obj, float duration, Vector3 c1, Vector3 c2, Vector3 p2, bool relativeWorld = false)
        {
            return obj.transform.CCCatmullRoomTo(duration, c1, c2, p2, relativeWorld);
        }
        public static ActionInterval CCCatmullRoomTo(Transform obj, float duration, Vector3 c1, Vector3 c2, Vector3 p2, bool relativeWorld = false)
        {
            return obj.CCCatmullRoomTo(duration, c1, c2, p2, relativeWorld);
        }
        public static ActionInterval CCCatmullRoomTo(Component obj, float duration, Vector3 c1, Vector3 c2, Vector3 p2, bool relativeWorld = false)
        {
            return obj.transform.CCCatmullRoomTo(duration, c1, c2, p2, relativeWorld);
        }
        #endregion

        #region CCShake 震动
        public static ActionInterval CCShake(GameObject obj, float duration, Vector3 magnitude, bool relativeWorld = false)
        {
            return obj.transform.CCShake(duration, magnitude, relativeWorld);
        }
        public static ActionInterval CCShake(Transform obj, float duration, Vector3 magnitude, bool relativeWorld = false)
        {
            return obj.CCMoveTo(duration, magnitude, relativeWorld);
        }
        public static ActionInterval CCShake(Component obj, float duration, Vector3 magnitude, bool relativeWorld = false)
        {
            return obj.transform.CCShake(duration, magnitude, relativeWorld);
        }
        #endregion

        #region CCEllipse 椭圆运动
        public static ActionInterval CCEllipse(GameObject obj, float duration, Vector3 centerPos, Vector3 vecA, Vector3 vecB, float a, float b, float startp = 0, float endp = 1, bool relativeWorld = false)
        {
            return obj.transform.CCEllipse(duration, centerPos, vecA, vecB,a,b,startp,endp, relativeWorld);
        }
        public static ActionInterval CCEllipse(Transform obj, float duration, Vector3 centerPos, Vector3 vecA, Vector3 vecB, float a, float b, float startp = 0, float endp = 1, bool relativeWorld = false)
        {
            return obj.CCEllipse(duration, centerPos, vecA, vecB, a, b, startp, endp, relativeWorld);
        }
        public static ActionInterval CCEllipse(Component obj, float duration, Vector3 centerPos, Vector3 vecA, Vector3 vecB, float a, float b, float startp = 0, float endp = 1, bool relativeWorld = false)
        {
            return obj.transform.CCEllipse(duration, centerPos, vecA, vecB, a, b, startp, endp, relativeWorld);
        }
        #endregion

        #region CCEllipse 椭圆运动2D
        public static ActionInterval CCEllipse2D(GameObject obj, float duration, Vector3 centerPos, float a, float b, float startp = 0, float endp = 1, bool relativeWorld = false)
        {
            return obj.transform.CCEllipse2D(duration, centerPos, a, b, startp, endp, relativeWorld);
        }
        public static ActionInterval CCEllipse2D(Transform obj, float duration, Vector3 centerPos, float a, float b, float startp = 0, float endp = 1, bool relativeWorld = false)
        {
            return obj.CCEllipse2D(duration, centerPos, a, b, startp, endp, relativeWorld);
        }
        public static ActionInterval CCEllipse2D(Component obj, float duration, Vector3 centerPos, float a, float b, float startp = 0, float endp = 1, bool relativeWorld = false)
        {
            return obj.transform.CCEllipse2D(duration, centerPos, a, b, startp, endp, relativeWorld);
        }
        #endregion

        #region CCAnchoredPositionTo RectTransform Graphic
        public static ActionInterval CCAnchoredPositionTo(RectTransform obj, float duration, Vector3 to)
        {
            return obj.CCAnchoredPositionTo(duration, to);
        }
        public static ActionInterval CCAnchoredPositionTo(Graphic obj, float duration, Vector3 to)
        {
            return obj.rectTransform.CCAnchoredPositionTo(duration, to);
        }
        #endregion

        #region CCSizeTo RectTransform Graphic
        public static ActionInterval CCSizeTo(RectTransform obj, float duration, Vector3 to)
        {
            return obj.CCSizeTo(duration, to);
        }
        public static ActionInterval CCSizeTo(Graphic obj, float duration, Vector3 to)
        {
            return obj.rectTransform.CCScaleTo(duration, to);
        }
        #endregion

        #region CCColorTo Graphic
        public static ActionInterval CCColorTo(Graphic obj, float duration, Color to)
        {
            return obj.CCColorTo(duration, to);
        }
        #endregion

        #region CCFadeTo Graphic CanvasGroup
        public static ActionInterval CCFadeTo(Graphic obj, float duration, float to)
        {
            return obj.CCFadeTo(duration, to);
        }
        public static ActionInterval CCFadeTo(CanvasGroup obj, float duration, float to)
        {
            return obj.CCFadeTo(duration, to);
        }
        #endregion

        #region CCFadeIn(alpha 1) 淡入 Graphic CanvasGroup
        public static ActionInterval CCFadeIn(Graphic obj, float duration)
        {
            return obj.CCFadeIn(duration);
        }
        public static ActionInterval CCFadeIn(CanvasGroup obj, float duration)
        {
            return obj.CCFadeIn(duration);
        }
        #endregion

        #region CCFadeOut(alpha 0) 淡出 Graphic CanvasGroup
        public static ActionInterval CCFadeOut(Graphic obj, float duration)
        {
            return obj.CCFadeOut(duration);
        }
        public static ActionInterval CCFadeOut(CanvasGroup obj, float duration)
        {
            return obj.CCFadeOut(duration);
        }
        #endregion

        #region CCFillAmout 进度条 Image
        public static ActionInterval CCFillAmout(Image obj, float duration, float to = 1)
        {
            return obj.CCFillAmout(duration, to);
        }
        #endregion

        #region CCNumberTo 数字跳动 Text
        public static ActionInterval CCNumberTo(Text obj, float duration, string format, float from, float to, string endformat = "")
        {
            return obj.CCNumberTo(duration, format,from,to,endformat);
        }
        #endregion

        #region CCTextPrint 打印机 Text
        public static ActionInterval CCTextPrint(Text obj, float duration, string s)
        {
            return obj.CCTextPrint(duration, s);
        }
        #endregion

        #endregion

        #region 辅助Action 回调 延迟 延迟回调
        public static ActionInterval CCCall(System.Action func)
        {
            return CCActionExecute.Create(func);
        }
        public static ActionInterval CCDelay(float duration)
        {
            return CCActionDelay.Create(duration);
        }
        public static ActionInterval CCDelay(float duration, System.Action func)
        {
            return CCSequence(CCDelay(duration), CCCall(func));
        }
        #endregion

        #region 容器Action 顺序 并行 重复 无限重复
        public static ActionInterval CCSequence(ActionInterval action1, params ActionInterval[] args)
        {
            return CCActionSequence.Create(action1, args);
        }
        public static ActionInterval CCSpawn(ActionInterval action1, params ActionInterval[] args)
        {
            return CCActionSpawn.Create(action1, args);
        }
        public static ActionInterval CCRepeat(ActionInterval action, uint time)
        {
            return CCActionRepeat.Create(action, time);
        }
        public static ActionInterval CCRepeatForever(ActionInterval action)
        {
            return CCActionRepeatForever.Create(action);
        }
        #endregion

        #region 变速
        #region Sine
        public static ActionInterval CCSineEaseIn(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.SineEaseIn);
        }
        public static ActionInterval CCSineEaseOut(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.SineEaseOut);
        }
        public static ActionInterval CCSineEaseInOut(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.SineEaseInOut);
        }
        #endregion

        #region Quad
        public static ActionInterval CCQuadEaseIn(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.QuadEaseIn);
        }
        public static ActionInterval CCQuadEaseOut(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.QuadEaseOut);
        }
        public static ActionInterval CCQuadEaseInOut(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.QuadEaseInOut);
        }
        #endregion

        #region Cubic
        public static ActionInterval CCCubicEaseIn(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.CubicEaseIn);
        }
        public static ActionInterval CCCubicEaseOut(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.CubicEaseOut);
        }
        public static ActionInterval CCCubicEaseInOut(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.CubicEaseInOut);
        }
        #endregion

        #region Quart
        public static ActionInterval CCQuartEaseIn(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.QuartEaseIn);
        }
        public static ActionInterval CCQuartEaseOut(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.QuartEaseOut);
        }
        public static ActionInterval CCQuartEaseInOut(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.QuartEaseInOut);
        }
        #endregion

        #region Quint
        public static ActionInterval CCQuintEaseIn(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.QuintEaseIn);
        }
        public static ActionInterval CCQuintEaseOut(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.QuintEaseOut);
        }
        public static ActionInterval CCQuintEaseInOut(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.QuintEaseInOut);
        }
        #endregion

        #region Expo
        public static ActionInterval CCExpoEaseIn(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.ExpoEaseIn);
        }
        public static ActionInterval CCExpoEaseOut(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.ExpoEaseOut);
        }
        public static ActionInterval CCExpoEaseInOut(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.ExpoEaseInOut);
        }
        #endregion

        #region Circ
        public static ActionInterval CCCircEaseIn(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.CircEaseIn);
        }
        public static ActionInterval CCCircEaseOut(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.CircEaseOut);
        }
        public static ActionInterval CCCircEaseInOut(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.CircEaseInOut);
        }
        #endregion

        #region Back
        public static ActionInterval CCBackEaseIn(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.BackEaseIn);
        }
        public static ActionInterval CCBackEaseOut(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.BackEaseOut);
        }
        public static ActionInterval CCBackEaseInOut(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.BackEaseInOut);
        }
        #endregion

        #region Bounce
        public static ActionInterval CCBounceEaseIn(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.BounceEaseIn);
        }
        public static ActionInterval CCBounceEaseOut(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.BounceEaseOut);
        }
        public static ActionInterval CCBounceEaseInOut(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.BounceEaseInOut);
        }
        #endregion

        #region Quadratic
        public static ActionInterval CCQuadraticIn(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.QuadraticIn);
        }
        public static ActionInterval CCQuadraticOut(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.QuadraticOut);
        }
        public static ActionInterval CCQuadraticInOut(ActionInterval action)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.QuadraticInOut);
        }
        #endregion

        #region Elastic rate
        public static ActionInterval CCElasticEaseIn(ActionInterval action,float rate)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.ElasticEaseIn,rate);
        }
        public static ActionInterval CCElasticEaseOut(ActionInterval action,float rate)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.ElasticEaseOut,rate);
        }
        public static ActionInterval CCElasticEaseInOut(ActionInterval action,float rate)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.ElasticEaseInOut,rate);
        }
        #endregion

        #region Ease rate
        public static ActionInterval CCEaseIn(ActionInterval action,float rate)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.EaseIn,rate);
        }
        public static ActionInterval CCEaseOut(ActionInterval action,float rate)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.EaseOut,rate);
        }
        public static ActionInterval CCEaseInOut(ActionInterval action,float rate)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.EaseInOut,rate);
        }
        #endregion

        #region CustomEase a,b,c,d
        public static ActionInterval CCCustomEase(ActionInterval action,float a,float b,float c,float d)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.CustomEase,a,b,c,d);
        }
        #endregion

        #region CustomEase a,b,c,d
        public static ActionInterval CCBezieratEase(ActionInterval action, float a, float b, float c, float d)
        {
            return CCActionEase.Create(action).SetEase(CCEaseFunc.BezieratEase, a, b, c, d);
        }
        #endregion
        #endregion

        #region 添加移除
        public static int Do(ActionInterval action, ActionObject obj)
        {
            return CCActionManager.Instance.AddAction(action, obj);
        }
        /// <summary>
        /// 执行Action
        /// </summary>
        /// <param name="action">action</param>
        /// <param name="obj">对象主体，可用于安全删除</param>
        /// <param name="repeatTime">重复次数，0或1不重复，小于0无限重复，大于1按照次数重复</param>
        /// <returns></returns>
        public static int Do(ActionInterval action, ActionObject obj,int repeatTime)
        {
            if(repeatTime == 0 || repeatTime == 1)
            {
                return Do(action, obj);
            }
            else if(repeatTime < 0)
            {
                return Do(CCRepeatForever(action), obj);
            }
            else
            {
                return Do(CCRepeat(action, (uint)repeatTime), obj);
            }
        }

        /// <summary>
        /// 移除单个Action
        /// </summary>
        /// <param name="tag">IAction.Flags</param>
        public static void KillAction(int tag)
        {
            CCActionManager.Instance.RemoveAction(tag);
        }
        /// <summary>
        /// 移除单个Action
        /// </summary>
        /// <param name="action">action</param>
        public static void KillAction(ActionInterval action)
        {
            CCActionManager.Instance.RemoveAction(action);
        }

        /// <summary>
        /// 移除对象全部Action
        /// </summary>
        /// <param name="target">对象</param>
        public static void Kill(ActionObject target)
        {
            CCActionManager.Instance.RemoveAllActionsFromTarget(target);
        }
        /// <summary>
        /// 移除组（对象）全部Action
        /// </summary>
        /// <param name="groupId">组ID</param>
        public static void Kill(int groupId)
        {
            CCActionManager.Instance.RemoveAllActionsFromTarget(groupId);
        }
        #endregion

        #region 轮询调用
        public static void Update(float dt)
        {
            CCActionManager.Instance.Update(dt);
        }
        #endregion
    }
}
