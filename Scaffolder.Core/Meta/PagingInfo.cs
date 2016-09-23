using System.Collections.Generic;

namespace Scaffolder.Core.Meta
{
	public class PagingInfo
	{
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
		public int TotalItemsCount { get; set; }
		public IEnumerable<dynamic> Items { get; set; }
	}
}