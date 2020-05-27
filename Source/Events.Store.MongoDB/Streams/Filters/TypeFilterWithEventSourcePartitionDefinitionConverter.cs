// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using Dolittle.Runtime.Events.Store.Streams.Filters;
using Dolittle.Runtime.Events.Store.Streams.Filters.EventHorizon;

namespace Dolittle.Runtime.Events.Store.MongoDB.Streams.Filters
{
    /// <summary>
    /// Represents an implementation of <see cref="IRuntimeFilterConverterFor{TFilterDefinition}" /> for <see cref="TypeFilterWithEventSourcePartitionDefinition" />.
    /// </summary>
    public class TypeFilterWithEventSourcePartitionDefinitionConverter : IRuntimeFilterConverterFor<TypeFilterWithEventSourcePartitionDefinition>
    {
        public AbstractFilterDefinition Convert(TypeFilterWithEventSourcePartitionDefinition filterDefinition)
        {
            throw new System.NotImplementedException();
        }
    }
}
