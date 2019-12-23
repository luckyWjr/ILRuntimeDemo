using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hotfix.Manager
{
    public class AtrributeData
    {
        public ManagerAtrribute attribute;
        public Type type;
    }

    public class ManagerAtrribute : Attribute
    {
        public string value { get; protected set; }
        public ManagerAtrribute(string value)
        {
            this.value = value;
        }
    }

    public class ManagerBaseWithAttr<T, V> : ManagerBase<T>, IAttribute where T : IManager, new() where V : ManagerAtrribute
    {
        protected Dictionary<string, AtrributeData> m_atrributeDataDic;

        protected ManagerBaseWithAttr()
        {
            m_atrributeDataDic = new Dictionary<string, AtrributeData>();
        }

        public virtual void CheckType(Type type)
        {
            var attrs = type.GetCustomAttributes(typeof(V), false);
            if (attrs.Length > 0)
            {
                var attr = attrs[0];
                if (attr is V)
                {
                    var _attr = (V)attr;
                    SaveAttribute(_attr.value, new AtrributeData() { attribute = _attr, type = type });
                }
            }
        }

        public AtrributeData GetAtrributeData(string typeName)
        {
            AtrributeData classData = null;
            m_atrributeDataDic.TryGetValue(typeName, out classData);
            return classData;
        }

        public void SaveAttribute(string name, AtrributeData data)
        {
            m_atrributeDataDic[name] = data;
        }

        public T2 CreateInstance<T2>(string typeName, params object[] args) where T2 : class
        {
            var data = GetAtrributeData(typeName);
            if (data == null)
            {
                Debug.LogError("没有找到:" + typeName + " -" + typeof(T2).Name);
                return null;
            }
            if (data.type != null)
                if (args.Length == 0)
                    return Activator.CreateInstance(data.type) as T2;
                else
                    return Activator.CreateInstance(data.type, args) as T2;
            else
                return null;
        }
    }
}
