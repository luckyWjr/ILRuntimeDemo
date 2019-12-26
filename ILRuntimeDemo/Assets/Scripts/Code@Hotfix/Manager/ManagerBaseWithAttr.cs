using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hotfix.Manager
{
    public class AttributeData
    {
        public ManagerAttribute attribute;
        public Type type;
    }

    public class ManagerAttribute : Attribute
    {
        public string value { get; protected set; }
        public ManagerAttribute(string value)
        {
            this.value = value;
        }
    }

    public class ManagerBaseWithAttr<T, V> : ManagerBase<T>, IAttribute where T : IManager, new() where V : ManagerAttribute
    {
        protected Dictionary<string, AttributeData> m_atrributeDataDic;

        protected ManagerBaseWithAttr()
        {
            m_atrributeDataDic = new Dictionary<string, AttributeData>();
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
                    SaveAttribute(_attr.value, new AttributeData() { attribute = _attr, type = type });
                }
            }
        }

        public AttributeData GetAtrributeData(string attrValue)
        {
            AttributeData classData = null;
            m_atrributeDataDic.TryGetValue(attrValue, out classData);
            return classData;
        }

        public void SaveAttribute(string name, AttributeData data)
        {
            m_atrributeDataDic[name] = data;
        }

        public T2 CreateInstance<T2>(string attrValue) where T2 : class
        {
            var data = GetAtrributeData(attrValue);
            if (data == null)
            {
                Debug.LogError("没有找到:" + attrValue + " -" + typeof(T2).Name);
                return null;
            }
            if (data.type != null)
            {
                object[] p = GetInstanceParams(data);
                if (p.Length == 0)
                    return Activator.CreateInstance(data.type) as T2;
                else
                    return Activator.CreateInstance(data.type, p) as T2;
            }
            return null;
        }

        public virtual object[] GetInstanceParams(AttributeData data)
        {
            return new object[] { data.attribute.value };
        }
    }
}
