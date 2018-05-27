using System;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace mCore.BuildingBlocks.EventBusRabbitMQ
{
    public class DefaultRabbitMQPersistentConnection : IRabbitMQPersisterConnection
    {
        private readonly IConnectionFactory connectionFactory;
        private readonly ILogger<DefaultRabbitMQPersistentConnection> logger;
        private IConnection connection;
        private bool disposed;

        private object sync_root = new object();

        public DefaultRabbitMQPersistentConnection(
            IConnectionFactory connectionFactory,
            ILogger<DefaultRabbitMQPersistentConnection> logger)
        {
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public bool IsConnected => connection != null && connection.IsOpen && !disposed;

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action.");
            }

            return connection.CreateModel();
        }

        public void Dispose()
        {
            if (disposed) return;

            disposed = true;

            connection.Dispose();
        }

        public bool TryConnect()
        {
            logger.LogInformation("RabbitMQ Client is trying to connect");

            lock (sync_root)
            {
                connection = connectionFactory.CreateConnection();

                if (IsConnected)
                {
                    connection.ConnectionBlocked += OnConnectionBlocked;
                    connection.CallbackException += OnCallbackException;
                    connection.ConnectionShutdown += OnConnectionShutdown;

                    logger.LogInformation($"RabbitMQ persistent connection acquired a connection {connection.Endpoint.HostName} and is subscribed to failure events");

                    return true;
                }
                else
                {
                    logger.LogCritical("FATAL ERROR: RabbitMQ connections could not be created and opened");

                    return false;
                }
            }
        }

        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (disposed) return;

            logger.LogWarning("A RabbitMQ connection is shutdown. Trying to re-connect...");

            TryConnect();
        }

        private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (disposed) return;

            logger.LogWarning("A RabbitMQ connection throw exception. Trying to re-connect...");

            TryConnect();
        }

        private void OnConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            if (disposed) return;

            logger.LogWarning("A RabbitMQ connection is on shutdown. Trying to re-connect...");

            TryConnect();
        }
    }
}
