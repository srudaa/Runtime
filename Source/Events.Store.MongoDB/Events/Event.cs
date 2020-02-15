// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dolittle.Runtime.Events.Store.MongoDB.Events
{
    /// <summary>
    /// Represents an event stored in the MongoDB event store.
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class.
        /// </summary>
        /// <param name="streamPosition">The position in the stream.</param>
        /// <param name="eventLogVersion">The event log version this event comes from.</param>
        /// <param name="partition">The partition that this event is in.</param>
        /// <param name="metadata">The The event metadata.</param>
        /// <param name="aggregate">The aggregate metadata.</param>
        /// <param name="content">The event content.</param>
        public Event(uint streamPosition, uint eventLogVersion, Guid partition, EventMetadata metadata, AggregateMetadata aggregate, BsonDocument content)
        {
            StreamPosition = streamPosition;
            EventLogVersion = eventLogVersion;
            Partition = partition;
            Metadata = metadata;
            Aggregate = aggregate;
            Content = content;
        }

        /// <summary>
        /// Gets or sets stream position.
        /// </summary>
        [BsonId]
        public uint StreamPosition { get; set; }

        /// <summary>
        /// Gets or sets the event log version of the event.
        /// </summary>
        public uint EventLogVersion { get; set; }

        /// <summary>
        /// Gets or sets the partition that the event belongs in.
        /// </summary>
        public Guid Partition { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="EventMetadata"/> containing the platform generated event information.
        /// </summary>
        public EventMetadata Metadata { get; set; }

        /// <summary>
        /// Gets or sets the event sourcing specific <see cref="AggregateMetadata"/>.
        /// </summary>
        public AggregateMetadata Aggregate { get; set; }

        /// <summary>
        /// Gets or sets the domain specific event data.
        /// </summary>
        public BsonDocument Content { get; set; }
    }
}