using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Template.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class DapperConnection
    {
        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public DapperConnection(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
    }
}
