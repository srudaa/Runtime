// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Runtime.Events.Store.MongoDB.Aggregates;
using Dolittle.Runtime.Events.Store.MongoDB.Processing.Streams;
using Dolittle.Runtime.Events.Store.Streams;
using MongoDB.Driver;

namespace Dolittle.Runtime.Events.Store.MongoDB
{
    /// <summary>
    /// Represents a connection to the MongoDB EventStore database.
    /// </summary>
    [SingletonPerTenant]
    public class EventStoreConnection
    {
        readonly DatabaseConnection _connection;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventStoreConnection"/> class.
        /// </summary>
        /// <param name="connection">A connection to the MongoDB database.</param>
        /// <param name="logger">An <see cref="ILogger"/>.</param>
        public EventStoreConnection(DatabaseConnection connection, ILogger logger)
        {
            _connection = connection;
            _logger = logger;

            MongoClient = connection.MongoClient;

            EventLog = connection.Database.GetCollection<MongoDB.Events.Event>(Constants.EventLogCollection);
            Aggregates = connection.Database.GetCollection<AggregateRoot>(Constants.AggregateRootInstanceCollection);

            StreamProcessorStates = connection.Database.GetCollection<AbstractStreamProcessorState>(Constants.StreamProcessorStateCollection);
            StreamDefinitions = connection.Database.GetCollection<MongoDB.Streams.StreamDefinition>(Constants.StreamDefinitionCollection);

            CreateCollectionsAndIndexes();
        }

        /// <summary>
        /// Gets the <see cref="IMongoClient"/> configured for the MongoDB database.
        /// </summary>
        public IMongoClient MongoClient { get; }

        /// <summary>
        /// Gets the <see cref="IMongoCollection{Event}"/> where Events in the event log are stored.
        /// </summary>
        public IMongoCollection<MongoDB.Events.Event> EventLog { get; }

        /// <summary>
        /// Gets the <see cref="IMongoCollection{AggregateRoot}"/> where Aggregate Roots are stored.
        /// </summary>
        public IMongoCollection<AggregateRoot> Aggregates { get; }

        /// <summary>
        /// Gets the <see cref="IMongoCollection{StreamProcessorState}" /> where <see cref="AbstractStreamProcessorState" >stream processor states</see> are stored.
        /// </summary>
        public IMongoCollection<AbstractStreamProcessorState> StreamProcessorStates { get; }

        /// <summary>
        /// Gets the <see cref="IMongoCollection{TDocument}" /> for <see cref="MongoDB.Streams.StreamDefinition" />.
        /// </summary>
        public IMongoCollection<MongoDB.Streams.StreamDefinition> StreamDefinitions { get; }

        /// <summary>
        /// Gets the correct <see cref="IMongoCollection{TDocument}" /> for <see cref="Events.StreamEvent" />.
        /// </summary>
        /// <param name="scope">The <see cref="ScopeId" />.</param>
        /// <param name="stream">The <see cref="StreamId" />.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" />.</param>
        /// <returns>The collection.</returns>
        public Task<IMongoCollection<Events.StreamEvent>> GetStreamCollection(ScopeId scope, StreamId stream, CancellationToken cancellationToken) =>
            scope == ScopeId.Default ? GetStreamCollection(stream, cancellationToken) : GetScopedStreamCollection(scope, stream, cancellationToken);

        /// <summary>
        /// Gets the <see cref="IMongoCollection{StreamEvent}" /> that represents a stream of events.
        /// </summary>
        /// <param name="stream">The <see cref="StreamId" />.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" />.</param>
        /// <returns>The <see cref="IMongoCollection{StreamEvent}" />.</returns>
        public async Task<IMongoCollection<Events.StreamEvent>> GetStreamCollection(StreamId stream, CancellationToken cancellationToken)
        {
            var collection = _connection.Database.GetCollection<Events.StreamEvent>(Constants.CollectionNameForStream(stream));
            await CreateCollectionsAndIndexesForStreamEventsAsync(collection, cancellationToken).ConfigureAwait(false);
            return collection;
        }

        /// <summary>
        /// Gets the <see cref="IMongoCollection{ReceivedEvent}" /> that represents a collection of the events received from a microservice.
        /// </summary>
        /// <param name="scope">The <see cref="ScopeId" />.</param>
        /// <param name="stream">The <see cref="StreamId" />.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" />.</param>
        /// <returns>The <see cref="IMongoCollection{StreamEvent}" />.</returns>
        public async Task<IMongoCollection<Events.StreamEvent>> GetScopedStreamCollection(ScopeId scope, StreamId stream, CancellationToken cancellationToken)
        {
            var collection = _connection.Database.GetCollection<Events.StreamEvent>(Constants.CollectionNameForScopedStream(scope, stream));
            await CreateCollectionsAndIndexesForStreamEventsAsync(collection, cancellationToken).ConfigureAwait(false);
            return collection;
        }

        /// <summary>
        /// Gets the correct <see cref="IMongoCollection{TDocument}" /> for <see cref="AbstractStreamProcessorState" />.
        /// </summary>
        /// <param name="scope">The <see cref="ScopeId" />.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" />.</param>
        /// <returns>The collection.</returns>
        public Task<IMongoCollection<AbstractStreamProcessorState>> GetStreamProcessorStateCollection(
            ScopeId scope,
            CancellationToken cancellationToken) =>
            scope == ScopeId.Default ? Task.FromResult(StreamProcessorStates)
                : GetScopedStreamProcessorStateCollection(scope, cancellationToken);

        /// <summary>
        /// Gets the scoped <see cref="IMongoCollection{T}" /> of <see cref="StreamProcessorState" />.
        /// </summary>
        /// <param name="scope">The <see cref="ScopeId" />.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" />.</param>
        /// <returns>The <see cref="IMongoCollection{StreamProcessorState}" />.</returns>
        public async Task<IMongoCollection<AbstractStreamProcessorState>> GetScopedStreamProcessorStateCollection(
            ScopeId scope,
            CancellationToken cancellationToken)
        {
            var collection = _connection.Database.GetCollection<AbstractStreamProcessorState>(Constants.CollectionNameForScopedStreamProcessorStates(scope));
            await CreateCollectionsAndIndexesForStreamProcessorStatesAsync(collection, cancellationToken).ConfigureAwait(false);
            return collection;
        }

        /// <summary>
        /// Gets the correct event log <see cref="IMongoCollection{TDocument}" /> for <see cref="Event" />.
        /// </summary>
        /// <param name="scope">The <see cref="ScopeId" />.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" />.</param>
        /// <returns>The collection.</returns>
        public Task<IMongoCollection<MongoDB.Events.Event>> GetEventLogCollection(ScopeId scope, CancellationToken cancellationToken) =>
            scope == ScopeId.Default ? Task.FromResult(EventLog) : GetScopedEventLog(scope, cancellationToken);

        /// <summary>
        /// Gets the correct Stream Definitions <see cref="IMongoCollection{TDocument}" /> for <see cref="MongoDB.Streams.StreamDefinition" />.
        /// </summary>
        /// <param name="scope">The <see cref="ScopeId" />.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" />.</param>
        /// <returns>The collection.</returns>
        public Task<IMongoCollection<MongoDB.Streams.StreamDefinition>> GetStreamDefinitionsCollection(ScopeId scope, CancellationToken cancellationToken) =>
            scope == ScopeId.Default ? Task.FromResult(StreamDefinitions) : GetScopedStreamDefinitions(scope, cancellationToken);

        /// <summary>
        /// Gets the <see cref="IMongoCollection{T}" /> for the Stream Definitions.
        /// </summary>
        /// <param name="scope">The <see cref="ScopeId" />.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" />.</param>
        /// <returns>The <see cref="IMongoCollection{StreamEvent}" />.</returns>
        public async Task<IMongoCollection<MongoDB.Streams.StreamDefinition>> GetScopedStreamDefinitions(ScopeId scope, CancellationToken cancellationToken)
        {
            var collection = _connection.Database.GetCollection<MongoDB.Streams.StreamDefinition>(Constants.CollectionNameForScopedStreamDefinitions(scope));
            await CreateCollectionsIndexesForStreamDefinitionsAsync(collection, cancellationToken).ConfigureAwait(false);
            return collection;
        }

        /// <summary>
        /// Gets the <see cref="IMongoCollection{ReceivedEvent}" /> that represents a collection of the events received from a microservice.
        /// </summary>
        /// <param name="scope">The <see cref="ScopeId" />.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" />.</param>
        /// <returns>The <see cref="IMongoCollection{StreamEvent}" />.</returns>
        public async Task<IMongoCollection<MongoDB.Events.Event>> GetScopedEventLog(ScopeId scope, CancellationToken cancellationToken)
        {
            var collection = _connection.Database.GetCollection<MongoDB.Events.Event>(Constants.CollectionNameForScopedEventLog(scope));
            await CreateCollectionsIndexesForEventLogAsync(collection, cancellationToken).ConfigureAwait(false);
            return collection;
        }

        /// <summary>
        /// Gets the <see cref="IMongoCollection{StreamEvent}" /> that represents a stream of events.
        /// </summary>
        /// <param name="stream">The <see cref="StreamId" />.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" />.</param>
        /// <returns>The <see cref="IMongoCollection{StreamEvent}" />.</returns>
        public async Task<IMongoCollection<Events.StreamEvent>> GetPublicStreamCollection(StreamId stream, CancellationToken cancellationToken)
        {
            var collection = _connection.Database.GetCollection<Events.StreamEvent>(Constants.CollectionNameForPublicStream(stream));
            await CreateCollectionsAndIndexesForStreamEventsAsync(collection, cancellationToken).ConfigureAwait(false);
            return collection;
        }

        /// <summary>
        /// These methods implicitly also create the collections themselves as we create the indexes.
        /// </summary>
        void CreateCollectionsAndIndexes()
        {
            CreateCollectionsAndIndexesForEventLog();
            CreateCollectionsAndIndexesForAggregates();
            CreateCollectionsAndIndexesForStreamProcessorStates();
            CreateCollectionsAndIndexesForTypePartitionFilterDefinitions();
        }

        void CreateCollectionsAndIndexesForEventLog()
        {
            EventLog.Indexes.CreateOne(new CreateIndexModel<MongoDB.Events.Event>(
                Builders<MongoDB.Events.Event>.IndexKeys
                    .Ascending(_ => _.Metadata.EventSource)));

            EventLog.Indexes.CreateOne(new CreateIndexModel<MongoDB.Events.Event>(
                Builders<MongoDB.Events.Event>.IndexKeys
                    .Ascending(_ => _.Metadata.EventSource)
                    .Ascending(_ => _.Aggregate.TypeId)));
        }

        void CreateCollectionsAndIndexesForAggregates()
        {
            Aggregates.Indexes.CreateOne(new CreateIndexModel<AggregateRoot>(
                Builders<AggregateRoot>.IndexKeys
                    .Ascending(_ => _.EventSource)
                    .Ascending(_ => _.AggregateType),
                new CreateIndexOptions { Unique = true }));
        }

        /// <summary>
        /// Creates the compound index for <see cref="StreamProcessorState"/>.
        /// </summary>
        void CreateCollectionsAndIndexesForStreamProcessorStates()
        {
            StreamProcessorStates.Indexes.CreateOne(
                new CreateIndexModel<AbstractStreamProcessorState>(
                    Builders<AbstractStreamProcessorState>.IndexKeys
                        .Ascending(_ => _.ScopeId)
                        .Ascending(_ => _.EventProcessorId)
                        .Ascending(_ => _.SourceStreamId),
                    new CreateIndexOptions { Unique = true }));
        }

        void CreateCollectionsAndIndexesForTypePartitionFilterDefinitions()
        {
            StreamDefinitions.Indexes.CreateOne(new CreateIndexModel<MongoDB.Streams.StreamDefinition>(
                Builders<MongoDB.Streams.StreamDefinition>.IndexKeys
                    .Ascending(_ => _.StreamId)));
        }

        async Task CreateCollectionsAndIndexesForStreamEventsAsync(IMongoCollection<Events.StreamEvent> stream, CancellationToken cancellationToken)
        {
            await stream.Indexes.CreateOneAsync(
                new CreateIndexModel<MongoDB.Events.StreamEvent>(
                    Builders<MongoDB.Events.StreamEvent>.IndexKeys
                        .Ascending(_ => _.Metadata.EventLogSequenceNumber),
                    new CreateIndexOptions { Unique = true }),
                cancellationToken: cancellationToken).ConfigureAwait(false);

            await stream.Indexes.CreateOneAsync(
                new CreateIndexModel<MongoDB.Events.StreamEvent>(
                    Builders<MongoDB.Events.StreamEvent>.IndexKeys
                        .Ascending(_ => _.Metadata.EventSource)),
                cancellationToken: cancellationToken).ConfigureAwait(false);

            await stream.Indexes.CreateOneAsync(
                new CreateIndexModel<MongoDB.Events.StreamEvent>(
                    Builders<MongoDB.Events.StreamEvent>.IndexKeys
                        .Ascending(_ => _.Partition)),
                cancellationToken: cancellationToken).ConfigureAwait(false);

            await stream.Indexes.CreateOneAsync(
                new CreateIndexModel<MongoDB.Events.StreamEvent>(
                    Builders<MongoDB.Events.StreamEvent>.IndexKeys
                        .Ascending(_ => _.Metadata.EventSource)
                        .Ascending(_ => _.Aggregate.TypeId)),
                cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        async Task CreateCollectionsIndexesForStreamDefinitionsAsync(IMongoCollection<MongoDB.Streams.StreamDefinition> stream, CancellationToken cancellationToken)
        {
            await stream.Indexes.CreateOneAsync(
                new CreateIndexModel<MongoDB.Streams.StreamDefinition>(
                    Builders<MongoDB.Streams.StreamDefinition>.IndexKeys
                        .Ascending(_ => _.StreamId),
                    new CreateIndexOptions { Unique = true }),
                cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates the compound index for <see cref="StreamProcessorState"/>.
        /// </summary>
        /// <param name="streamProcessorState">Collection of <see cref="StreamProcessorState"/> to add indexes to.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>Task.</returns>
        async Task CreateCollectionsAndIndexesForStreamProcessorStatesAsync(
            IMongoCollection<AbstractStreamProcessorState> streamProcessorState,
            CancellationToken cancellationToken)
        {
            await streamProcessorState.Indexes.CreateOneAsync(
                new CreateIndexModel<AbstractStreamProcessorState>(
                    Builders<AbstractStreamProcessorState>.IndexKeys
                        .Ascending(_ => _.ScopeId)
                        .Ascending(_ => _.EventProcessorId)
                        .Ascending(_ => _.SourceStreamId),
                    new CreateIndexOptions { Unique = true }),
                cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        async Task CreateCollectionsIndexesForEventLogAsync(IMongoCollection<MongoDB.Events.Event> stream, CancellationToken cancellationToken)
        {
            await stream.Indexes.CreateOneAsync(
                new CreateIndexModel<MongoDB.Events.Event>(
                Builders<MongoDB.Events.Event>.IndexKeys
                    .Ascending(_ => _.Metadata.EventSource)),
                cancellationToken: cancellationToken).ConfigureAwait(false);

            await stream.Indexes.CreateOneAsync(
                new CreateIndexModel<MongoDB.Events.Event>(
                Builders<MongoDB.Events.Event>.IndexKeys
                    .Ascending(_ => _.Metadata.EventSource)
                    .Ascending(_ => _.Aggregate.TypeId)),
                cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
