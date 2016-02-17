using System;

namespace Kutcha.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class KutchaStoreNameAttribute : Attribute
    {
        public string StoreName { get; private set; }

        public KutchaStoreNameAttribute(string storeName)
        {
            // TODO add regex validation on white-spaces
            Argument.StringNotEmpty(storeName, "storeName");
            StoreName = storeName.Trim();
        }
    }
}
