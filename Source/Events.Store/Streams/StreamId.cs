﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Concepts;

namespace Dolittle.Runtime.Events.Store.Streams
{
    /// <summary>
    /// Represents the identification of a stream.
    /// </summary>
    public class StreamId : ConceptAs<Guid>
    {
        /// <summary>
        /// Represents the all stream <see cref="StreamId"/>.
        /// </summary>
        public static StreamId EventLog = Guid.Empty;

        /// <summary>
        /// Gets a value indicating whether a <see cref="StreamId" /> is writeable for a user-defined filter.
        /// </summary>
        public bool IsNonWriteable => this == EventLog;

        /// <summary>
        /// Implicitly convert from a <see cref="Guid"/> to a <see cref="StreamId"/>.
        /// </summary>
        /// <param name="streamId"><see cref="Guid"/> representation.</param>
        public static implicit operator StreamId(Guid streamId) =>
            new StreamId { Value = streamId };

        /// <summary>
        /// Creates a new instance of <see cref="StreamId"/> with a unique id.
        /// </summary>
        /// <returns>A new <see cref="StreamId"/>.</returns>
        public static StreamId New() =>
            new StreamId { Value = Guid.NewGuid() };
    }
}
