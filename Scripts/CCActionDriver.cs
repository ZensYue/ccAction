using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ccAction
{
    public class CCActionDriver : MonoBehaviour
    {
        static GameObject _driver;
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            if(!_driver)
            {
                _driver = new UnityEngine.GameObject($"[{nameof(CCActionDriver)}]");
                _driver.AddComponent<CCActionDriver>();
                UnityEngine.Object.DontDestroyOnLoad(_driver);
                CCLog.Log($"{nameof(CCActionDriver)} initalize !");
            }
        }

        // Update is called once per frame
        void Update()
        {
            CCAction.Update(Time.deltaTime);
        }
    }
}
