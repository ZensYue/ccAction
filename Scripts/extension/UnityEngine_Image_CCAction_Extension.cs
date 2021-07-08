//------------------------------------------------------------------------------
//      Copyright (c) 2021 , ZensYue ZensYue@163.com
//      All rights reserved.
//      Use, modification and distribution are subject to the "MIT License"
//------------------------------------------------------------------------------

using ccAction;

namespace UnityEngine.UI
{
    public static class UnityEngine_Image_CCAction_Extension
    {
        /// <summary>
        /// image fillAmount
        /// </summary>
        /// <param name="img">图片</param>
        /// <param name="duration">时间</param>
        /// <param name="startp">起始fillAmount值</param>
        /// <param name="to">结束fillAmount值</param>
        /// <returns>IFiniteTimeAction</returns>
        public static ActionInterval CCFillAmout(this Image img, float duration, float to = 1)
        {

#if UNITY_EDITOR
            if (img.type != Image.Type.Filled)
            {
                CCLog.LogError("image type is not Filled");
            }
#endif
            to = Mathf.Clamp01(to);
            var action = CCActionFloat.Create(duration, img.fillAmount, to);
            action.SetInitFunc(() => { return img.fillAmount; });
            action.SetUpdate((t, result) => {
                img.fillAmount = result;
            });
            return action;
        }
    }
}
