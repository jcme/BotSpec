﻿using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Attachments;
using KBrimble.DirectLineTester.Assertions.Messages;
using Microsoft.Bot.Connector.DirectLine.Models;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.MessageAssertionTests.When_testing_messages
{
    [TestFixture]
    public class For_attachments
    {
        [Test]
        public void HasAttachment_should_return_MessageAttachmentAssertions()
        {
            var message = new Message();

            var sut = new MessageAssertions(message);

            sut.HasAttachment().Should().BeAssignableTo<MessageAttachmentAssertions>().And.NotBeNull();
        }
    }
}