// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Runtime.Events.Processing.Filters;
using Dolittle.Runtime.Events.Streams;
using Machine.Specifications;

namespace Dolittle.Runtime.EventHorizon.Producer.for_PublicEventFilter.when_filtering_non_public_event
{
    public class with_partition : given.a_public_event_filter
    {
        static IFilterResult result;
        Because of = () => result = filter.Filter(a_non_public_event, Guid.NewGuid(), Guid.NewGuid(), default).GetAwaiter().GetResult();

        It should_filter_successfully = () => result.Succeeded.ShouldBeTrue();
        It should_not_filter_to_partition = () => result.Partition.ShouldEqual(PartitionId.NotSet);
        It should_not_be_included = () => result.IsIncluded.ShouldBeFalse();
    }
}