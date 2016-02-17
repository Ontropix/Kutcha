namespace Kutcha.Core
{
    public interface IKutchaContext : IReadOnlyKutchaContext
    {
        IKutchaStore<TRoot> GetStore<TRoot>() where TRoot : class, IKutchaRoot, new();
        
    }
}