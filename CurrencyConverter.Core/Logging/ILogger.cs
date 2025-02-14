namespace CurrencyConverter.Core.Logging
{
    public interface ILogger
    {
        string Log(string message);
        string Log(Exception ex);
    }
}
