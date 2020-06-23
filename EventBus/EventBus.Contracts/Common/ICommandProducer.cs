using System.Threading.Tasks;

namespace EventBus.Contracts.Common
{
    /// <summary>
    /// Define interface for commands producer.
    /// U -- ICommand interface,
    /// T -- data transfer object.
    /// </summary>
    public interface ICommandProducer<U,T>
    {
        /// <summary>
        /// Send command to event bus.
        /// </summary>
        /// <param name="data">Data object to send.</param>
        Task<bool> Send(T data);
    }
}