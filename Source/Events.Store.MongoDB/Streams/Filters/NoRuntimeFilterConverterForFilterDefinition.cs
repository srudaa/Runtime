// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Runtime.Events.Store.Streams.Filters;

namespace Dolittle.Runtime.Events.Store.MongoDB.Streams.Filters
{
    /// <summary>
    /// Exception that gets thrown when a <see cref="IRuntimeFilterConverterFor{TFilterDefinition}" /> a filter definition was not found in the container.
    /// </summary>
    public class NoRuntimeFilterConverterForFilterDefinition : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoRuntimeFilterConverterForFilterDefinition"/> class.
        /// </summary>
        /// <param name="filterDefinitionType">The <see cref="Type" /> of the <see cref="IFilterDefinition" />.</param>
        public NoRuntimeFilterConverterForFilterDefinition(Type filterDefinitionType)
            : base($"Found no system that can convert filter definition {filterDefinitionType}")
        {
        }
    }
}