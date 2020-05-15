using UnityEngine;

namespace HotFix_Project
{
    class TestDelegate
    {
        static TestDelegateMeth testDelegateMeth;
        static TestDelegateFunction testDelegateFun;
        static System.Action<string> testAction;

        public static void Initialize()
        {
            testDelegateMeth = Method;
            testDelegateFun = FunMethod;
            testAction = Action;
        }

        public static void Initialize2()
        {
            ILRuntimeManager.DelegateMethod = Method;
            ILRuntimeManager.DelegateFunc = FunMethod;
            ILRuntimeManager.DelegateAction = Action;
        }

        public static void RunTest()
        {
            testDelegateMeth(1219);
            string str = testDelegateFun("Rorschach");
            Debug.Log("TestDelegate.RunTest res = " + str);
            testAction("Action RRR");
        }

        public static void RunTest2()
        {
            ILRuntimeManager.DelegateMethod(1211);
            string str = ILRuntimeManager.DelegateFunc("David");
            Debug.Log("TestDelegate.RunTest2 res = " + str);
            ILRuntimeManager.DelegateAction("RRR");
        }

        static void Method(int a)
        {
            Debug.Log("TestDelegate.Method, a = " + a);
        }

        static string FunMethod(string a)
        {
            return "FunMethod:" + a;
        }

        static void Action(string a)
        {
            UnityEngine.Debug.Log("!! TestDelegate.Action, a = " + a);
        }
    }
}
