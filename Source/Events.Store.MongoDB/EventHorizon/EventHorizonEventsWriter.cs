// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;
using Dolittle.Logging;
using Dolittle.Runtime.EventHorizon.Consumer;
using Dolittle.Runtime.Events.Store.MongoDB.Events;
using Dolittle.Runtime.Events.Store.MongoDB.Streams;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Dolittle.Runtime.Events.Store.MongoDB.EventHorizon
{
    /// <summary>
    /// Represents an implementation of <see cref="IWriteEventHorizonEvents" />.
    /// </summary>
    public class EventHorizonEventsWriter : IWriteEventHorizonEvents
    {
        readonly FilterDefinitionBuilder<MongoDB.Events.Event> _eventFilter = Builders<MongoDB.Events.Event>.Filter;
        readonly EventStoreConnection _connection;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHorizonEventsWriter"/> class.
        /// </summary>
        /// <param name="connection">An <see cref="EventStoreConnection"/> to a MongoDB EventStore.</param>
        /// <param name="logger">An <see cref="ILogger"/>.</param>
        public EventHorizonEventsWriter(
            EventStoreConnection connection,
            ILogger logger)
        {
            _connection = connection;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task Write(CommittedEvent @event, ScopeId scope, CancellationToken cancellationToken)
        {
            await EventsToStreamsWriter.Write(
                _connection,
                await _connection.GetScopedEventLog(scope, cancellationToken).ConfigureAwait(false),
                _eventFilter,
                streamPosition => CreateEventFromEventHorizonEvent(@event, streamPosition.Value),
                cancellationToken).ConfigureAwait(false);
        }

        // TODO add OriginSequenceNumber to GRPC so that we can use it
        MongoDB.Events.Event CreateEventFromEventHorizonEvent(CommittedEvent @event, EventLogSequenceNumber sequenceNumber) =>
            new MongoDB.Events.Event(
                sequenceNumber,
                @event.ExecutionContext.ToStoreRepresentation(),
                new EventMetadata(
                    @event.Occurred,
                    @event.EventSource,
                    @event.Type.Id,
                    @event.Type.Generation,
                    @event.Public,
                    true,
                    sequenceNumber),
                new AggregateMetadata(),
                BsonDocument.Parse(@event.Content));
    }
}
