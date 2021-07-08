//------------------------------------------------------------------------------
//      Copyright (c) 2021 , ZensYue ZensYue@163.com
//      All rights reserved.
//      Use, modification and distribution are subject to the "MIT License"
//------------------------------------------------------------------------------

using UnityEngine;
using System;

namespace ccAction
{
    /// <summary>
    /// 执行Action
    /// </summary>
    public class CCActionExecute : ActionInterval
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
}