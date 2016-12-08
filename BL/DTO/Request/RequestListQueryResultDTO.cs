using System.Collections.Generic;
using BL.DTO.Filters;

namespace BL.DTO.Request
{
	public class RequestListQueryResultDTO
	{
		public RequestFilter Filter { get; set; }

		public int ResultCount { get; set; }

		public IEnumerable<RequestDTO> ResultRequests { get; set; }

		public int RequestedPage { get; set; }
	}
}