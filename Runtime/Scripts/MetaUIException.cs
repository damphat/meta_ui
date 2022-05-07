#region using

using System;

#endregion

namespace MetaUI
{
    public class MetaUIException : Exception
    {
        public MetaUIException()
        {
        }

        public MetaUIException(string message)
            : base(message)
        {
        }

        public MetaUIException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}