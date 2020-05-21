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
    unsafe class LoadingPanel_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::LoadingPanel);

            field = type.GetField("m_Slider", flag);
            app.RegisterCLRFieldGetter(field, get_m_Slider_0);
            app.RegisterCLRFieldSetter(field, set_m_Slider_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_m_Slider_0, AssignFromStack_m_Slider_0);
            field = type.GetField("m_Text", flag);
            app.RegisterCLRFieldGetter(field, get_m_Text_1);
            app.RegisterCLRFieldSetter(field, set_m_Text_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_m_Text_1, AssignFromStack_m_Text_1);


        }



        static object get_m_Slider_0(ref object o)
        {
            return ((global::LoadingPanel)o).m_Slider;
        }

        static StackObject* CopyToStack_m_Slider_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::LoadingPanel)o).m_Slider;
            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_m_Slider_0(ref object o, object v)
        {
            ((global::LoadingPanel)o).m_Slider = (UnityEngine.UI.Slider)v;
        }

        static StackObject* AssignFromStack_m_Slider_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.UI.Slider @m_Slider = (UnityEngine.UI.Slider)typeof(UnityEngine.UI.Slider).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            ((global::LoadingPanel)o).m_Slider = @m_Slider;
            return ptr_of_this_method;
        }

        static object get_m_Text_1(ref object o)
        {
            return ((global::LoadingPanel)o).m_Text;
        }

        static StackObject* CopyToStack_m_Text_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::LoadingPanel)o).m_Text;
            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_m_Text_1(ref object o, object v)
        {
            ((global::LoadingPanel)o).m_Text = (UnityEngine.UI.Text)v;
        }

        static StackObject* AssignFromStack_m_Text_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.UI.Text @m_Text = (UnityEngine.UI.Text)typeof(UnityEngine.UI.Text).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            ((global::LoadingPanel)o).m_Text = @m_Text;
            return ptr_of_this_method;
        }



    }
}
