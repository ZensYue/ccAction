//#define CC_ACTION_TYPE_MONO

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

using ActionObject = UnityEngine.Object;

namespace ccAction
{
	internal class m_hashElement
	{
		public List<IAction> actions = new List<IAction>();
		public ActionObject target;
		public bool safeMode = false;
		public int groupId;
		public int actionIndex;
		public IAction currentAction = null;
		public bool currentActionSalvaged;
		public bool paused = false;
	};

#if CC_ACTION_TYPE_MONO
	/// <summary>
	/// MONO模式，可以用AddComponent的方式处理，允许多个对象
	/// </summary>
	[DisallowMultipleComponent]
	public class CCActionManager : MonoBehaviour {
#else
	/// <summary>
	/// 非MONO模式，需要启动ccActionManager，每帧调用update
	/// </summary>
	public class CCActionManager
	{
#endif
		/// <summary>
		/// 这里改用Dictionary,cocos用链表
		/// </summary>
		private Dictionary<int, m_hashElement> m_targets = new Dictionary<int, m_hashElement>();
		private List<int> m_deletes = new List<int>();

		m_hashElement m_currentTarget;

		bool m_currentTargetSalvaged = false;

#if CC_ACTION_TYPE_MONO
		/// <summary>
		/// MOMO模式下可以使用的方法
		/// </summary>
		
		// Update is called once per frame
		void Update()
		{
			Profiler.BeginSample("ActionManager");
			OnUpdate(Time.deltaTime);
			Profiler.EndSample();
		}

		public void AddAction(CCAction action, bool paused = false)
		{
			AddAction(action, transform, paused);
		}
#else
		private static CCActionManager m_instance;
		public static CCActionManager Instance {
			get 
			{ 
				if(m_instance == null)
					m_instance = new CCActionManager();
				return m_instance;
			}
		}

		CCActionManager()
		{
			if (m_instance != null)
				this.LogError("ActionManager have be instanced,do not new it again !!!");
		}

		public void Update(float dt)
		{
			Profiler.BeginSample("ActionManager");
			OnUpdate(dt);
			Profiler.EndSample();
		}
#endif
		public int AddAction(IAction action,Transform target)
        {
			if (target == null)
            {
				this.LogError("target can't be null");
				return -1;
			}
			return AddAction(action, target.GetInstanceID(), target);
		}

		public int AddAction(IAction action,int groupId, ActionObject actionObject = null)
		{
			if (action == null)
				this.LogError("action can't be null");

			m_hashElement element;
			if(!m_targets.TryGetValue(groupId, out element))
			{
				element = new m_hashElement
				{
					paused = false,
					groupId = groupId,
					target = actionObject,
					safeMode = actionObject != null,
				};
				m_targets.Add(groupId, element);
			}
			action.GroupID = groupId;
			action.StartWithTarget();
			element.actions.Add(action);
			return groupId;
		}


		public void RemoveAction(int tag)
        {
            foreach (var m in m_targets)
            {
				var element = m.Value;
				foreach (var action in element.actions)
                {
					if(action.GetTag() == tag)
                    {
						RemoveAction(action);
						return;
                    }
                }
            }
        }

		public void RemoveAction(IAction action)
		{
			m_hashElement element;
			if(m_targets.TryGetValue(action.GroupID, out element))
			{
				if (!element.actions.Contains(action))
					return;
				var index = element.actions.IndexOf(action);
				if (action == element.currentAction && (!element.currentActionSalvaged))
					element.currentActionSalvaged = true;
				element.actions.Remove(action);
				if (element.actionIndex >= index)
					element.actionIndex--;
				if (element.actions.Count == 0)
				{
					if (element == m_currentTarget)
						m_currentTargetSalvaged = true;
					else
						m_targets.Remove(action.GroupID);
				}
			}
		}
		public void RemoveAllActionsFromTarget(ActionObject target)
        {
			RemoveAllActionsFromTarget(target.GetInstanceID());
		}

		public void RemoveAllActionsFromTarget(int groupId)
		{
			m_hashElement element;
			if (m_targets.TryGetValue(groupId, out element))
			{
				if (element.actions.Contains(element.currentAction) && (!element.currentActionSalvaged))
				{
					element.currentActionSalvaged = true;
				}
				element.actions.Clear();
				if (m_currentTarget == element)
					m_currentTargetSalvaged = true;
				else
					m_targets.Remove(groupId);
			}
		}

		private void OnUpdate(float dt)
		{
			m_deletes.Clear();
			foreach (var item in m_targets)
			{ 
				if(item.Value.safeMode && item.Value.target.Equals(null))
				{
					m_targets.Remove(item.Key);
					continue;
				}
				m_currentTarget = item.Value;
				m_currentTargetSalvaged = false;
				if(!m_currentTarget.paused)
				{
					m_currentTarget.actionIndex = 0;
					while(m_currentTarget.actionIndex < m_currentTarget.actions.Count)
					{
						m_currentTarget.currentAction = m_currentTarget.actions[m_currentTarget.actionIndex];
						if (m_currentTarget.currentAction == null)
							continue;
						// The 'actions' MutableArray may change while inside this loop
						m_currentTarget.currentActionSalvaged = false;

						m_currentTarget.currentAction.Step(dt);

						if (m_currentTarget.currentActionSalvaged)
						{
							// The currentAction told the node to remove it. To prevent the action from
							// accidentally deallocating itself before finishing its step, we retained
							// it. Now that step is done, it's safe to release it.
							m_currentTarget.currentAction = null;
						}
						else if(m_currentTarget.currentAction.IsDone())
						{
							m_currentTarget.currentAction.Stop();
							var action = m_currentTarget.currentAction;
							// Make currentAction nil to prevent removeAction from salvaging it.
							m_currentTarget.currentAction = null;
							// 删除已经做是否在循环的判断，无须m_currentTarget.actionIndex--
							RemoveAction(action);
						}
						m_currentTarget.currentAction = null;
						m_currentTarget.actionIndex++;
					}
				}
				if (m_currentTargetSalvaged && m_currentTarget.actions.Count == 0)
					m_deletes.Add(item.Key);
			}

			foreach (var target in m_deletes)
			{
				m_targets.Remove(target);
			}
		}
	}

}