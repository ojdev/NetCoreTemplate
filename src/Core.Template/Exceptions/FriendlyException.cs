using System;

namespace Core.Template.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class FriendlyException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public FriendlyException()
        { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public FriendlyException(string message)
            : base(message)
        { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public FriendlyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
