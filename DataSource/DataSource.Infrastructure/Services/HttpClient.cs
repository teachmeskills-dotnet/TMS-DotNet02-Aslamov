using DataSource.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace DataSource.Infrastructure.Services
{
    /// <summary>
    /// Define class to send generated data to specified API.
    /// </summary>
    public class HttpClient : IHttpClient
    {
        /// <inheritdoc/>
        public Task<bool> SendDataRecord()
        {
            throw new NotImplementedException();
        }
    }
}