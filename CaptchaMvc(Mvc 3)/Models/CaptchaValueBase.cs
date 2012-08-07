﻿using System;
using CaptchaMvc.Interface;

namespace CaptchaMvc.Models
{
    /// <summary>
    /// Represents the base model for storing ​​captcha values.
    /// </summary>
    [Serializable]
    public abstract class CaptchaValueBase : ICaptchaValue
    {
        #region Implementation of ICaptchaValue

        /// <summary>
        /// Gets the specified captcha text.
        /// </summary>
        public abstract string CaptchaText { get; }

        /// <summary>
        /// Gets the specified captcha value.
        /// </summary>
        public abstract object Value { get; }

        /// <summary>
        /// Serializes the <see cref="ICaptchaValue"/>, to the given string.
        /// </summary>
        /// <returns>The result string.</returns>
        public string Serialize()
        {
            return string.Format("~[{0}]~|-|-|~[{1}]~", CaptchaText, Value);
        }

        /// <summary>
        /// Deserializes the specified <see cref="string"/> into an <see cref="ICaptchaValue"/>.
        /// </summary>
        /// <param name="serializeState">The specified serialize state.</param>
        public void Deserialize(string serializeState)
        {
            if (serializeState == null)
                throw new ArgumentNullException("serializeState");
            string[] strings = serializeState.Split(new[] {"|-|-|"}, StringSplitOptions.RemoveEmptyEntries);
            if (strings.Length != 2)
                throw new ArgumentException(
                    string.Format("It is not possible to deserialize an object from a string '{0}'.", serializeState));
            string text = strings[0].Replace("~[", string.Empty).Replace("]~", string.Empty);
            string value = strings[1].Replace("~[", string.Empty).Replace("]~", string.Empty);
            DeserializeInternal(text, value);
        }

        /// <summary>
        /// Determines whether the current captcha value is equal for the <c>inputText</c>.
        /// </summary>
        /// <param name="inputText">The specified input text.</param>
        /// <returns><c>True</c> if the value is equals; otherwise, <c>false</c>.</returns>
        public abstract bool IsEqual(string inputText);

        #endregion

        #region Method

        /// <summary>
        /// Deserializes the specified values into an <see cref="ICaptchaValue"/>.
        /// </summary>
        /// <param name="captchaText">The specified captcha text.</param>
        /// <param name="value">The specified captcha value.</param>
        protected abstract void DeserializeInternal(string captchaText, string value);

        #endregion
    }
}