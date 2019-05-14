using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Template.Configuration
{
    /// <summary>
    /// 静态值
    /// </summary>
    public class StaticConst
    {
        /// <summary>
        /// 
        /// </summary>
        public const string UserNameNotNullAndEmpty = "用户名不能为空";
    }
    /// <summary>
    /// 
    /// </summary>
    public enum StaticConstSourceType
    {
        /// <summary>
        /// 
        /// </summary>
        Local,
        /// <summary>
        /// 
        /// </summary>
        Consul
    }
}
