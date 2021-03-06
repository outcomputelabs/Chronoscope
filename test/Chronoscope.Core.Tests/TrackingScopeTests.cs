﻿using Chronoscope.Core.Tests.Fakes;
using Chronoscope.Tests.Fakes;
using Microsoft.Extensions.Options;
using System;
using System.Globalization;
using Xunit;

namespace Chronoscope.Core.Tests
{
    public class TrackingScopeTests
    {
        [Fact]
        public void ThrowsOnNullContext()
        {
            // arrange
            var options = Options.Create(new ChronoscopeOptions());
            IChronoscopeContext context = null;
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var parentId = Guid.NewGuid();

            // act
            var result = Assert.Throws<ArgumentNullException>(() => new TrackingScope(options, context, id, name, parentId));

            // assert
            Assert.Equal(nameof(context), result.ParamName);
        }

        [Fact]
        public void UsesDefaultName()
        {
            // arrange
            var options = Options.Create(new ChronoscopeOptions
            {
                DefaultTaskScopeNameFormat = "Format {0}"
            });
            var context = new FakeChronoscopeContext
            {
                Sink = new FakeSinks(),
                EventFactory = new FakeTrackingEventFactory(),
                Clock = new FakeSystemClock()
            };
            var id = Guid.NewGuid();
            string name = null;
            var parentId = Guid.NewGuid();

            // act
            var scope = new TrackingScope(options, context, id, name, parentId);

            // assert
            Assert.Equal(string.Format(CultureInfo.InvariantCulture, options.Value.DefaultTaskScopeNameFormat, id), scope.Name);
        }

        [Fact]
        public void CreatesSubScope()
        {
            // arrange
            var id = Guid.NewGuid();
            string name = Guid.NewGuid().ToString();
            var parentId = Guid.NewGuid();
            var subId = Guid.NewGuid();
            var subName = Guid.NewGuid().ToString();
            var options = Options.Create(new ChronoscopeOptions());
            var context = new FakeChronoscopeContext
            {
                Sink = new FakeSinks(),
                EventFactory = new FakeTrackingEventFactory(),
                Clock = new FakeSystemClock(),
                ScopeFactory = new FakeTrackingScopeFactory()
            };
            var scope = new TrackingScope(options, context, id, name, parentId);

            // act
            var result = scope.CreateScope(subId, subName);

            // assert
            Assert.Equal(subId, result.Id);
            Assert.Equal(subName, result.Name);
            Assert.Equal(id, result.ParentId);
        }
    }
}