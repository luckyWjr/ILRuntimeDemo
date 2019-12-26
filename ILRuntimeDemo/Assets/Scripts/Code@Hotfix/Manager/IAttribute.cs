using System;
namespace Hotfix.Manager
{
    public interface IAttribute
    {
        //检测符合IAttribute的类
        void CheckType(Type type);
        //获取Attribute信息
        AttributeData GetAtrributeData(string attrValue);
        //生成被管理类的实例，管理类为T，被管理的类为T2
        T2 CreateInstance<T2>(string attrValue) where T2 : class;
        //获取被管理类的构造函数参数
        object[] GetInstanceParams(AttributeData data);
    }
}