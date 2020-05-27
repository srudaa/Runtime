// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Runtime.Events.Store.Streams.Filters;

namespace Dolittle.Runtime.Events.Store.MongoDB.Streams.Filters
{
    /// <summary>
    /// Defines a system that knows about <see cref="IRuntimeFilterConverterFor{TFilterDefinition}" />.
    /// </summary>
    public interface IRuntimeFilterConverters
    {
        /// <summary>
        /// Converts a <typeparamref name="TFilterDefinition" /> to <see cref="AbstractFilterDefinition" />.
        /// </summary>
        /// <param name="filterDefinition">The <typeparamref name="TFilterDefinition" />.</param>
        /// <typeparam name="TFilterDefinition">The <see cref="IFilterDefinition" /> type.</typeparam>
        /// <returns>The converted <see cref="AbstractFilterDefinition "/>.</returns>
        AbstractFilterDefinition Convert<TFilterDefinition>(TFilterDefinition filterDefinition)
            where TFilterDefinition : IFilterDefinition;
    }
}
