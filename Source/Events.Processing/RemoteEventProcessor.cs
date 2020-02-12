// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Dolittle.Logging;

namespace Dolittle.Runtime.Events.Processing
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventProcessor" />that processes the handling of an event.
    /// </summary>
    public class RemoteEventProcessor : IEventProcessor
    {
        readonly IRemoteProcessorService _remoteProcessor;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteEventProcessor"/> class.
        /// </summary>
        /// <param name="id">The <see cref="EventProcessorId" />.</param>
        /// <param name="remoteProcessor">The <see cref="IRemoteProcessorService" />.</param>
        /// <param name="logger">The <see cref="ILogger" />.</param>
        public RemoteEventProcessor(
            EventProcessorId id,
            IRemoteProcessorService remoteProcessor,
            ILogger logger)
        {
            Identifier = id;
            _remoteProcessor = remoteProcessor;
            _logger = logger;
            LogMessageBeginning = $"Remote Event Processor '{Identifier}'";
        }

        /// <inheritdoc />
        public EventProcessorId Identifier { get; }

        string LogMessageBeginning { get; }

        /// <inheritdoc />
        public Task<IProcessingResult> Process(Store.CommittedEvent @event, PartitionId partitionId)
        {
            _logger.Debug($"{LogMessageBeginning} is processing event '{@event.Type.Id.Value}' for partition '{partitionId.Value}'");
            return _remoteProcessor.Process(@event, partitionId, Identifier);
        }
    }
}