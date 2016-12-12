using System.Collections.Generic;
using BL.DTO.Filters;

namespace BL.DTO.PostDTOs
{
	public class CommentListQueryResultDTO
	{
		public CommentFilter Filter { get; set; }

		public int ResultCount { get; set; }

		public IEnumerable<CommentDTO> ResultComments { get; set; }

		public int RequestedPage { get; set; }
	}
}