﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Messages;
using KBrimble.DirectLineTester.Exceptions;
using Microsoft.Bot.Connector.DirectLine.Models;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.MessageAssertionTests.When_testing_message_sets
{
    [TestFixture]
    public class For_from_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void HaveFromMatching_should_pass_if_regex_exactly_matches_message_From_of_one_message(string fromAndRegex)
        {
            var messages = MessageTestData.CreateMessageSetWithOneMessageThatHasSetProperties(fromProperty: fromAndRegex);

            var sut = new MessageSetAssertions(messages);

            Action act = () => sut.HaveFromMatching(fromAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void HaveFromMatching_should_pass_if_regex_exactly_matches_From_of_at_least_1_message_regardless_of_case(string text, string regex)
        {
            var messages = MessageTestData.CreateMessageSetWithOneMessageThatHasSetProperties(fromProperty: text);

            var sut = new MessageSetAssertions(messages);

            Action act = () => sut.HaveFromMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void HaveFromMatching_should_pass_when_using_standard_regex_features(string text, string regex)
        {
            var messages = MessageTestData.CreateMessageSetWithOneMessageThatHasSetProperties(fromProperty: text);

            var sut = new MessageSetAssertions(messages);

            Action act = () => sut.HaveFromMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text!")]
        [TestCase("^[j-z ]*$")]
        [TestCase("s{12}")]
        public void HaveFromMatching_should_throw_MessageSetAssertionFailedException_when_regex_matches_no_messages(string regex)
        {
            var messages = MessageTestData.CreateRandomMessages();

            var sut = new MessageSetAssertions(messages);

            Action act = () => sut.HaveFromMatching(regex);

            act.ShouldThrow<MessageSetAssertionFailedException>();
        }

        [Test]
        public void HaveFromMatching_should_throw_MessageSetAssertionFailedException_when_From_of_all_messages_is_null()
        {
            var messages = Enumerable.Range(1, 5).Select(_ => new Message()).ToList();

            var sut = new MessageSetAssertions(messages);

            Action act = () => sut.HaveFromMatching(".*");

            act.ShouldThrow<MessageSetAssertionFailedException>();
        }

        [Test]
        public void HaveFromMatching_should_throw_MessageSetAssertionFailedException_when_trying_to_capture_groups_but_From_of_all_messages_is_null()
        {
            IList<string> matches;

            var messages = Enumerable.Range(1, 5).Select(_ => new Message()).ToList();

            var sut = new MessageSetAssertions(messages);

            Action act = () => sut.HaveFromMatching(".*", "(.*)", out matches);

            act.ShouldThrow<MessageSetAssertionFailedException>();
        }

        [Test]
        public void HaveFromMatching_should_not_output_matches_when_regex_does_not_match_From_of_any_messages()
        {
            IList<string> matches = null;

            var messages = MessageTestData.CreateRandomMessages();

            var sut = new MessageSetAssertions(messages);

            Action act = () => sut.HaveFromMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<MessageSetAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void HaveFromMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_From_of_any_message()
        {
            IList<string> matches;

            var messages = MessageTestData.CreateRandomMessages();

            var sut = new MessageSetAssertions(messages);

            sut.HaveFromMatching(".*", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void HaveFromMatching_should_output_matches_when_groupMatchingRegex_matches_From_of_any_message()
        {
            IList<string> matches;

            const string someFrom = "some text";
            var messages = MessageTestData.CreateMessageSetWithOneMessageThatHasSetProperties(fromProperty: someFrom);

            var sut = new MessageSetAssertions(messages);

            sut.HaveFromMatching(someFrom, $"({someFrom})", out matches);

            matches.First().Should().Be(someFrom);
        }

        [Test]
        public void HaveFromMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_From_several_times_for_a_single_message()
        {
            IList<string> matches;

            const string someFrom = "some text";
            var messages = MessageTestData.CreateMessageSetWithOneMessageThatHasSetProperties(fromProperty: someFrom);

            var sut = new MessageSetAssertions(messages);

            const string match1 = "some";
            const string match2 = "text";
            sut.HaveFromMatching(someFrom, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void HaveFromMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_From_on_multiple_messages()
        {
            IList<string> matches;

            var messages = MessageTestData.CreateRandomMessages();
            messages.Add(new Message(fromProperty: "some text"));
            messages.Add(new Message(fromProperty: "same text"));

            var sut = new MessageSetAssertions(messages);

            sut.HaveFromMatching(".*", @"(s[oa]me) (text)", out matches);

            matches.Should().Contain("some", "same", "text");
        }

        [Test]
        public void HaveFromMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var messages = MessageTestData.CreateRandomMessages();

            var sut = new MessageSetAssertions(messages);

            Action act = () => sut.HaveFromMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void HaveFromMatching_should_throw_ArgumentNullException_when_capturing_groups_if_regex_is_null()
        {
            IList<string> matches;

            var messages = MessageTestData.CreateRandomMessages();

            var sut = new MessageSetAssertions(messages);

            Action act = () => sut.HaveFromMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void HaveFromMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var messages = MessageTestData.CreateRandomMessages();

            var sut = new MessageSetAssertions(messages);

            Action act = () => sut.HaveFromMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}