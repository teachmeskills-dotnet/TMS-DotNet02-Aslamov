using DataSource.Application.DTO;
using System.Threading.Tasks;

namespace DataSource.Application.Interfaces
{
    /// <summary>
    /// Define interface for data sender.
    /// </summary>
    public interface ITransmitter
    {
        /// <summary>
        /// Send data records to specified API.
        /// </summary>
        /// <param name="record"></param>
        /// <returns>Operation status</returns>
        /// <summary>
        Task<bool> SendDataRecord(RecordDTO record);
    }
}