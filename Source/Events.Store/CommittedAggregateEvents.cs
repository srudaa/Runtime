// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Collections.Generic;
using Dolittle.Artifacts;
using Dolittle.Collections;
using Dolittle.Events;

namespace Dolittle.Runtime.Events.Store
{
    /// <summary>
    /// Represents a sequence of <see cref="IEvent"/>s applied by an AggregateRoot to an Event Source that have been committed to the Event Store.
    /// </summary>
    public class CommittedAggregateEvents : IReadOnlyList<CommittedAggregateEvent>
    {
        readonly NullFreeList<CommittedAggregateEvent> _events;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommittedAggregateEvents"/> class.
        /// </summary>
        /// <param name="eventSource">The <see cref="EventSourceId"/> that the Events were applied to.</param>
        /// <param name="aggregateRoot">The <see cref="ArtifactId"/> representing the type of the Aggregate Root that applied the Event to the Event Source.</param>
        /// <param name="oldAggregateRootVersion">The version of the <see cref="AggregateRoot"/> that applied the Events.</param>
        /// <param name="newAggregateRootVersion">The new version of the <see cref="AggregateRoot"/> that applied the Events.</param>
        /// <param name="events">The <see cref="CommittedAggregateEvent">events</see>.</param>
        public CommittedAggregateEvents(EventSourceId eventSource, ArtifactId aggregateRoot, AggregateRootVersion oldAggregateRootVersion, AggregateRootVersion newAggregateRootVersion, IReadOnlyList<CommittedAggregateEvent> events)
        {
            EventSource = eventSource;
            AggregateRoot = aggregateRoot;
            AggregateRootVersion = oldAggregateRootVersion;

            for (var i = 0; i < events.Count; i++)
            {
                var @event = events[i];
                ThrowIfEventIsNull(@event);
                ThrowIfEventWasAppliedToOtherEventSource(@event);
                ThrowIfEventWasAppliedByOtherAggregateRoot(@event);
                ThrowIfAggreggateRootVersionIsOutOfOrder(@event);
                if (i > 0) ThrowIfEventLogVersionIsOutOfOrder(@event, events[i - 1]);
                AggregateRootVersion++;
            }

            ThrowIfEventsAreMissingForExpectedVersion(newAggregateRootVersion);

            _events = new NullFreeList<CommittedAggregateEvent>(events);
        }

        /// <summary>
        /// Gets the Event Source that the Events were applied to.
        /// </summary>
        public EventSourceId EventSource { get; }

        /// <summary>
        /// Gets the <see cref="ArtifactId"/> representing the type of the Aggregate Root that applied the Event to the Event Source.
        /// </summary>
        public ArtifactId AggregateRoot { get; }

        /// <summary>
        /// Gets the version of the <see cref="AggregateRoot"/> after all the Events was applied.
        /// </summary>
        public AggregateRootVersion AggregateRootVersion { get; }

        /// <summary>
        /// Gets a value indicating whether or not there are any events in the committed sequence.
        /// </summary>
        public bool HasEvents => Count > 0;

        /// <inheritdoc/>
        public int Count => _events.Count;

        /// <inheritdoc/>
        public CommittedAggregateEvent this[int index] => _events[index];

        /// <inheritdoc/>
        public IEnumerator<CommittedAggregateEvent> GetEnumerator() => _events.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => _events.GetEnumerator();

        void ThrowIfEventIsNull(CommittedAggregateEvent @event)
        {
            if (@event == null) throw new EventCanNotBeNull();
        }

        void ThrowIfEventWasAppliedToOtherEventSource(CommittedAggregateEvent @event)
        {
            if (@event.EventSource != EventSource) throw new EventWasAppliedToOtherEventSource(@event.EventSource, EventSource);
        }

        void ThrowIfEventWasAppliedByOtherAggregateRoot(CommittedAggregateEvent @event)
        {
            if (@event.AggregateRoot.Id != AggregateRoot) throw new EventWasAppliedByOtherAggregateRoot(@event.AggregateRoot.Id, AggregateRoot);
        }

        void ThrowIfAggreggateRootVersionIsOutOfOrder(CommittedAggregateEvent @event)
        {
            if (@event.AggregateRootVersion != AggregateRootVersion) throw new AggregateRootVersionIsOutOfOrder(@event.AggregateRootVersion, AggregateRootVersion);
        }

        void ThrowIfEventLogVersionIsOutOfOrder(CommittedAggregateEvent @event, CommittedAggregateEvent previousEvent)
        {
            if (@event.EventLogVersion != previousEvent.EventLogVersion + 1) throw new EventLogVersionIsOutOfOrder(@event.EventLogVersion, previousEvent.EventLogVersion + 1);
        }

        void ThrowIfEventsAreMissingForExpectedVersion(AggregateRootVersion aggregateRootVersion)
        {
            if (AggregateRootVersion != aggregateRootVersion) throw new MissingEventsForExpectedAggregateRootVersion(aggregateRootVersion, AggregateRootVersion);
        }
    }
}