using System.Threading.Tasks;

namespace EventBus.Contracts.Common
{
    /// <summary>
    /// Define interface for events producer.
    /// U -- IEvent interface,
    /// T -- data transfer object.
    /// </summary>
    public interface IEventProducer<U,T>
    {
        /// <summary>
        /// Send event to event bus.
        /// </summary>
        /// <param name="data">Data object.</param>
        Task<bool> Publish(T data);
    }
}