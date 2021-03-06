using System;
using System.Collections.Generic;
using System.Linq;
using BotSpec.Exceptions;
using Microsoft.Bot.Connector.DirectLine;

namespace BotSpec.Assertions.Cards.CardComponents
{
    internal class FactSetAssertions : IFactAssertions, IThrow<FactAssertionFailedException>
    {
        public readonly IEnumerable<Fact> Facts;
        private readonly SetHelpers<Fact, FactAssertionFailedException> _setHelper;

        public FactSetAssertions(IEnumerable<Fact> facts) : this()
        {
            if (facts == null)
                throw new ArgumentNullException(nameof(facts));
            Facts = facts.Where(fact => fact != null).ToList();
        }

        public FactSetAssertions(ReceiptCard receiptCard)
        {
            if (receiptCard?.Facts == null)
                throw new ArgumentNullException(nameof(receiptCard.Facts));

            Facts = receiptCard.Facts.Where(fact => fact != null).ToList();
        }

        public FactSetAssertions(IEnumerable<ReceiptCard> receiptCards) : this()
        {
            if (receiptCards == null)
                throw new ArgumentNullException(nameof(receiptCards));

            Facts = receiptCards.Where(x => x != null).SelectMany(x => x.Facts).Where(x => x != null).ToList();
        }

        private FactSetAssertions()
        {
            _setHelper = new SetHelpers<Fact, FactAssertionFailedException>();
        }

        public IFactAssertions KeyMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelper.TestSetForMatch(Facts, fact => fact.That().KeyMatching(regex), CreateEx(nameof(Fact.Key), regex));

            return this;
        }

        public IFactAssertions KeyMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<Fact, FactAssertionFailedException>.TestWithGroups act =
                (Fact fact, out IList<string> matches) => fact.That().KeyMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelper.TestSetForMatchAndReturnGroups(Facts, act, CreateEx(nameof(Fact.Key), regex));

            return this;
        }

        public IFactAssertions ValueMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelper.TestSetForMatch(Facts, fact => fact.That().ValueMatching(regex), CreateEx(nameof(Fact.Key), regex));

            return this;
        }

        public IFactAssertions ValueMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<Fact, FactAssertionFailedException>.TestWithGroups act =
                (Fact fact, out IList<string> matches) => fact.That().ValueMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelper.TestSetForMatchAndReturnGroups(Facts, act, CreateEx(nameof(Fact.Key), regex));

            return this;

        }

        public Func<FactAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected property {testedProperty} to match {regex} but did not";
            return () => new FactAssertionFailedException(message);
        }
    }
}