using System.Threading.Tasks;

namespace EventBus.Contracts.Common
{
    /// <summary>
    /// Define interface for commands producer.
    /// </summary>
    public interface ICommandProducer<T>
    {
        /// <summary>
        /// Send command to event bus.
        /// </summary>
        /// <param name="data">Data object.</param>
        Task<bool> Send(T data);
    }
}