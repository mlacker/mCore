using System;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using mCore.BuildingBlocks.EventBus;
using mCore.BuildingBlocks.EventBus.Abstractions;
using mCore.BuildingBlocks.EventBus.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace mCore.BuildingBlocks.EventBusRabbitMQ
{
    public class EventBusRabbitMQ : IEventBus, IDisposable
    {
        const string BROKER_NAME = "eshop_event_bus";

        private readonly IRabbitMQPersisterConnection persisterConnection;
        private readonly IEventBusSubscriptionsManager subsManager;
        private readonly ILifetimeScope autofac;

        private IModel consumerChannel;
        private string queueName;

        public EventBusRabbitMQ(
            IRabbitMQPersisterConnection persisterConnection,
            IEventBusSubscriptionsManager subsManager,
            ILifetimeScope autofac,
            string queueName = null)
        {
            this.persisterConnection = persisterConnection ?? throw new ArgumentNullException(nameof(persisterConnection));
            this.subsManager = subsManager ?? throw new ArgumentNullException(nameof(subsManager));
            this.autofac = autofac;
            this.queueName = queueName;
            this.consumerChannel = CreateConsumerChannel();

            this.subsManager.OnEventRemoved += SubsManager_OnEventRemoved;
        }

        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            if (!persisterConnection.IsConnected)
            {
                persisterConnection.TryConnect();
            }

            using (var channel = persisterConnection.CreateModel())
            {
                channel.QueueUnbind(
                    queue: queueName,
                    exchange: BROKER_NAME,
                    routingKey: eventName);

                if (subsManager.IsEmpty)
                {
                    queueName = string.Empty;
                    consumerChannel.Close();
                }
            }
        }

        public void Publish(IntegrationEvent @event)
        {
            if (!persisterConnection.IsConnected)
            {
                persisterConnection.TryConnect();
            }

            using (var channel = persisterConnection.CreateModel())
            {
                var eventName = @event.GetType().Name;

                channel.ExchangeDeclare(exchange: BROKER_NAME, type: "direct");

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                // persistent
                properties.DeliveryMode = 2;

                channel.BasicPublish(
                    exchange: BROKER_NAME,
                    routingKey: eventName,
                    mandatory: true,
                    basicProperties: properties,
                    body: body);
            }
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = subsManager.GetEventKey<T>();
            DoInternalSubscription(eventName);
            subsManager.AddSubscription<T, TH>();
        }

        public void SubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            DoInternalSubscription(eventName);
            subsManager.AddDynamicSubscription<TH>(eventName);
        }

        private void DoInternalSubscription(string eventName)
        {
            var containsKey = subsManager.HasSubscriptionsForEvent(eventName);

            if (!containsKey)
            {
                if (!persisterConnection.IsConnected)
                {
                    persisterConnection.TryConnect();
                }

                using (var channel = persisterConnection.CreateModel())
                {
                    channel.QueueBind(
                        queue: queueName,
                        exchange: BROKER_NAME,
                        routingKey: eventName);
                }
            }
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            subsManager.RemoveSubscription<T, TH>();
        }

        public void UnsubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            subsManager.RemoveDynamicSubscription<TH>(eventName);
        }

        public void Dispose()
        {
            if (consumerChannel != null)
            {
                consumerChannel.Dispose();
            }

            subsManager.Clear();
        }

        private IModel CreateConsumerChannel()
        {
            if (!persisterConnection.IsConnected)
            {
                persisterConnection.TryConnect();
            }

            var channel = persisterConnection.CreateModel();

            channel.ExchangeDeclare(exchange: BROKER_NAME, type: "direct");

            channel.QueueDeclare(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
           {
               var eventName = ea.RoutingKey;
               var message = Encoding.UTF8.GetString(ea.Body);

               await ProcessEvent(eventName, message);

               channel.BasicAck(ea.DeliveryTag, multiple: false);
           };

            channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

            channel.CallbackException += (sender, ea) =>
            {
                consumerChannel.Dispose();
                consumerChannel = CreateConsumerChannel();
            };

            return channel;
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (subsManager.HasSubscriptionsForEvent(eventName))
            {
                using (var scope = autofac.BeginLifetimeScope(BROKER_NAME))
                {
                    var subscriptions = subsManager.GetHandlersForEvent(eventName);
                    foreach (var subscription in subscriptions)
                    {
                        if (subscription.IsDynamic)
                        {
                            var handler = scope.ResolveOptional(subscription.HandlerType) as IDynamicIntegrationEventHandler;
                            dynamic eventData = JObject.Parse(message);
                            await handler.Handle(eventData);
                        }
                        else
                        {
                            var eventType = subsManager.GetEventTypeByName(eventName);
                            var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                            var handler = scope.ResolveOptional(subscription.HandlerType);
                            var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                            await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                        }
                    }
                }
            }
        }
    }
}
