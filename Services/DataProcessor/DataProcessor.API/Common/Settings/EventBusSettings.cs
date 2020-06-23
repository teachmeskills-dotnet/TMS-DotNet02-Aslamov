namespace DataProcessor.API.Common.Settings
{
    /// <summary>
    /// RabbitMQ event bus settings.
    /// </summary>
    public class EventBusSettings
    {
        /// <summary>
        /// Event bus host name.
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// Event bus (RabbitMQ-based) docker host name.
        /// </summary>
        public string DockerHostName { get; set; }

        /// <summary>
        /// RabbitMQ virtual host name.
        /// </summary>
        public string VirtualHostName { get; set; }

        /// <summary>
        /// RabbitMQ user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// RabbitMQ password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Queue for records (data) processing.
        /// </summary>
        public string DataProcessingQueue { get; set; }

        /// <summary>
        /// Queue for dataprocessing reports.
        /// </summary>
        public string ReportsQueue { get; set; }
    }
}