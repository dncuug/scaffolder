using System;

namespace Scaffolder.Core.Meta
{
    public class Reference
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public String Table { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public String KeyColumn { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public String TextColumn { get; set; }

        public String GetColumnAlias()
        {
            return $"{Table}_{TextColumn}";
        }
    }
}
