﻿namespace Sensor.API.Common.Settings
{
    /// <summary>
    /// Application settings.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Secret key.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Event bus (RabbitMQ-based) settings.
        /// </summary>
        public EventBusSettings EvengBusSettings { get; set; }
    }
}