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
        public static IFiniteTimeAction CCShow(this GameObject obj)
        {
            var action = CCActionExecute.Create(() => { obj.SetActive(true); });
            return action;
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        /// <param name="obj">GameObject</param>
        /// <returns></returns>
        public static IFiniteTimeAction CCHide(this GameObject obj)
        {
            var action = CCActionExecute.Create(() => { obj.SetActive(false); });
            return action;
        }


    }
}
