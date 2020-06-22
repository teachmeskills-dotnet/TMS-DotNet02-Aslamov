using System;

namespace EventBus.Contracts.Common
{
    /// <summary>
    /// Define interface for event bus commands.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Command Identifier.
        /// </summary>
        Guid CommandId { get; set; }

        /// <summary>
        /// Command creation date.
        /// </summary>
        DateTime CreationDate { get; set; }
    }
}