using System;
using System.Collections.Generic;
using BotSpec.Assertions.Cards.CardComponents;
using BotSpec.Exceptions;
using Microsoft.Bot.Connector.DirectLine;

namespace BotSpec.Assertions.Cards
{
    internal class ThumbnailCardAssertions : IThumbnailCardAssertions, IThrow<ThumbnailCardAssertionFailedException>
    {
        private readonly ThumbnailCard _thumbnailCard;
        private readonly StringHelpers<ThumbnailCardAssertionFailedException> _stringHelpers;

        public ThumbnailCardAssertions(ThumbnailCard thumbnailCard)
        {
            _thumbnailCard = thumbnailCard;
            _stringHelpers = new StringHelpers<ThumbnailCardAssertionFailedException>();
        }

        public IThumbnailCardAssertions TitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_thumbnailCard.Title, regex, CreateEx(nameof(ThumbnailCard.Title), regex));

            return this;
        }

        public IThumbnailCardAssertions TitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_thumbnailCard.Title, regex, groupMatchRegex, CreateEx(nameof(ThumbnailCard.Title), regex));

            return this;
        }

        public IThumbnailCardAssertions SubtitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_thumbnailCard.Subtitle, regex, CreateEx(nameof(ThumbnailCard.Subtitle), regex));

            return this;
        }

        public IThumbnailCardAssertions SubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_thumbnailCard.Subtitle, regex, groupMatchRegex, CreateEx(nameof(ThumbnailCard.Subtitle), regex));
            return this;
        }

        public IThumbnailCardAssertions TextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_thumbnailCard.Text, regex, CreateEx(nameof(ThumbnailCard.Text), regex));

            return this;
        }

        public IThumbnailCardAssertions TextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_thumbnailCard.Text, regex, groupMatchRegex, CreateEx(nameof(ThumbnailCard.Text), regex));

            return this;
        }

        public ICardImageAssertions WithCardImage()
        {
            return new CardImageSetAssertions(_thumbnailCard.Images);
        }

        public ICardActionAssertions WithButtons()
        {
            return new CardActionSetAssertions(_thumbnailCard.Buttons);
        }

        public ICardActionAssertions WithTapAction()
        {
            return new CardActionAssertions(_thumbnailCard.Tap);
        }

        public Func<ThumbnailCardAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected thumbnail card to have property {testedProperty} to match {regex} but regex test failed.";
            return () => new ThumbnailCardAssertionFailedException(message);
        }
    }
}