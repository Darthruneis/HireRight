using FluentAssertions;
using NUnit.Framework;

namespace HireRight.Persistence.Tests
{
    internal class FakeDto
    {
        internal string Prop1 { get; set; }
    }

    [TestFixture]
    public class MaybeTests
    {
        [Test]
        public void NullObjectConversionTest()
        {
            FakeDto dto = null;

            Maybe<FakeDto> maybe = dto;

            maybe.Should().NotBeNull();
            maybe.HasNoValue.Should().BeTrue();
        }

        [Test]
        public void NullConversionTest()
        {
            Maybe<FakeDto> maybe = null;

            maybe.Should().NotBeNull();
            maybe.HasNoValue.Should().BeTrue();
        }

        [Test]
        public void ValueConversionTest()
        {
            FakeDto dto = new FakeDto();

            Maybe<FakeDto> maybe = dto;

            maybe.Should().NotBeNull();
            maybe.HasValue.Should().BeTrue();
        }
    }
}