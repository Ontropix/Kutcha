namespace Kutcha.Core
{
    public interface IReadOnlyKutchaContext
    {
        IKutchaReadOnlyStore<TRoot> GetReadOnlyStore<TRoot>() where TRoot : class, IKutchaRoot, new();
    }
}