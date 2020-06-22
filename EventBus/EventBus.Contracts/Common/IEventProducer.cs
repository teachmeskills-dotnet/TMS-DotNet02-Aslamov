using System.Threading.Tasks;

namespace EventBus.Contracts.Common
{
    /// <summary>
    /// Define interface for events producer.
    /// </summary>
    public interface IEventProducer<T>
    {
        /// <summary>
        /// Send event to event bus.
        /// </summary>
        /// <param name="data">Data object.</param>
        Task<bool> Publish(T data);
    }
}