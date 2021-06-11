using System;
using System.Collections.Generic;
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
        [Fact(Timeout = 10000)]
        public async Task ThatStoredEventsCanBeRehydrated()
        {
            var settings = ConnectionSettings.Create()
                .KeepReconnecting()
                .SetGossipTimeout(TimeSpan.FromMilliseconds(1500))
                .SetGossipSeedEndPoints(
                    new IPEndPoint(IPAddress.Loopback, 2113)
                )
                .DisableTls()
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

            var expectedSum = 150 + 100 - 25;

            foreach (var ev in list)
            {
                var json = JsonConvert.SerializeObject(ev,
                    new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.None});
                var payload = Encoding.UTF8.GetBytes(json);
                var eventStoreDataType = new EventData(Guid.NewGuid(), ev.GetType().Name, true, payload, null);
                await connection.AppendToStreamAsync(Infrastructure.DataStorage.EventStore.StreamId(aggregateId),
                    ExpectedVersion.Any, eventStoreDataType);
            }

            var results = await connection.ReadStreamEventsForwardAsync(
                Infrastructure.DataStorage.EventStore.StreamId(aggregateId), StreamPosition.Start, 999, false);

            var account = new BankAccount();
            foreach (var result in results.Events)
            {
                var esJsonData = Encoding.UTF8.GetString(result.Event.Data);
                if (result.Event.EventType == "AccountCreatedEvent")
                {
                    var obj = JsonConvert.DeserializeObject<AccountCreatedEvent>(esJsonData);
                    account.Apply(obj);
                }
                if (result.Event.EventType == "FundsDepositedEvent")
                {
                    var obj = JsonConvert.DeserializeObject<FundsDepositedEvent>(esJsonData);
                    account.Apply(obj);
                }
                if (result.Event.EventType == "FundsWithdrawnEvent")
                {
                    var obj = JsonConvert.DeserializeObject<FundsWithdrawnEvent>(esJsonData);
                    account.Apply(obj);
                }

            }

            Assert.Equal("Eric", account.Name);
            Assert.Equal(expectedSum, account.CurrentBalance);

        }
    }

    public class BankAccount
    {
        public Guid Id { get; set; }

        public decimal CurrentBalance { get; set; }
        public List<MoneyTransaction> Transactions { get; set; } = new List<MoneyTransaction>();
        public string Name { get; set; }


        public void Apply(FundsDepositedEvent fundsDepositedEvent)
        {
            var item = new MoneyTransaction {Amount = fundsDepositedEvent.Amount};
            Transactions.Add(item);
            CurrentBalance += fundsDepositedEvent.Amount;
        }

        public void Apply(AccountCreatedEvent accountCreatedEvent)
        {
            Name = accountCreatedEvent.Name;
        }

        public void Apply(FundsWithdrawnEvent fundsWithdrawnEvent)
        {
            var item = new MoneyTransaction { Amount = fundsWithdrawnEvent.Amount };
            Transactions.Add(item);
            CurrentBalance -= fundsWithdrawnEvent.Amount;
        }
    }

    public class MoneyTransaction
    {
        public decimal Amount { get; set; }
    }

    public class FundsWithdrawnEvent : IEvent
    {
        public Guid AggregateId { get; }
        public decimal Amount { get; }

        public FundsWithdrawnEvent(Guid aggregateId, decimal amount)
        {
            AggregateId = aggregateId;
            Amount = amount;
        }
    }

    public class FundsDepositedEvent : IEvent
    {
        public Guid AggregateId { get; }
        public decimal Amount { get; }

        public FundsDepositedEvent(Guid aggregateId, decimal amount)
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
