using System;
using System.Runtime.Serialization;

namespace Core.Template.Domain.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class DomainException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public DomainException()
        { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public DomainException(string message)
            : base(message)
        { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public DomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
