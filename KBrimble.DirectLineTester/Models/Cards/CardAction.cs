﻿namespace KBrimble.DirectLineTester.Models.Cards
{
    /// <summary>
    /// An action on a card
    /// </summary>
    internal class CardAction
    {
        /// <summary>
        /// Initializes a new instance of the CardAction class.
        /// </summary>
        public CardAction() { }

        /// <summary>
        /// Initializes a new instance of the CardAction class.
        /// </summary>
        public CardAction(string type = default(string), string title = default(string), string image = default(string), string value = default(string))
        {
            Type = type;
            Title = title;
            Image = image;
            Value = value;
        }

        /// <summary>
        /// Defines the type of action implemented by this button.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Text description which appear on the button.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// URL Picture which will appear on the button, next to text label.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Supplementary parameter for action. Content of this property
        /// depends on the ActionType
        /// </summary>
        public string Value { get; set; }

    }
}
