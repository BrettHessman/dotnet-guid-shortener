using System;
using Xunit;

namespace GuidShortener.Test
{
    public class Basic32Tests
    {
        [Fact]
        public void InputEqualsOutputZeroGuidFor32()
        {
            var guid = Guid.Empty;
            var str = GuidShortener.ToB32String(guid);
            var resl = GuidShortener.FromB32ToGuid(str);
            Assert.Equal(guid, resl);
        }

        [Fact]
        public void InputEqualsOutputOneGuidFor32()
        {
            var guid = Guid.Parse("00000001-0000-0000-0000-000000000000");
            var str = GuidShortener.ToB32String(guid);
            var resl = GuidShortener.FromB32ToGuid(str);
            Assert.Equal(guid, resl);
        }

        [Fact]
        public void InputEqualsOutputMaxGuidFor32()
        {
            var guid = Guid.Parse("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF");
            var str = GuidShortener.ToB32String(guid);
            var resl = GuidShortener.FromB32ToGuid(str);
            Assert.Equal(guid, resl);
        }

        [Fact]
        public void InputEqualsOutputKnownGuidFor32()
        {
            var guid = Guid.Parse("936E7B0C-0FB7-45F3-8DD1-F9263EAA8B36");
            var str = GuidShortener.ToB32String(guid);
            var resl = GuidShortener.FromB32ToGuid(str);
            Assert.Equal(guid, resl);
        }

        [Fact]
        public void InputEqualsOutputArbitraryGuidFor32()
        {
            var guid = Guid.NewGuid();
            var str = GuidShortener.ToB32String(guid);
            var resl = GuidShortener.FromB32ToGuid(str);
            Assert.Equal(guid, resl);
        }

    }
}
