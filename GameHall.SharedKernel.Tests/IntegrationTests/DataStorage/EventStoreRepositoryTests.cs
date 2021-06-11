using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using Newtonsoft.Json;
using Xunit;

namespace GameHall.SharedKernel.Tests.IntegrationTests.DataStorage
{
    public class EventStoreRepositoryTests
    {
        [Fact(Timeout = 20000)]
        public async Task ThatStoredEventsCanBeRehydrated()
        {
            var settings = ConnectionSettings.Create()
                .KeepReconnecting()
                .SetGossipTimeout(TimeSpan.FromMilliseconds(500))
                .SetGossipSeedEndPoints(
                    new IPEndPoint(IPAddress.Loopback, 2113)
                )
                .SetDefaultUserCredentials(new UserCredentials("admin", "changeit"));
            var connection = EventStoreConnection.Create(settings, new IPEndPoint(IPAddress.Loopback, 1113));
            await connection.ConnectAsync();

            var aggregateId = Guid.NewGuid();
            var list = new List<IEvent>()
            {
                new AccountCreatedEvent(aggregateId, "Eric"),
                new FundsDepositedEvent(aggregateId, 150),
                new FundsDepositedEvent(aggregateId, 100),
                new FundsWithdrawnEvent(aggregateId, 25),
            };

            foreach (var ev in list)
            {
                var json = JsonConvert.SerializeObject(ev,
                    new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.None});
                var payload = Encoding.UTF8.GetBytes(json);
                var eventStoreDataType = new EventData(Guid.NewGuid(), ev.GetType().Name, true, payload, null);
                await connection.AppendToStreamAsync(Infrastructure.DataStorage.EventStore.StreamId(aggregateId),
                    ExpectedVersion.Any, eventStoreDataType);
            }
        }
    }

    public class FundsWithdrawnEvent : IEvent
    {
        public Guid AggregateId { get; }
        public int Amount { get; }

        public FundsWithdrawnEvent(Guid aggregateId, int amount)
        {
            AggregateId = aggregateId;
            Amount = amount;
        }
    }

    public class FundsDepositedEvent : IEvent
    {
        public Guid AggregateId { get; }
        public int Amount { get; }

        public FundsDepositedEvent(Guid aggregateId, int amount)
        {
            AggregateId = aggregateId;
            Amount = amount;
        }
    }

    public class AccountCreatedEvent : IEvent
    {
        public Guid AggregateId { get; }
        public string Name { get; }

        public AccountCreatedEvent(Guid aggregateId, string name)
        {
            AggregateId = aggregateId;
            Name = name;
        }
    }

    public interface IEvent
    {
    }
}
