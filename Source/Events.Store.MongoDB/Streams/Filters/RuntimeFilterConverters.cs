// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.DependencyInversion;
using Dolittle.Runtime.Events.Store.Streams.Filters;

namespace Dolittle.Runtime.Events.Store.MongoDB.Streams.Filters
{
    /// <summary>
    /// Represents an implementation of <see cref="IRuntimeFilterConverters" />.
    /// </summary>
    public class RuntimeFilterConverters : IRuntimeFilterConverters
    {
        readonly IContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeFilterConverters"/> class.
        /// </summary>
        /// <param name="container">The <see cref="IContainer" />.</param>
        public RuntimeFilterConverters(IContainer container)
        {
            _container = container;
        }

        /// <inheritdoc/>
        public AbstractFilterDefinition Convert<TFilterDefinition>(TFilterDefinition filterDefinition)
            where TFilterDefinition : IFilterDefinition
        {
            if (TryGetConverterFor<TFilterDefinition>(out var converter))
            {
                return converter.Convert(filterDefinition);
            }

            throw new NoRuntimeFilterConverterForFilterDefinition(typeof(TFilterDefinition));
        }

        bool TryGetConverterFor<TFilterDefinition>(out IRuntimeFilterConverterFor<TFilterDefinition> converter)
            where TFilterDefinition : IFilterDefinition
        {
            try
            {
                converter = _container.Get(typeof(IRuntimeFilterConverterFor<>).MakeGenericType(typeof(TFilterDefinition))) as IRuntimeFilterConverterFor<TFilterDefinition>;
            }
            catch
            {
                converter = null;
            }

            return converter != null;
        }
    }
}
