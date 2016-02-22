namespace Kutcha.Core
{
    public interface IKutchaContext : IKutchaReadOnlyContext
    {
        IKutchaStore<TRoot> GetStore<TRoot>() where TRoot : class, IKutchaRoot, new();
        
    }
}