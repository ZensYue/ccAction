using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ccAction
{
    /// <summary>
    /// 设置移动坐标系类型
    /// </summary>
    public enum PositionType
    {
        Local = 0,  //localPosition
        Global,     //position
        Anchored     //anchoredPosition3D
    }

    /// <summary>
    /// gameObject.SetActive(false) 代替方法类型
    /// </summary>
    public enum ActiveType
    {
        Move=0,     // 移动到场景外的固定位置
        Layer       // 设置到看不到的层级
    }

    public static class CCConst
    {
        static int m_layer = 0;
        public static int ActiveLayer { get { return m_layer; } set { ActiveLayer = value; } }

        static Vector3 m_pos = new Vector3(-100f, -100f, -100f);
        public static Vector3 ActivePosition { get { return m_pos; } set { m_pos = value; } }


        static ActiveType m_activeType = ActiveType.Move;
        public static ActiveType ActiveType { get { return m_activeType; } set { m_activeType = value; } }

        public static void SetActiveLayerMask(int layerMask)
        {
            ActiveLayer = (1 << layerMask);
        }
    }
}