using System;
using RabbitMQ.Client;

namespace mCore.BuildingBlocks.EventBusRabbitMQ
{
    public interface IRabbitMQPersisterConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
