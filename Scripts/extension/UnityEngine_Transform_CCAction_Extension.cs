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
    public static class UnityEngine_Transform_CCAction_Extension
    {
        /// <summary>
        /// 缩放
        /// </summary>
        /// <param name="obj">Transform</param>
        /// <param name="duration">时间</param>
        /// <param name="to">目标值</param>
        /// <returns></returns>
        public static ActionInterval CCScaleTo(this Transform obj, float duration, Vector3 to)
        {
            var action = CCActionVector3.Create(duration, obj.localScale, to);
            action.SetInitFunc(() => { return obj.localScale; });
            action.SetUpdate((t,result) => { obj.localScale = result; });
            return action;
        }

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="obj">Transform</param>
        /// <param name="duration">时间</param>
        /// <param name="to">目标值</param>
        /// <returns></returns>
        public static ActionInterval CCMoveTo(this Transform obj, float duration, Vector3 to,bool relativeWorld = false)
        {
            var action = CCActionVector3.Create(duration, relativeWorld ? obj.position:obj.localPosition, to);
            action.SetInitFunc(() => { return relativeWorld ? obj.position : obj.localPosition; });
            action.SetUpdate((t, result) => {
                if (relativeWorld) obj.position = result;
                else obj.localPosition = result;
            });
            return action;
        }

        /// <summary>
        /// 旋转
        /// </summary>
        /// <param name="obj">Transform</param>
        /// <param name="duration">时间</param>
        /// <param name="to">目标值</param>
        /// <returns></returns>
        public static ActionInterval CCAnglesTo(this Transform obj, float duration, Vector3 to, bool relativeWorld = false)
        {
            var action = CCActionVector3.Create(duration, relativeWorld ? obj.eulerAngles : obj.localEulerAngles, to);
            action.SetInitFunc(() => { return relativeWorld ? obj.eulerAngles : obj.localEulerAngles; });
            action.SetUpdate((t, result) => {
                if (relativeWorld) obj.eulerAngles = result;
                else obj.localEulerAngles = result;
            });
            return action;
        }

        /// <summary>
        /// Rotation
        /// </summary>
        /// <param name="obj">Transform</param>
        /// <param name="duration">时间</param>
        /// <param name="to">目标值</param>
        /// <returns></returns>
        public static ActionInterval CCRotationTo(this Transform obj, float duration, Quaternion to, bool relativeWorld = false)
        {
            var action = CCActionQuaternion.Create(duration, relativeWorld ? obj.rotation : obj.localRotation, to);
            action.SetInitFunc(() => { return relativeWorld ? obj.rotation : obj.localRotation; });
            action.SetUpdate((t, result) => {
                if (relativeWorld) obj.rotation = result;
                else obj.localRotation = result;
            });
            return action;
        }


        /// <summary>
        /// 二阶贝塞尔曲线运动
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="duration">时间</param>
        /// <param name="c">控制点</param>
        /// <param name="p2">目标点</param>
        /// <param name="relativeWorld">是否是直接坐标</param>
        /// <returns>ActionInterval</returns>
        public static ActionInterval CCQuadBezierTo(this Transform obj, float duration, Vector3 c, Vector3 p2, bool relativeWorld = false)
        {
            Vector3 p1 = relativeWorld ? obj.position : obj.localPosition;
            var action = CCActionFloat.Create(duration, 0, 1);
            action.SetUpdate((t, result) => {
                var pos = CCActionMath.QuadBezier(p1, c, p2, result);
                if (relativeWorld) obj.position = pos;
                else obj.localPosition = pos;
            });
            return action;
        }

        /// <summary>
        /// 三阶贝塞尔曲线运动
        /// </summary>
        /// <param name="obj">Transform</param>
        /// <param name="duration">时间</param>
        /// <param name="to">目标值</param>
        /// <returns></returns>
        public static ActionInterval CCCubicBezierTo(this Transform obj, float duration, Vector3 c1, Vector3 c2, Vector3 p2, bool relativeWorld = false)
        {
            Vector3 p1 = relativeWorld ? obj.position : obj.localPosition;
            var action = CCActionFloat.Create(duration, 0, 1);
            action.SetUpdate((t, result) => {
                var pos = CCActionMath.CubicBezier(p1, c1, c2, p2, result);
                if (relativeWorld) obj.position = pos;
                else obj.localPosition = pos;
            });
            return action;
        }

        /// <summary>
        /// 样条曲线
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="duration">时间</param>
        /// <param name="c1">第一个控制点</param>
        /// <param name="c2">第二个控制点</param>
        /// <param name="p2">目标</param>
        /// <param name="relativeWorld">是否是世界坐标</param>
        /// <returns></returns>
        public static ActionInterval CCCatmullRoomTo(this Transform obj, float duration, Vector3 c1, Vector3 c2, Vector3 p2, bool relativeWorld = false)
        {
            Vector3 p1 = relativeWorld ? obj.position : obj.localPosition;
            var action = CCActionFloat.Create(duration, 0, 1);
            action.SetUpdate((t, result) => {
                var pos = CCActionMath.CatmullRoom(p1, c1, c2, p2, result);
                if (relativeWorld) obj.position = pos;
                else obj.localPosition = pos;
            });
            return action;
        }

        /// <summary>
        /// 震动
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="duration">时间</param>
        /// <param name="magnitude">震动系数</param>
        /// <param name="relativeWorld">是否是直接坐标</param>
        /// <returns>ActionInterval</returns>
        public static ActionInterval CCShake(this Transform obj, float duration, Vector3 magnitude, bool relativeWorld = false)
        {
            Vector3 curpos = relativeWorld ? obj.position : obj.localPosition;
            var action = CCActionFloat.Create(duration, 0, 1);
            action.SetInitFunc(() => {
                return 0;
            });
            action.SetUpdate((t, result) => {
                var pos = CCActionMath.Shake(magnitude, curpos, result);
                if (relativeWorld) obj.position = pos;
                else obj.localPosition = pos;
            });
            return action;
        }

        /// <summary>
		/// 椭圆运动 
		/// VecA与VecB垂直
		/// a和b相等，2D就是一个圆，3D就是一个球
		/// </summary>
		/// <param name="centerPos">中心点</param>
		/// <param name="vecA">向量A，如果是2D，可以当做x或者y轴，与向量B垂直</param>
		/// <param name="VecB">向量B，如果是2D，可以当做x或者y轴，与向量A垂直</param>
		/// <param name="a">向量A长度</param>
		/// <param name="b">向量B长度</param>
		/// <param name="t"></param>
		/// <returns>ActionInterval</returns>
        public static ActionInterval CCEllipse(this Transform obj, float duration, Vector3 centerPos, Vector3 vecA, Vector3 vecB, float a, float b,float startp=0,float endp = 1, bool relativeWorld = false)
        {
#if UNITY_EDITOR
            if(vecA == Vector3.zero) 
                CCLog.LogError("vecA is zero");
            if(vecB == Vector3.zero) 
                CCLog.LogError("vecB is zero");

            if ((vecA.x * vecB.x + vecA.y * vecB.y + vecA.z * vecB.z) != 0)
            {
                CCLog.LogError("vecA X vecA != 0");
            }
#endif
            vecA.Normalize();
            vecB.Normalize();

            Vector3 p1 = relativeWorld ? obj.position : obj.localPosition;
            var action = CCActionFloat.Create(duration, startp, endp);
            action.SetUpdate((t, result) => {
                var pos = CCActionMath.Ellipse(centerPos,vecA,vecB,a,b, result);
                if (relativeWorld) obj.position = pos;
                else obj.localPosition = pos;
            });
            return action;
        }

        /// <summary>
		/// 椭圆运动 2D
		/// VecA与VecB垂直
		/// a和b相等，2D就是一个圆，3D就是一个球
		/// </summary>
		/// <param name="centerPos">中心点</param>
		/// <param name="a">向量A长度 x轴</param>
		/// <param name="b">向量B长度 y轴</param>
		/// <param name="t"></param>
		/// <returns>ActionInterval</returns>
        public static ActionInterval CCEllipse2D(this Transform obj, float duration, Vector2 centerPos, float a, float b, float startp = 0, float endp = 1, bool relativeWorld = false)
        {
            return obj.CCEllipse(duration, centerPos, new Vector3(1,0,0), new Vector3(0,1,0), a, b, startp, endp, relativeWorld);
        }
    }
}