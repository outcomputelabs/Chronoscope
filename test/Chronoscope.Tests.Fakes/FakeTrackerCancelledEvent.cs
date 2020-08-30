﻿using Chronoscope.Events;
using System;

namespace Chronoscope.Tests.Fakes
{
    public class FakeTrackerCancelledEvent : ITrackerCancelledEvent
    {
        public Exception Exception { get; set; }

        public Guid TrackerId { get; set; }

        public TimeSpan Elapsed { get; set; }

        public Guid ScopeId { get; set; }

        public DateTimeOffset Timestamp { get; set; }
    }
}