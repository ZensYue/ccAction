using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ccAction;

namespace UnityEngine
{
    public static class UnityEngine_Transform_ccAction_Extension
    {
        /// <summary>
        /// 缩放
        /// </summary>
        /// <param name="obj">Transform</param>
        /// <param name="duration">时间</param>
        /// <param name="to">目标值</param>
        /// <returns></returns>
        public static IFiniteTimeAction CCScaleTo(this Transform obj, float duration, Vector3 to)
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
        public static IFiniteTimeAction CCMoveTo(this Transform obj, float duration, Vector3 to,bool relativeWorld = false)
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
        public static IFiniteTimeAction CCAnglesTo(this Transform obj, float duration, Vector3 to, bool relativeWorld = false)
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
        public static IFiniteTimeAction CCRotationTo(this Transform obj, float duration, Quaternion to, bool relativeWorld = false)
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
        /// <param name="obj">Transform</param>
        /// <param name="duration">时间</param>
        /// <param name="to">目标值</param>
        /// <returns></returns>
        public static IFiniteTimeAction CCQuadBezierTo(this Transform obj, float duration, Vector3 c, Vector3 p2, bool relativeWorld = false)
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
        public static IFiniteTimeAction CCCubicBezierTo(this Transform obj, float duration, Vector3 c1, Vector3 c2, Vector3 p2, bool relativeWorld = false)
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
        public static IFiniteTimeAction CCCatmullRoomTo(this Transform obj, float duration, Vector3 c1, Vector3 c2, Vector3 p2, bool relativeWorld = false)
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

        public static IFiniteTimeAction CCEllipse(this Transform obj, float duration, Vector3 centerPos, Vector3 vecA, Vector3 vecB, float a, float b,float startp=0,float endp = 1, bool relativeWorld = false)
        {
            Vector3 p1 = relativeWorld ? obj.position : obj.localPosition;
            var action = CCActionFloat.Create(duration, startp, endp);
            action.SetUpdate((t, result) => {
                var pos = CCActionMath.Ellipse(centerPos,vecA,vecB,a,b, result);
                if (relativeWorld) obj.position = pos;
                else obj.localPosition = pos;
            });
            return action;
        }
    }
}