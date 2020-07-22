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
    unsafe class DemoPanel_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::DemoPanel);

            field = type.GetField("image", flag);
            app.RegisterCLRFieldGetter(field, get_image_0);
            app.RegisterCLRFieldSetter(field, set_image_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_image_0, AssignFromStack_image_0);
            field = type.GetField("text", flag);
            app.RegisterCLRFieldGetter(field, get_text_1);
            app.RegisterCLRFieldSetter(field, set_text_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_text_1, AssignFromStack_text_1);


        }



        static object get_image_0(ref object o)
        {
            return ((global::DemoPanel)o).image;
        }

        static StackObject* CopyToStack_image_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::DemoPanel)o).image;
            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_image_0(ref object o, object v)
        {
            ((global::DemoPanel)o).image = (UnityEngine.UI.Image)v;
        }

        static StackObject* AssignFromStack_image_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.UI.Image @image = (UnityEngine.UI.Image)typeof(UnityEngine.UI.Image).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            ((global::DemoPanel)o).image = @image;
            return ptr_of_this_method;
        }

        static object get_text_1(ref object o)
        {
            return ((global::DemoPanel)o).text;
        }

        static StackObject* CopyToStack_text_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::DemoPanel)o).text;
            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_text_1(ref object o, object v)
        {
            ((global::DemoPanel)o).text = (UnityEngine.UI.Text)v;
        }

        static StackObject* AssignFromStack_text_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.UI.Text @text = (UnityEngine.UI.Text)typeof(UnityEngine.UI.Text).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            ((global::DemoPanel)o).text = @text;
            return ptr_of_this_method;
        }



    }
}
