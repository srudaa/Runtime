// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;

namespace Dolittle.Runtime.Events.Store.Streams
{
    /// <summary>
    /// Defines a system that knows about <see cref="ICanFetchEventsFromStream">event fetchers</see>.
    /// </summary>
    public interface IEventFetchers
    {
        /// <summary>
        /// Gets an instance of <see cref="ICanFetchEventsFromStream" />.
        /// </summary>
        /// <param name="streamDefinition">The <see cref="IStreamDefinition" />.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" />.</param>
        /// <returns>A <see cref="Task" /> that, whn resolved, returns the <see cref="ICanFetchEventsFromStream" />.</returns>
        Task<ICanFetchEventsFromStream> GetFetcherFor(IStreamDefinition streamDefinition, CancellationToken cancellationToken);
    }
}
