﻿using System;

namespace Chronoscope.Events
{
    /// <summary>
    /// Default implementation of <see cref="ITrackerCompletedEvent"/>.
    /// </summary>
    internal class TrackerCompletedEvent : ITrackerCompletedEvent
    {
        public TrackerCompletedEvent(Guid scopeId, Guid trackerId, DateTimeOffset timestamp, TimeSpan elapsed)
        {
            ScopeId = scopeId;
            TrackerId = trackerId;
            Timestamp = timestamp;
            Elapsed = elapsed;
        }

        public Guid ScopeId { get; }

        public Guid TrackerId { get; }

        public DateTimeOffset Timestamp { get; }

        public TimeSpan Elapsed { get; }
    }
}