// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using Dolittle.Logging;
using Dolittle.Runtime.Events.Processing;
using Machine.Specifications;

namespace Dolittle.Runtime.EventHorizon.Consumer.for_ReceivedEventsProcessor.when_processing
{
    public class an_event : given.all_dependencies
    {
        static ReceivedEventsProcessor processor;
        static IProcessingResult result;

        Establish context = () => processor = new ReceivedEventsProcessor(microservice, tenant, received_events_writer.Object, Moq.Mock.Of<ILogger>());

        Because of = () => result = processor.Process(@event, partition, default).GetAwaiter().GetResult();

        It should_write_event = () => received_events_writer.Verify(_ => _.Write(@event, microservice, tenant, Moq.It.IsAny<CancellationToken>()), Moq.Times.Once);
        It should_return_succeeded_processing_result = () => result.Succeeded.ShouldBeTrue();
    }
}