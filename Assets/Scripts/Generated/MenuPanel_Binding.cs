using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class MenuPanel_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::MenuPanel);

            field = type.GetField("m_StartButton", flag);
            app.RegisterCLRFieldGetter(field, get_m_StartButton_0);
            app.RegisterCLRFieldSetter(field, set_m_StartButton_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_m_StartButton_0, AssignFromStack_m_StartButton_0);
            field = type.GetField("m_LoadButton", flag);
            app.RegisterCLRFieldGetter(field, get_m_LoadButton_1);
            app.RegisterCLRFieldSetter(field, set_m_LoadButton_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_m_LoadButton_1, AssignFromStack_m_LoadButton_1);
            field = type.GetField("m_ExitButton", flag);
            app.RegisterCLRFieldGetter(field, get_m_ExitButton_2);
            app.RegisterCLRFieldSetter(field, set_m_ExitButton_2);
            app.RegisterCLRFieldBinding(field, CopyToStack_m_ExitButton_2, AssignFromStack_m_ExitButton_2);
            field = type.GetField("m_Test1", flag);
            app.RegisterCLRFieldGetter(field, get_m_Test1_3);
            app.RegisterCLRFieldSetter(field, set_m_Test1_3);
            app.RegisterCLRFieldBinding(field, CopyToStack_m_Test1_3, AssignFromStack_m_Test1_3);
            field = type.GetField("m_Test3", flag);
            app.RegisterCLRFieldGetter(field, get_m_Test3_4);
            app.RegisterCLRFieldSetter(field, set_m_Test3_4);
            app.RegisterCLRFieldBinding(field, CopyToStack_m_Test3_4, AssignFromStack_m_Test3_4);
            field = type.GetField("m_Test2", flag);
            app.RegisterCLRFieldGetter(field, get_m_Test2_5);
            app.RegisterCLRFieldSetter(field, set_m_Test2_5);
            app.RegisterCLRFieldBinding(field, CopyToStack_m_Test2_5, AssignFromStack_m_Test2_5);


        }



        static object get_m_StartButton_0(ref object o)
        {
            return ((global::MenuPanel)o).m_StartButton;
        }

        static StackObject* CopyToStack_m_StartButton_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::MenuPanel)o).m_StartButton;
            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_m_StartButton_0(ref object o, object v)
        {
            ((global::MenuPanel)o).m_StartButton = (UnityEngine.UI.Button)v;
        }

        static StackObject* AssignFromStack_m_StartButton_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.UI.Button @m_StartButton = (UnityEngine.UI.Button)typeof(UnityEngine.UI.Button).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            ((global::MenuPanel)o).m_StartButton = @m_StartButton;
            return ptr_of_this_method;
        }

        static object get_m_LoadButton_1(ref object o)
        {
            return ((global::MenuPanel)o).m_LoadButton;
        }

        static StackObject* CopyToStack_m_LoadButton_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::MenuPanel)o).m_LoadButton;
            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_m_LoadButton_1(ref object o, object v)
        {
            ((global::MenuPanel)o).m_LoadButton = (UnityEngine.UI.Button)v;
        }

        static StackObject* AssignFromStack_m_LoadButton_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.UI.Button @m_LoadButton = (UnityEngine.UI.Button)typeof(UnityEngine.UI.Button).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            ((global::MenuPanel)o).m_LoadButton = @m_LoadButton;
            return ptr_of_this_method;
        }

        static object get_m_ExitButton_2(ref object o)
        {
            return ((global::MenuPanel)o).m_ExitButton;
        }

        static StackObject* CopyToStack_m_ExitButton_2(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::MenuPanel)o).m_ExitButton;
            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_m_ExitButton_2(ref object o, object v)
        {
            ((global::MenuPanel)o).m_ExitButton = (UnityEngine.UI.Button)v;
        }

        static StackObject* AssignFromStack_m_ExitButton_2(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.UI.Button @m_ExitButton = (UnityEngine.UI.Button)typeof(UnityEngine.UI.Button).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            ((global::MenuPanel)o).m_ExitButton = @m_ExitButton;
            return ptr_of_this_method;
        }

        static object get_m_Test1_3(ref object o)
        {
            return ((global::MenuPanel)o).m_Test1;
        }

        static StackObject* CopyToStack_m_Test1_3(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::MenuPanel)o).m_Test1;
            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_m_Test1_3(ref object o, object v)
        {
            ((global::MenuPanel)o).m_Test1 = (UnityEngine.UI.Image)v;
        }

        static StackObject* AssignFromStack_m_Test1_3(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.UI.Image @m_Test1 = (UnityEngine.UI.Image)typeof(UnityEngine.UI.Image).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            ((global::MenuPanel)o).m_Test1 = @m_Test1;
            return ptr_of_this_method;
        }

        static object get_m_Test3_4(ref object o)
        {
            return ((global::MenuPanel)o).m_Test3;
        }

        static StackObject* CopyToStack_m_Test3_4(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::MenuPanel)o).m_Test3;
            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_m_Test3_4(ref object o, object v)
        {
            ((global::MenuPanel)o).m_Test3 = (UnityEngine.UI.Image)v;
        }

        static StackObject* AssignFromStack_m_Test3_4(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.UI.Image @m_Test3 = (UnityEngine.UI.Image)typeof(UnityEngine.UI.Image).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            ((global::MenuPanel)o).m_Test3 = @m_Test3;
            return ptr_of_this_method;
        }

        static object get_m_Test2_5(ref object o)
        {
            return ((global::MenuPanel)o).m_Test2;
        }

        static StackObject* CopyToStack_m_Test2_5(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::MenuPanel)o).m_Test2;
            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_m_Test2_5(ref object o, object v)
        {
            ((global::MenuPanel)o).m_Test2 = (UnityEngine.UI.Image)v;
        }

        static StackObject* AssignFromStack_m_Test2_5(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.UI.Image @m_Test2 = (UnityEngine.UI.Image)typeof(UnityEngine.UI.Image).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            ((global::MenuPanel)o).m_Test2 = @m_Test2;
            return ptr_of_this_method;
        }



    }
}
