using System;
using System.Reflection;

namespace Kutcha.Core
{
    public class AttributeStoreNamingConvention : IStoreNamingConvention
    {
        public string GetStoreName<TRoot>() where TRoot : class, IKutchaRoot, new()
        {
            Type rootType = typeof(TRoot);
            return GetStoreName(rootType);
        }

        public string GetStoreName(Type rootType)
        {
            if (!typeof (IKutchaRoot).IsAssignableFrom(rootType))
            {
                throw new ArgumentException("RootType must implement IKutchaRoot interface.");
            }

            var attribute = rootType.GetCustomAttribute<KutchaStoreNameAttribute>(inherit: false);
            return attribute != null ? attribute.StoreName : rootType.Name;
        }
    }
}