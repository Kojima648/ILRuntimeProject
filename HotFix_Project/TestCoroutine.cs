using System.Collections;
using UnityEngine;

namespace HotFix_Project
{
    class TestCoroutine
    {
        public static void RunTest()
        {
            GameStart.Instance.StartCoroutine(Coroutine());
        }

        static IEnumerator Coroutine()
        {
            Debug.Log("开始携程,t= " + Time.time);
            yield return new WaitForSeconds(3);
            Debug.Log("等待了3秒,t= " + Time.time);
        }
    }
}
