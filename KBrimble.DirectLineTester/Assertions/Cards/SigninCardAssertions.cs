﻿using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public class SigninCardAssertions : ISigninCardAssertions
    {
        private readonly SigninCard _signinCard;
        private readonly StringHelpers<SigninCardAssertionFailedException> _stringHelpers;

        public SigninCardAssertions(SigninCard signinCard)
        {
            _signinCard = signinCard;
            _stringHelpers = new StringHelpers<SigninCardAssertionFailedException>();
        }

        public ISigninCardAssertions TextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_signinCard.Text, regex, CreateEx(nameof(_signinCard.Text), regex));

            return this;
        }

        public ISigninCardAssertions TextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_signinCard.Text, regex, groupMatchRegex, CreateEx(nameof(_signinCard.Text), regex));

            return this;
        }

        public ICardActionAssertions WithButtons()
        {
            return new CardActionSetAssertions(_signinCard.Buttons);
        }

        public Func<SigninCardAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected Signin to have property {testedProperty} matching regex {regex} but did not";
            return () => new SigninCardAssertionFailedException(message);
        }
    }
}
