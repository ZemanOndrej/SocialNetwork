using System.Collections.Generic;
using BL.DTO.Filters;

namespace BL.DTO.PostDTOs
{
	public class ReactionListQueryResultDTO
	{
		public ReactionFilter Filter { get; set; }

		public int ResultCount { get; set; }

		public IEnumerable<ReactionDTO> ResultReactions { get; set; }

		public int RequestedPage { get; set; }
	}
}