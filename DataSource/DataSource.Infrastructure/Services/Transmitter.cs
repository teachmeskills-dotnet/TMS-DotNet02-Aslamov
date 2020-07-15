using DataSource.Application.DTO;
using DataSource.Application.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataSource.Infrastructure.Services
{
    /// <summary>
    /// Define class to send generated data to specified API.
    /// </summary>
    public class Transmitter : ITransmitter, IDisposable
    {
        // Disposing status.
        private bool _disposed = false;

        // Http client.
        private static HttpClient _client;

        /// <summary>
        /// Constructor of data transmitter.
        /// </summary>
        /// <param name="baseAddress">Http base address.</param>
        /// <param name="authorizationToken">Authorization token.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Transmitter(string baseAddress, string authorizationToken)
        {
            baseAddress = baseAddress ?? throw new ArgumentNullException(nameof(baseAddress));
            authorizationToken = authorizationToken ?? throw new ArgumentNullException(nameof(authorizationToken));

            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseAddress);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        ~Transmitter() => Dispose(false);

        /// <inheritdoc/>
        public async Task<bool> SendDataRecord(RecordDTO record)
        {
            var json = JsonConvert.SerializeObject(record);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var result = await _client.PostAsync(string.Empty, content);
                var success = result.StatusCode == HttpStatusCode.Created;
                return success;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Transmitter exception! {ex.Message}");
                return false;
            }   
        }

        /// <summary>
        /// Release the unmanaged resources and dispose managed resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
            }

            _client.Dispose();
            _disposed = true;
        }
    }
}