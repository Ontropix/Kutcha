using System;

namespace Kutcha.Core
{
    public interface IStoreNamingConvention
    {
        string GetStoreName<TRoot>() where TRoot : class, IKutchaRoot, new();
        string GetStoreName(Type rootType);
    }
}