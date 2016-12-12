using System.Collections.Generic;
using BL.DTO.Filters;

namespace BL.DTO.UserDTOs
{
	public class UserListQueryResultDTO
	{
		public UserFilter Filter { get; set; }

		public int ResultCount { get; set; }

		public IEnumerable<AccountDTO> ResultUsers { get; set; }

		public int RequestedPage { get; set; }
	}
}