// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Runtime.Events.Store.Streams;
using Machine.Specifications;

namespace Dolittle.Runtime.Events.Store.MongoDB.Streams.for_EventFromEventLogFetcher.when_checking_if_it_can_fetch
{
    public class from_all_stream : given.all_dependencies
    {
        static bool result;

        Because of = () => result = fetcher.CanFetchFromStream(StreamId.AllStreamId);
        It should_be_able_to_fetch = () => result.ShouldBeTrue();
    }
}