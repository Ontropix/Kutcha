namespace Kutcha.Core
{
    public interface IKutchaReadOnlyContext
    {
        IKutchaReadOnlyStore<TRoot> GetReadOnlyStore<TRoot>() where TRoot : class, IKutchaRoot, new();
    }
}