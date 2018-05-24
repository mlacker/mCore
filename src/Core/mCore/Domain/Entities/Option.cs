using System;

namespace mCore.Domain.Entities
{
    public class Option
    {
        public Option(string key) : this(key, key)
        {
        }

        public Option(string key, string text)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            Key = key;
            Text = text ?? key;
        }

        public string Key { get; private set; }

        public string Text { get; private set; }
    }
}
