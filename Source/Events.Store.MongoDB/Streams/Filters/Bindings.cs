// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Collections;
using Dolittle.DependencyInversion;
using Dolittle.Types;

namespace Dolittle.Runtime.Events.Store.MongoDB.Streams.Filters
{
    /// <summary>
    /// Provides bindings.
    /// </summary>
    public class Bindings : ICanProvideBindings
    {
        readonly ITypeFinder _typeFinder;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bindings"/> class.
        /// </summary>
        /// <param name="typeFinder">The <see cref="ITypeFinder" />.</param>
        public Bindings(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        /// <inheritdoc/>
        public void Provide(IBindingProviderBuilder builder)
        {
            _typeFinder.FindMultiple(typeof(IRuntimeFilterConverterFor<>)).ForEach(_ => ProvideForGenericInterface(builder, typeof(IRuntimeFilterConverterFor<>), _));
            _typeFinder.FindMultiple(typeof(IPersistedFilterConverterFor<>)).ForEach(_ => ProvideForGenericInterface(builder, typeof(IPersistedFilterConverterFor<>), _));
        }

        void ProvideForGenericInterface(IBindingProviderBuilder builder, Type genericInterface, Type implementation)
        {
            var genericParameterType = implementation.GenericTypeArguments[0];
            builder.Bind(genericInterface.MakeGenericType(genericParameterType)).To(implementation);
        }
    }
}