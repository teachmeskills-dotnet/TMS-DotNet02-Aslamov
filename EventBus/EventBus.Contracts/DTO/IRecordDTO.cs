namespace EventBus.Contracts.DTO
{
    /// <summary>
    /// Define interface for record DTO.
    /// </summary>
    public interface IRecordDTO : IDataDTO
    {
        /// <summary>
        /// Data identifier.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Mark deleted data.
        /// </summary>
        bool IsDeleted { get; set; }
    }
}