using System;
using Kutcha.Core;
using Kutcha.InMemory.ReadOnly;

namespace Kutcha.InMemory
{
    internal partial class InMemoryKutchaStore<TRoot> : InMemoryKutchaReadOnlyStore<TRoot>, IKutchaStore<TRoot> where TRoot : class, IKutchaRoot, new()
    {
        private void ValidateRoot(TRoot root)
        {
            Argument.IsNotNull(root, "root");
            Argument.StringNotEmpty(root.Id, "root.Id");
        }
        
        public void Truncate()
        {
            Container.Clear();
        }
    }
}