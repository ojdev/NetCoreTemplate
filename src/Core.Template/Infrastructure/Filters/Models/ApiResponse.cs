using Newtonsoft.Json;
using System;

namespace Core.Template.Infrastructure.Filters
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ApiResponse<TResult>
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("__abp")]
        public bool Abp { set; get; } = true;
        /// <summary>
        /// 
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TResult Result { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ErrorInfo Error { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool UnAuthorizedRequest { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ApiResponse()
        {
            Success = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        public ApiResponse(bool success)
        {
            Success = success;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        public ApiResponse(TResult result) : this()
        {
            Result = result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        /// <param name="unAuthorizedRequest"></param>
        public ApiResponse(ErrorInfo error, bool unAuthorizedRequest = false)
        {
            Error = error;
            UnAuthorizedRequest = unAuthorizedRequest;
            Success = false;
        }
    }
}
