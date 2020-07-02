// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.DependencyInversion;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Runtime.Events.Processing;
using Dolittle.Runtime.Events.Processing.Contracts;
using Dolittle.Runtime.Events.Processing.EventHandlers;
using Dolittle.Runtime.Events.Processing.Filters;
using Dolittle.Runtime.Events.Processing.Streams;
using Dolittle.Runtime.Events.Store;
using Dolittle.Runtime.Events.Store.Streams;
using Dolittle.Services;
using Grpc.Core;
using Machine.Specifications;
using Moq;

namespace Dolittle.Runtime.Events.Procesing.EventHandlers.for_EventHandlerService.given
{
    public class an_event_handlers_service
    {
        protected static EventHandlersService service;
        protected static ScopeId scope;
        protected static EventProcessorId event_processor_id;
        protected static Mock<IReverseCallDispatchers> dispatchers;
        protected static Mock<IExecutionContextManager> execution_context_manager;
        Establish context = () =>
        {
            var filterForAllTenants = new Mock<IValidateFilterForAllTenants>();
            var streamProcessors = new Mock<IStreamProcessors>();

            var getEventsToStreamsWriter = new Mock<FactoryFor<IWriteEventsToStreams>>();
            var streamDefinitions = new Mock<IStreamDefinitions>();

            dispatchers = new Mock<IReverseCallDispatchers>();
            dispatchers.Setup(_ => _.GetFor<EventHandlerClientToRuntimeMessage, EventHandlerRuntimeToClientMessage, EventHandlerRegistrationRequest, EventHandlerRegistrationResponse, HandleEventRequest, EventHandlerResponse>(
                Moq.It.IsAny<IAsyncStreamReader<EventHandlerClientToRuntimeMessage>>(),
                Moq.It.IsAny<IServerStreamWriter<EventHandlerRuntimeToClientMessage>>(),
                Moq.It.IsAny<ServerCallContext>(),
                Moq.It.IsAny<Func<EventHandlerClientToRuntimeMessage, EventHandlerRegistrationRequest>>(),

            ));
)
            execution_context_manager = new Mock<IExecutionContextManager>();

            var loggerManager = new Mock<ILoggerManager>();
            loggerManager.Setup(_ => _.CreateLogger<EventHandlersService>())
                .Returns(new Mock<ILogger<EventHandlersService>>().Object);

            service = new EventHandlersService(
                filterForAllTenants.Object,
                streamProcessors.Object,
                getEventsToStreamsWriter.Object,
                streamDefinitions.Object,
                dispatchers.Object,
                execution_context_manager.Object,
                loggerManager.Object);

            scope = Guid.NewGuid();
            event_processor_id = Guid.NewGuid();
        };
    }
}
