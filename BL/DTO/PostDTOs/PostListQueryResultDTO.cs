using System.Collections.Generic;
using BL.DTO.Filters;

namespace BL.DTO.PostDTOs
{
	public class PostListQueryResultDTO
	{
		public PostFilter Filter { get; set; }

		public int ResultCount { get; set; }

		public IEnumerable<PostDTO> ResultPosts { get; set; }

		public int RequestedPage { get; set; }
	}
}