using System;

namespace X.Scaffolding.Core
{
    public static class Paging
    {
        /// <summary>
        /// Items per page
        /// </summary>
        public static int PageSize { get; set; }

        static Paging()
        {
            PageSize = 15;
        }
    }
}
