using System;

namespace Core.Template.Infrastructure.Filters
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ValidationErrorInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string[] Members { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ValidationErrorInfo()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ValidationErrorInfo(string message) => Message = message;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="members"></param>
        public ValidationErrorInfo(string message, string[] members) : this(message) => Members = members;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="member"></param>
        public ValidationErrorInfo(string message, string member) : this(message, new[] { member }) { }
    }
}
