namespace Identity.API.Common.Settings
{
    /// <summary>
    /// RabbitMQ event bus settings.
    /// </summary>
    public class EventBusSettings
    {
        /// <summary>
        /// RabbitMQ host name.
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// Event bus (RabbitMQ-based) docker host name.
        /// </summary>
        public string DockerHostName { get; set; }

        /// <summary>
        /// Event bus (RabbitMQ-based) virtual host name.
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
    }
}