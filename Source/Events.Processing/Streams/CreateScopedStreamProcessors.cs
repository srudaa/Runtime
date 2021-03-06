// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Resilience;
using Dolittle.Runtime.Events.Store.Streams;
using Dolittle.Tenancy;

namespace Dolittle.Runtime.Events.Processing.Streams
{
    /// <summary>
    /// Represents an implementation <see cref="ICreateScopedStreamProcessors" />.
    /// </summary>
    public class CreateScopedStreamProcessors : ICreateScopedStreamProcessors
    {
        readonly IEventFetchers _eventFetchers;
        readonly IResilientStreamProcessorStateRepository _streamProcessorStates;
        readonly IAsyncPolicyFor<ICanFetchEventsFromStream> _eventsFetcherPolicy;
        readonly ILoggerManager _loggerManager;
        readonly TenantId _tenant;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateScopedStreamProcessors"/> class.
        /// </summary>
        /// <param name="eventFetchers">The <see cref="IEventFetchers" />.</param>
        /// <param name="streamProcessorStates">The <see cref="IResilientStreamProcessorStateRepository" />.</param>
        /// <param name="executionContextManager">The <see cref="IExecutionContextManager" />.</param>
        /// <param name="eventsFetcherPolicy">The <see cref="IAsyncPolicyFor{T}" /> <see cref="ICanFetchEventsFromStream" />.</param>
        /// <param name="loggerManager">The <see cref="ILoggerManager" />.</param>
        public CreateScopedStreamProcessors(
            IEventFetchers eventFetchers,
            IResilientStreamProcessorStateRepository streamProcessorStates,
            IExecutionContextManager executionContextManager,
            IAsyncPolicyFor<ICanFetchEventsFromStream> eventsFetcherPolicy,
            ILoggerManager loggerManager)
        {
            _eventFetchers = eventFetchers;
            _streamProcessorStates = streamProcessorStates;
            _eventsFetcherPolicy = eventsFetcherPolicy;
            _loggerManager = loggerManager;
            _tenant = executionContextManager.Current.Tenant;
        }

        /// <inheritdoc />
        public async Task<AbstractScopedStreamProcessor> Create(
            IStreamDefinition streamDefinition,
            IStreamProcessorId streamProcessorId,
            IEventProcessor eventProcessor,
            CancellationToken cancellationToken)
        {
            if (streamDefinition.Partitioned)
            {
                var eventFetcher = await _eventFetchers.GetPartitionedFetcherFor(eventProcessor.Scope, streamDefinition, cancellationToken).ConfigureAwait(false);
                return await CreatePartitionedScopedStreamProcessor(streamProcessorId, eventProcessor, eventFetcher, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                var eventFetcher = await _eventFetchers.GetFetcherFor(eventProcessor.Scope, streamDefinition, cancellationToken).ConfigureAwait(false);
                return await CreateUnpartitionedScopedStreamProcessor(streamProcessorId, eventProcessor, eventFetcher, cancellationToken).ConfigureAwait(false);
            }
        }

        async Task<Partitioned.ScopedStreamProcessor> CreatePartitionedScopedStreamProcessor(
            IStreamProcessorId streamProcessorId,
            IEventProcessor eventProcessor,
            ICanFetchEventsFromPartitionedStream eventsFromStreamsFetcher,
            CancellationToken cancellationToken)
        {
            var tryGetStreamProcessorState = await _streamProcessorStates.TryGetFor(streamProcessorId, cancellationToken).ConfigureAwait(false);
            if (!tryGetStreamProcessorState.Success)
            {
                tryGetStreamProcessorState = Partitioned.StreamProcessorState.New;
                await _streamProcessorStates.Persist(streamProcessorId, tryGetStreamProcessorState.Result, cancellationToken).ConfigureAwait(false);
            }

            if (!tryGetStreamProcessorState.Result.Partitioned) throw new ExpectedPartitionedStreamProcessorState(streamProcessorId);

            return new Partitioned.ScopedStreamProcessor(
                _tenant,
                streamProcessorId,
                tryGetStreamProcessorState.Result as Partitioned.StreamProcessorState,
                eventProcessor,
                _streamProcessorStates,
                eventsFromStreamsFetcher,
                new Partitioned.FailingPartitions(_streamProcessorStates, eventProcessor, eventsFromStreamsFetcher, _eventsFetcherPolicy, _loggerManager.CreateLogger<Partitioned.FailingPartitions>()),
                _eventsFetcherPolicy,
                _loggerManager.CreateLogger<Partitioned.ScopedStreamProcessor>());
        }

        async Task<ScopedStreamProcessor> CreateUnpartitionedScopedStreamProcessor(
            IStreamProcessorId streamProcessorId,
            IEventProcessor eventProcessor,
            ICanFetchEventsFromStream eventsFromStreamsFetcher,
            CancellationToken cancellationToken)
        {
            var tryGetStreamProcessorState = await _streamProcessorStates.TryGetFor(streamProcessorId, cancellationToken).ConfigureAwait(false);
            if (!tryGetStreamProcessorState.Success)
            {
                tryGetStreamProcessorState = StreamProcessorState.New;
                await _streamProcessorStates.Persist(streamProcessorId, tryGetStreamProcessorState.Result, cancellationToken).ConfigureAwait(false);
            }

            if (tryGetStreamProcessorState.Result.Partitioned) throw new ExpectedUnpartitionedStreamProcessorState(streamProcessorId);
            return new ScopedStreamProcessor(
                _tenant,
                streamProcessorId,
                tryGetStreamProcessorState.Result as StreamProcessorState,
                eventProcessor,
                _streamProcessorStates,
                eventsFromStreamsFetcher,
                _eventsFetcherPolicy,
                _loggerManager.CreateLogger<ScopedStreamProcessor>());
        }
    }
}