// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Booting;
using Dolittle.Collections;
using Dolittle.DependencyInversion;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Runtime.Events.Processing.EventHorizon;
using Dolittle.Runtime.Events.Processing.Streams;
using Dolittle.Runtime.Events.Streams;
using Dolittle.Runtime.Tenancy;
using Dolittle.Tenancy;

namespace Dolittle.Runtime.Events.Processing
{
    /// <summary>
    /// Performs the boot procedure for the processing of events.
    /// </summary>
    public class BootProcedure : ICanPerformBootProcedure
    {
        readonly IExecutionContextManager _executionContextManager;
        readonly ITenants _tenants;
        readonly FactoryFor<IStreamProcessors> _getStreamProcessors;
        readonly FactoryFor<IWriteEventsToStreams> _getStreamWriter;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BootProcedure"/> class.
        /// </summary>
        /// <param name="executionContextManager">The <see cref="IExecutionContextManager" />.</param>
        /// <param name="tenants">The <see cref="ITenants" />.</param>
        /// <param name="getStreamProcessors">The <see cref="FactoryFor{IStreamProcessors}" />.</param>
        /// <param name="getStreamWriter">The <see cref="FactoryFor{IWriteEventsToStreams}" />.</param>
        /// <param name="logger">The <see cref="ILogger" />.</param>
        public BootProcedure(
            IExecutionContextManager executionContextManager,
            ITenants tenants,
            FactoryFor<IStreamProcessors> getStreamProcessors,
            FactoryFor<IWriteEventsToStreams> getStreamWriter,
            ILogger logger)
        {
            _getStreamProcessors = getStreamProcessors;
            _getStreamWriter = getStreamWriter;
            _logger = logger;
            _executionContextManager = executionContextManager;
            _tenants = tenants;
        }

        /// <inheritdoc/>
        public bool CanPerform() => true;

        /// <inheritdoc/>
        public void Perform()
        {
            CreateStreamProcessors();
        }

        void CreateStreamProcessors()
        {
            var tenants = _tenants.All;
            tenants.ForEach(tenant =>
            {
                _executionContextManager.CurrentFor(tenant);
                RegisterPublicEventsStream(tenant);
            });
        }

        void RegisterPublicEventsStream(TenantId tenant)
        {
            _logger.Debug($"Registering stream processor for the public event stream for tenant {tenant}");
            var filter = new PublicEventFilter(_getStreamWriter(), _logger);
            var eventHorizon = new EventHorizon.EventHorizon(_logger);
            _getStreamProcessors().Register(filter, StreamId.AllStreamId);
            _getStreamProcessors().Register(eventHorizon, StreamId.PublicEventsId);
        }
    }
}