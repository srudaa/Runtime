// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Runtime.Events.Processing.Contracts;
using Grpc.Core;
using Machine.Specifications;
using Moq;

namespace Dolittle.Runtime.Events.Procesing.EventHandlers.for_EventHandlerService
{
    public class when_connecting : given.an_event_handlers_service
    {
        protected static Mock<ServerCallContext> _serverCallContext;
        protected static Mock<IAsyncStreamReader<EventHandlerClientToRuntimeMessage>> _runtimeStream;
        protected static Mock<IServerStreamWriter<EventHandlerRuntimeToClientMessage>> _clientStream;

        Establish context = () =>
        {
            _runtimeStream = new Mock<IAsyncStreamReader<EventHandlerClientToRuntimeMessage>>();
            _clientStream = new Mock<IServerStreamWriter<EventHandlerRuntimeToClientMessage>>();
            _serverCallContext = new Mock<ServerCallContext>();
        };

        Because of = async () => await service.Connect(_runtimeStream.Object, _clientStream.Object, _serverCallContext.Object);
    }
}
