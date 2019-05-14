using System;

namespace Core.Template.Infrastructure.Filters
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ErrorInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Details { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ValidationErrorInfo[] ValidationErrors { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ErrorInfo()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ErrorInfo(string message)
        {
            Message = message;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        public ErrorInfo(int code)
        {
            Code = code;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public ErrorInfo(int code, string message) : this(message)
        {
            Code = code;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="details"></param>
        public ErrorInfo(string message, string details) : this(message)
        {
            Details = details;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="details"></param>
        public ErrorInfo(int code, string message, string details) : this(message, details)
        {
            Code = code;
        }
    }
}
