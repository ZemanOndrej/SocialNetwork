using System.Collections.Generic;
using BL.DTO.Filters;

namespace BL.DTO.UserDTOs
{
	public class FriendListQueryResultDTO
	{
		public FriendshipFilter Filter { get; set; }

		public int ResultCount { get; set; }

		public IEnumerable<FriendshipDTO> ResultFriendships { get; set; }

		public int RequestedPage { get; set; }
	}
}