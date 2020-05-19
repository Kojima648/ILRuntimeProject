using System;
using System.Collections;
using System.Collections.Generic;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using UnityEngine;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

public class WindowAdapter : CrossBindingAdaptor
{
    public override Type BaseCLRType
    {
        get { return typeof(Window); }
    }

    public override Type AdaptorType
    {
        get { return typeof(Adapter); }
    }

    public override object CreateCLRInstance(AppDomain appdomain, ILTypeInstance instance)
    {
        return new Adapter(appdomain, instance);
    }

    class Adapter : Window, CrossBindingAdaptorType
    {
        private AppDomain m_Appdomain;
        private ILTypeInstance m_Instance;
        private IMethod m_Awake;
        private IMethod m_OnClose;
        private IMethod m_OnDisable;
        private IMethod m_OnShow;
        private IMethod m_OnUpdate;
        private IMethod m_ToString;
        private object[] m_ParamList = new object[3];
        private bool m_OnCloseInvoking = false;

        public Adapter()
        {
        }

        public Adapter(AppDomain appdomain, ILTypeInstance instance)
        {
            m_Appdomain = appdomain;
            m_Instance = instance;
        }


        public ILTypeInstance ILInstance => m_Instance;

        public override void Awake(object param1 = null, object param2 = null, object param3 = null)
        {
            if (m_Awake == null)
            {
                m_Awake = m_Instance.Type.GetMethod("Awake", 3);
            }

            if (m_Awake != null)
            {
                m_ParamList[0] = param1;
                m_ParamList[1] = param2;
                m_ParamList[2] = param3;
                m_Appdomain.Invoke(m_Awake, m_Instance, m_ParamList);
            }
        }

        public override void OnClose()
        {
            if (m_OnClose == null)
            {
                m_OnClose = m_Instance.Type.GetMethod("OnClose");
            }

            //必须要设定一个标识位来表示当前是否在调用中, 否则如果脚本类里调用了base.OnClose()就会造成无限循环
            if (m_OnClose != null && !m_OnCloseInvoking)
            {
                m_OnCloseInvoking = true;
                m_Appdomain.Invoke(m_OnClose, m_Instance);
                m_OnCloseInvoking = false;
            }
            else
            {
                base.OnClose();
            }
        }

        public override void OnDisable()
        {
            if (m_OnDisable == null)
            {
                m_OnDisable = m_Instance.Type.GetMethod("OnDisable");
            }

            if (m_OnDisable != null)
            {
                m_Appdomain.Invoke(m_OnDisable, m_Instance);
            }
        }

        public override void OnShow(object param1 = null, object param2 = null, object param3 = null)
        {
            if (m_OnShow == null)
            {
                m_OnShow = m_Instance.Type.GetMethod("OnShow", 3);
            }

            if (m_OnShow != null)
            {
                m_ParamList[0] = param1;
                m_ParamList[1] = param2;
                m_ParamList[2] = param3;
                m_Appdomain.Invoke(m_OnShow, m_Instance, m_ParamList);
            }
        }

        public override void OnUpdate()
        {
            if (m_OnUpdate == null)
            {
                m_OnUpdate = m_Instance.Type.GetMethod("OnUpdate");
            }

            if (m_OnUpdate != null)
            {
                m_Appdomain.Invoke(m_OnUpdate, m_Instance);
            }
        }

        public override string ToString()
        {
            if (m_ToString == null)
            {
                m_ToString = m_Appdomain.ObjectType.GetMethod("ToString", 0);
            }

            IMethod m = m_Instance.Type.GetVirtualMethod(m_ToString);
            if (m == null || m is ILMethod)
            {
                return m_Instance.ToString();
            }
            else
            {
                return m_Instance.Type.FullName;
            }
        }
    }
}