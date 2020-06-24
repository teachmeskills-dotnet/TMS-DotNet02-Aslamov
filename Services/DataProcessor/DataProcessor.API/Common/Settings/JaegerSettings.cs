namespace DataProcessor.API.Common.Settings
{
    /// <summary>
    /// Jaeger service settings.
    /// </summary>
    public class JaegerSettings
    {
        /// <summary>
        /// Name of tracing service (in Jaeger UI).
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Default agent host (Development environment).
        /// </summary>
        public string DefaultAgentHost { get; set; }

        /// <summary>
        /// Docker agent host (Production environment).
        /// </summary>
        public string DockerAgentHost { get; set; }

        /// <summary>
        /// Jaeger agent port.
        /// </summary>
        public string AgentPort { get; set; }

        /// <summary>
        /// Sampler type.
        /// </summary>
        public string SamplerType { get; set; }
    }
}