using Microsoft.Extensions.Logging;

namespace FappCommon.Kafka.Interfaces;

public interface IKafkaLogger<out T> : ILogger<T>
{}