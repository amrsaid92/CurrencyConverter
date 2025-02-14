using CurrencyConverter.RepositoryInterfaces.General;

namespace CurrencyConverter.ServiceInterfaces.General
{
    public interface IUnitWorkService : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
    }
}