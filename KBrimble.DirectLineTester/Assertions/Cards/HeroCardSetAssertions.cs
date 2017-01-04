using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Attachments;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public class HeroCardSetAssertions : IHeroCardAssertions
    {
        public readonly IEnumerable<HeroCard> HeroCards;
        private readonly SetHelpers<HeroCard, HeroCardAssertionFailedException> _setHelpers;

        public HeroCardSetAssertions(MessageSet messageSet) : this()
        {
            var attachmentExtractor = AttachmentExtractorFactory.GetAttachmentExtractor();
            HeroCards = attachmentExtractor.ExtractCards<HeroCard>(messageSet);
        }

        public HeroCardSetAssertions(IEnumerable<Message> messageSet) : this()
        {
            var attachmentExtractor = AttachmentExtractorFactory.GetAttachmentExtractor();
            HeroCards = attachmentExtractor.ExtractCards<HeroCard>(messageSet);
        }

        public HeroCardSetAssertions(Message message) : this()
        {
            var attachmentExtractor = AttachmentExtractorFactory.GetAttachmentExtractor();
            HeroCards = attachmentExtractor.ExtractCards<HeroCard>(message);
        }

        public HeroCardSetAssertions(IEnumerable<HeroCard> heroCards) : this()
        {
            HeroCards = heroCards;
        }

        private HeroCardSetAssertions()
        {
            _setHelpers = new SetHelpers<HeroCard, HeroCardAssertionFailedException>();
        }

        public IHeroCardAssertions SubtitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(HeroCards, card => card.That().SubtitleMatching(regex), CreateEx(nameof(HeroCard.Subtitle), regex));

            return this;
        }

        public IHeroCardAssertions SubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<HeroCard, HeroCardAssertionFailedException>.TestWithGroups act
                = (HeroCard card, out IList<string> matches) => card.That().SubtitleMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(HeroCards, act, CreateEx(nameof(HeroCard.Subtitle), regex));

            return this;
        }

        public IHeroCardAssertions TextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(HeroCards, card => card.That().TextMatching(regex), CreateEx(nameof(HeroCard.Text), regex));

            return this;
        }

        public IHeroCardAssertions TextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<HeroCard, HeroCardAssertionFailedException>.TestWithGroups act
                = (HeroCard card, out IList<string> matches) => card.That().TextMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(HeroCards, act, CreateEx(nameof(HeroCard.Text), regex));

            return this;
        }

        public IHeroCardAssertions TitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(HeroCards, card => card.That().TitleMatching(regex), CreateEx(nameof(HeroCard.Title), regex));

            return this;
        }

        public IHeroCardAssertions TitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<HeroCard, HeroCardAssertionFailedException>.TestWithGroups act
                = (HeroCard card, out IList<string> matches) => card.That().TitleMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(HeroCards, act, CreateEx(nameof(HeroCard.Title), regex));

            return this;
        }

        public Func<HeroCardAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected at least one hero card in set to have property {testedProperty} to match {regex} but none did.";
            return () => new HeroCardAssertionFailedException(message);
        }

        public ICardImageAssertions WithCardImage()
        {
            return new CardImageSetAssertions(HeroCards);
        }

        public ICardActionAssertions WithButtons()
        {
            return new CardActionSetAssertions(HeroCards as IEnumerable<IHaveButtons>);
        }

        public ICardActionAssertions WithTapAction()
        {
            return new CardActionSetAssertions(HeroCards as IEnumerable<IHaveTapAction>);
        }
    }
}