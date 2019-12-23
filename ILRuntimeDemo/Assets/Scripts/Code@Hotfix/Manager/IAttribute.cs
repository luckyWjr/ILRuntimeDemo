using System;
namespace Hotfix.Manager
{
    public interface IAttribute
    {
        void CheckType(Type type);
        T2 CreateInstance<T2>(string typeName, params object[] args) where T2 : class;
        AtrributeData GetAtrributeData(string typeName);
    }
}