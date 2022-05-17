#region using

using System;

#endregion

namespace MetaUI
{
    public class MetaException : Exception
    {
        public MetaException()
        {
        }

        public MetaException(string message)
            : base(message)
        {
        }

        public MetaException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}