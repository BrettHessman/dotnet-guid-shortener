using System;
using Xunit;

namespace GuidShortener.Test
{
    public class Basic64Tests
    {
        [Fact]
        public void InputEqualsOutputZeroGuidFor64()
        {
            var guid = Guid.Empty;
            var str = GuidShortener.ToB64String(guid);
            var resl = GuidShortener.FromB64ToGuid(str);
            Assert.Equal(guid, resl);
        }

        [Fact]
        public void InputEqualsOutputOneGuidFor64()
        {
            var guid = Guid.Parse("00000001-0000-0000-0000-000000000000");
            var str = GuidShortener.ToB64String(guid);
            var resl = GuidShortener.FromB64ToGuid(str);
            Assert.Equal(guid, resl);
        }

        [Fact]
        public void InputEqualsOutputMaxGuidFor64()
        {
            var guid = Guid.Parse("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF");
            var str = GuidShortener.ToB64String(guid);
            var resl = GuidShortener.FromB64ToGuid(str);
            Assert.Equal(guid, resl);
        }

        [Fact]
        public void InputEqualsOutputKnownGuidFor64()
        {
            var guid = Guid.Parse("936E7B0C-0FB7-45F3-8DD1-F9263EAA8B36");
            var str = GuidShortener.ToB64String(guid);
            var resl = GuidShortener.FromB64ToGuid(str);
            Assert.Equal(guid, resl);
        }

        [Fact]
        public void InputEqualsOutputArbitraryGuidFor64()
        {
            var guid = Guid.NewGuid();
            var str = GuidShortener.ToB64String(guid);
            var resl = GuidShortener.FromB64ToGuid(str);
            Assert.Equal(guid, resl);
        }

    }
}
