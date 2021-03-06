// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.Runtime.Events.Store.MongoDB.Events.for_CommittedEventExtensions.when_converting_to_committed_event
{
    public class from_stream_event
    {
        static StreamEvent @event;
        static CommittedEvent result;

        Establish context = () =>
        {
            @event = events.a_stream_event_not_from_aggregate(random.stream_position, Guid.Parse("8872b3ec-c816-4b1f-b132-f53f2937f525"));
        };

        Because of = () => result = @event.ToCommittedEvent();

        It should_return_a_committed_event = () => result.ShouldBeOfExactType<CommittedEvent>();
        It should_represent_the_same_event = () => result.ShouldBeTheSameAs(@event);
    }
}