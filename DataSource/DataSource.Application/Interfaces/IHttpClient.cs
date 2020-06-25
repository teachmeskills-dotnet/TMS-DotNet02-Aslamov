using System.Threading.Tasks;

namespace DataSource.Application.Interfaces
{
    /// <summary>
    /// Define interface for HttpClient.
    /// </summary>
    public interface IHttpClient
    {
        /// <summary>
        /// Send data record to specified API.
        /// </summary>
        /// <returns>Operation status.</returns>
        Task<bool> SendDataRecord();
    }
}