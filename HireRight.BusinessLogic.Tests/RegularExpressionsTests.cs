using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FluentAssertions;
using NUnit.Framework;

namespace HireRight.BusinessLogic.Tests
{
    [TestFixture]
    public class RegularExpressionsTests
    {
        public static IEnumerable<object[]> PhoneNumberData()
        {
            var dividerCharacters = new[] {' ', '.', '-'};

            //basic invalid characters tests
            yield return new object[] {"", false};
            yield return new object[] {"*", false};
            yield return new object[] {"a", false};
            yield return new object[] {"A", false};

            //no dividers tests
            yield return new object[] { "+1231234567890", true };
            yield return new object[] { "1231234567890", true };
            yield return new object[] { "+121234567890", true };
            yield return new object[] { "121234567890", true };
            yield return new object[] { "+11234567890", true };
            yield return new object[] { "11234567890", true };
            yield return new object[] { "1234567890", true };
            yield return new object[] { "123456789", false };
            yield return new object[] { "12345678", false };
            yield return new object[] { "1234567", false };
            yield return new object[] { "123456", false };
            yield return new object[] { "12345", false };
            yield return new object[] { "1234", false };
            yield return new object[] { "123", false };
            yield return new object[] { "12", false };
            yield return new object[] { "1", false };
            
            foreach (char c in dividerCharacters)
            {
                yield return new object[] { $"1 123{c}456{c}7890", true};
                yield return new object[] { $"+1 (123){c}456{c}7890", true};
                yield return new object[] { $"12 123{c}456{c}7890", true };
                yield return new object[] { $"+12 (123){c}456{c}7890", true };
                yield return new object[] { $"123 123{c}456{c}7890", true };
                yield return new object[] { $"+123 (123){c}456{c}7890", true };
                yield return new object[] { $"1123{c}456{c}7890", true };
                yield return new object[] { $"+1(123){c}456{c}7890", true };
                yield return new object[] { $"12123{c}456{c}7890", true };
                yield return new object[] { $"+12(123){c}456{c}7890", true };
                yield return new object[] { $"123123{c}456{c}7890", true };
                yield return new object[] { $"+123(123){c}456{c}7890", true };

                yield return new object[] { $"123{c}456{c}7890", true };
                yield return new object[] { $"(123)456{c}7890", true };
                yield return new object[] { $"123{c}456{c}7890{c}", false};
                yield return new object[] { $"12{c}345{c}6789", false };
                yield return new object[] { $"1{c}234{c}5678", false };
                yield return new object[] { $"123{c}4567", false };
                yield return new object[] { $"12{c}3456", false };
                yield return new object[] { $"1{c}2345", false };
                yield return new object[] { $"{c}1234", false };
                yield return new object[] { $"{c}123", false };
                yield return new object[] { $"{c}12", false };
                yield return new object[] { $"{c}1", false };
            }
        }

        [TestCaseSource(nameof(PhoneNumberData))]
        public void PhoneNumberTests(string input, bool expectedMatch)
        {
            Regex regex = new Regex(RegularExpressions.PhoneNumber);

            bool result = regex.IsMatch(input);

            result.Should().Be(expectedMatch);
        }

        [TestCase("Chris.Thompson@amtrustgroup.Com", true)]
        [TestCase("Chris.Thompson@amtrustgroup.com", true)]
        [TestCase("Chris.Thompson@amtrustgroup.COM", true)]
        [TestCase("Chris.Thompson@AmtrustGroup.com", true)]
        [TestCase("Chris.Thompson@AMTRUSTGROUP.com", true)]
        [TestCase("chris.thompson@amtrustgroup.com", true)]
        [TestCase("CHRIS.THOMPSON@amtrustgroup.com", true)]
        [TestCase("CHRIS.THOMPSON@AMTRUSTGROUP.COM", true)]
        [TestCase("CHRIS@AMTRUSTGROUP.COM", true)]
        [TestCase("CHRIS@AMTRUSTGROUP.com", true)]
        [TestCase("CHRIS@amtrustgroup.COM", true)]
        [TestCase("chris@AMTRUSTGROUP.COM", true)]
        [TestCase("chris@AMTRUSTGROUP.com", true)]
        [TestCase("chris@amtrustgroup.COM", true)]
        [TestCase("chris@amtrustgroup.", false)]
        [TestCase("chris@amtrustgroup", false)]
        [TestCase("chris@", false)]
        [TestCase("chris@.", false)]
        [TestCase("chris@.com", false)]
        [TestCase("@amtrustgroup.com", false)]
        [TestCase("@.com", false)]
        [TestCase("@amtrustgroup", false)]
        [TestCase("@amtrustgroup.", false)]
        public void EmailAddressTests(string input, bool expectedMatch)
        {
            Regex regex = new Regex(RegularExpressions.EmailAddress);

            bool result = regex.IsMatch(input);

            result.Should().Be(expectedMatch);
        }
    }
}