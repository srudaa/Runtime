// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Runtime.Events.Store.Streams.Filters;

namespace Dolittle.Runtime.Events.Store.MongoDB.Streams.Filters
{
    /// <summary>
    /// Defines a system that can convert a runtime <see cref="IFilterDefinition" /> to a persisted filter definition.
    /// </summary>
    /// <typeparam name="TFilterDefinition">The <see cref="IFilterDefinition" /> type.</typeparam>
    public interface IRuntimeFilterConverterFor<TFilterDefinition>
        where TFilterDefinition : IFilterDefinition
    {
        /// <summary>
        /// Convert the <typeparamref name="TFilterDefinition" /> to <see cref="AbstractFilterDefinition" />.
        /// </summary>
        /// <param name="filterDefinition">The <typeparamref name="TFilterDefinition" />.</param>
        /// <returns>The converted <see cref="AbstractFilterDefinition" />.</returns>
        AbstractFilterDefinition Convert(TFilterDefinition filterDefinition);
    }
}
