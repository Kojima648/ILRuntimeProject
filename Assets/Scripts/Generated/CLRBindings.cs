using System;
using System.Collections.Generic;
using System.Reflection;

namespace ILRuntime.Runtime.Generated
{
    class CLRBindings
    {


        /// <summary>
        /// Initialize the CLR binding, please invoke this AFTER CLR Redirection registration
        /// </summary>
        public static void Initialize(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            Window_Binding.Register(app);
            UnityEngine_GameObject_Binding.Register(app);
            Singleton_1_ResourceManager_Binding.Register(app);
            ResourceManager_Binding.Register(app);
            UnityEngine_Object_Binding.Register(app);
            DemoPanel_Binding.Register(app);
            UnityEngine_UI_Image_Binding.Register(app);
            UnityEngine_UI_Text_Binding.Register(app);
            UnityEngine_Color_Binding.Register(app);
            UnityEngine_UI_Graphic_Binding.Register(app);
            UnityEngine_Debug_Binding.Register(app);
            LoadingPanel_Binding.Register(app);
            GameMapManager_Binding.Register(app);
            UnityEngine_UI_Slider_Binding.Register(app);
            System_String_Binding.Register(app);
            Singleton_1_UIManager_Binding.Register(app);
            UIManager_Binding.Register(app);
            MenuPanel_Binding.Register(app);
            Singleton_1_ObjectManager_Binding.Register(app);
            ObjectManager_Binding.Register(app);
            Singleton_1_ConfigerManager_Binding.Register(app);
            ConfigerManager_Binding.Register(app);
            System_Object_Binding.Register(app);
            MonsterData_Binding.Register(app);
            System_Collections_Generic_List_1_MonsterBase_Binding.Register(app);
            MonsterBase_Binding.Register(app);
            UnityEngine_Input_Binding.Register(app);
            System_Int32_Binding.Register(app);
            System_Collections_Generic_List_1_Int32_Binding.Register(app);
            CLRBindingTestClass_Binding.Register(app);
            MonoSingleton_1_GameStart_Binding.Register(app);
            UnityEngine_MonoBehaviour_Binding.Register(app);
            UnityEngine_Time_Binding.Register(app);
            System_Single_Binding.Register(app);
            UnityEngine_WaitForSeconds_Binding.Register(app);
            System_NotSupportedException_Binding.Register(app);
            ILRuntimeManager_Binding.Register(app);
            TestDelegateMeth_Binding.Register(app);
            TestDelegateFunction_Binding.Register(app);
            System_Action_1_String_Binding.Register(app);

            ILRuntime.CLR.TypeSystem.CLRType __clrType = null;
        }

        /// <summary>
        /// Release the CLR binding, please invoke this BEFORE ILRuntime Appdomain destroy
        /// </summary>
        public static void Shutdown(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
        }
    }
}
