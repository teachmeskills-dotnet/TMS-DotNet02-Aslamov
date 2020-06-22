namespace EventBus.Contracts.Events
{
    /// <summary>
    /// Define message interface for record processed event.
    /// </summary>
    public interface IRecordProcessed
    {
        /// <summary>
        /// Message text.
        /// </summary>
        string Text { get; set; }
    }
}