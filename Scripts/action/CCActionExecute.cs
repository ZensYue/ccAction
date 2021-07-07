using UnityEngine;
using System;

namespace ccAction
{
    /// <summary>
    /// 执行Action
    /// </summary>
    public class CCActionExecute : IFiniteTimeAction
    {
        private bool m_done;

        private Action m_action;

        public static CCActionExecute Create(Action action)
        {
            var ret = new CCActionExecute(action);
            return ret;
        }

        public CCActionExecute(Action action)
        {
            m_action = action;
        }

        public override bool IsDone()
        {
            return m_done;
        }

        public override void StartWithTarget()
        {
            base.StartWithTarget();
            m_done = false;
        }

        public override void Step(float dt)
        {
            Update(1);
        }

        public override void Update(float time)
        {
            m_action?.Invoke();
            m_done = true;
        }
    }


    /*
    #region Show SetActive(true)
    public class Show: ActionExecute
    {
        Component m_component;
        public static Show Create()
        {
            var show = new Show();
            return show;
        }

        public override void Update(float time)
        {
            m_target?.gameObject.SetActive(true);
        }

        public override IAction Reverse()
        {
            return Hide.Create();
        }

        public override IAction Clone()
        {
            return Create();
        }
    }
    #endregion

    #region Hide SetActive(false)
    public class Hide : ActionExecute
    {
        public static Hide Create()
        {
            var hide = new Hide();
            return hide;
        }

        public override void Update(float time)
        {
#if CC_ACTION_TYPE_MONO
            Debug.LogError($"MONO模式不能用Hide，gameObject隐藏后，update无法继续。");
#else
            m_target?.gameObject.SetActive(false);
#endif
        }

        public override IAction Reverse()
        {
            return Show.Create();
        }

        public override IAction Clone()
        {
            return Create();
        }
    }
    #endregion

    #region Enable Behaviour enabled = true
    public class Enable : ActionExecute
    {
        Behaviour m_behaviour;
        public static Enable Create()
        {
            var ret = new Enable();
            return ret;
        }

        public IAction InitSubjectComponent(Behaviour component)
        {
            if (component == null)
                this.LogError("InitSubjectComponent component is null");
            m_behaviour = component;
            return this;
        }

        public override void Update(float time)
        {
            if (m_behaviour != null)
                m_behaviour.enabled = true;
        }

        public override IAction Reverse()
        {
            return Disenable.Create();
        }

        public override IAction Clone()
        {
            return Create();
        }
    }
    #endregion

    #region Enable Behaviour enabled = false
    public class Disenable : ActionExecute
    {
        Behaviour m_behaviour;
        public static Disenable Create()
        {
            var ret = new Disenable();
            return ret;
        }

        public IAction InitSubjectComponent(Behaviour component)
        {
            if (component == null)
                this.LogError("InitSubjectComponent component is null");
            m_behaviour = component;
            return this;
        }

        public override void Update(float time)
        {
            if (m_behaviour != null)
                m_behaviour.enabled = false;
        }

        public override IAction Reverse()
        {
            return Enable.Create();
        }

        public override IAction Clone()
        {
            return Create();
        }
    }
    #endregion

    #region CallFunc 支持三个格式的System.Action 无参数 带System.Object 带Transform(不需要传，用CCAction主体的Transform)
    public class CallFunc : ActionExecute
    {
        Action m_callFunc;
        Action<System.Object> m_callFuncST;
        Action<UnityEngine.Transform> m_callFuncUT;
        System.Object m_selectorTarget;
        public static CallFunc Create(Action callFunc)
        {
            CallFunc ret = new CallFunc();
            if (ret != null && ret.InitWithFunction(callFunc))
                return ret;
            return null;
        }

        public static CallFunc Create(Action<System.Object> callFunc, System.Object selectTarget)
        {
            CallFunc ret = new CallFunc();
            if (ret != null && ret.InitWithFunction(callFunc,selectTarget))
                return ret;
            return null;
        }

        public static CallFunc Create(Action<UnityEngine.Transform> callFunc)
        {
            CallFunc ret = new CallFunc();
            if (ret != null && ret.InitWithFunction(callFunc))
                return ret;
            return null;
        }

        public bool InitWithFunction(Action callFunc)
        {
            m_callFunc = callFunc;
            return true;
        }

        public bool InitWithFunction(Action<System.Object> callFunc, System.Object selectTarget)
        {
            m_callFuncST = callFunc;
            m_selectorTarget = selectTarget;
            return true;
        }

        public bool InitWithFunction(Action<Transform> callFunc)
        {
            m_callFuncUT = callFunc;
            return true;
        }

        public virtual void Execute()
        {
            m_callFunc?.Invoke();
            m_callFuncST?.Invoke(m_selectorTarget);
            m_callFuncUT?.Invoke(m_target);
        }

        public override void Update(float time)
        {
            base.Update(time);
            Execute();
        }

        public override IAction Reverse()
        {
            CallFunc ret = new CallFunc();
            if (m_callFunc!=null)
                ret.InitWithFunction(m_callFunc);
            if (m_callFuncST != null)
                ret.InitWithFunction(m_callFuncST,m_selectorTarget);
            if (m_callFuncUT != null)
                ret.InitWithFunction(m_callFuncUT);
            return ret;
        }

        public override IAction Clone()
        {
            CallFunc ret = new CallFunc();
            if (m_callFunc != null)
                ret.InitWithFunction(m_callFunc);
            if (m_callFuncST != null)
                ret.InitWithFunction(m_callFuncST, m_selectorTarget);
            if (m_callFuncUT != null)
                ret.InitWithFunction(m_callFuncUT);
            return ret;
        }
    }
    #endregion

    #region RemoveSelf UnityEngine.Object.Destroy or UnityEngine.Object.DestroyImmediate
    public class RemoveSelf : ActionExecute
    {
        float m_delayTime = 0.0f;
        bool m_isImmediate = false;
        bool m_allowDestroyingAssets = false;
        public static RemoveSelf Create(float delayTime = 0.0f, bool isImmediate = false, bool allowDestroyingAssets = false)
        {
            RemoveSelf ret = new RemoveSelf();
            if (ret != null && ret.Init(delayTime, isImmediate, allowDestroyingAssets))
                return ret;
            return null;
        }

        public bool Init(float delayTime = 0.0f, bool isImmediate = false, bool allowDestroyingAssets = false)
        {
            m_delayTime = delayTime;
            m_isImmediate = isImmediate;
            m_allowDestroyingAssets = allowDestroyingAssets;
            return true;
        }

        public override void Update(float time)
        {
            base.Update(time);
            if (m_isImmediate)
                UnityEngine.Object.DestroyImmediate(m_target.gameObject, m_allowDestroyingAssets);
            else
                UnityEngine.Object.Destroy(m_target.gameObject, m_delayTime);
        }

        public override IAction Reverse()
        {
            return Create(m_delayTime, m_isImmediate, m_allowDestroyingAssets);
        }

        public override IAction Clone()
        {
            return Create(m_delayTime, m_isImmediate, m_allowDestroyingAssets);
        }
    }
    #endregion

    class Blink:ActionInterval
    {
        protected int m_blinkTimes;
        protected bool m_originalState;
        public static Blink Create(float d,int times)
        {
            var ret = new Blink();
            if(ret.InitWithDuration(d,times))
            {
                return ret;
            }
            return null;
        }

        public bool InitWithDuration(float d,int times)
        {
            if(base.InitWithDuration(d))
            {
                m_blinkTimes = times;
                return true;
            }
            return false;
        }

        public override void StartWithTarget(Transform target)
        {
            base.StartWithTarget(target);
            m_originalState = m_target.gameObject.activeSelf;
        }

        public override void Stop()
        {
            if(m_target)
            {
                m_target.gameObject.SetActive(m_originalState);
            }
            base.Stop();
        }

        public override void Update(float time)
        {
            if(m_target && !IsDone())
            {
                float slice = 1.0f / m_blinkTimes;
                float m = time % slice;
#if CC_ACTION_TYPE_MONO
                Debug.LogError($"MONO模式不能用Blink，gameObject隐藏后，update无法继续。当前显示状态应该为：{(m > slice / 2 ? true : false)}");
#else
                m_target.gameObject.SetActive(m > slice / 2 ? true : false);
#endif
            }
        }

        public override IAction Clone()
        {
            return Create(m_duration, m_blinkTimes);
        }

        public override IAction Reverse()
        {
            return Create(m_duration, m_blinkTimes);
        }
    }
    */
}