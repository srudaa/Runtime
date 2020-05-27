// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Runtime.Events.Store.Streams.Filters;

namespace Dolittle.Runtime.Events.Store.MongoDB.Streams.Filters
{
    /// <summary>
    /// Defines a system that can convert a persisted filter definition to a <see cref="IFilterDefinition" />.
    /// </summary>
    /// <typeparam name="TFilterDefinition">The <see cref="AbstractFilterDefinition" /> type.</typeparam>
    public interface IPersistedFilterConverterFor<TFilterDefinition>
        where TFilterDefinition : AbstractFilterDefinition
    {
        /// <summary>
        /// Converts the <typeparamref name="TFilterDefinition" /> to <see cref="IFilterDefinition" />.
        /// </summary>
        /// <param name="filterDefinition">The <typeparamref name="TFilterDefinition" />.</param>
        /// <returns>The converted <see cref="IFilterDefinition" />.</returns>
        IFilterDefinition Convert(TFilterDefinition filterDefinition);
    }
}
