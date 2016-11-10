using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO.Filters;

namespace BL.DTO
{
	public class UserListQueryResultDTO
	{
		public UserFilter Filter { get; set; }

		public int ResultCount { get; set; }

		public IEnumerable<UserDTO> ResultUsers { get; set; }

		public int RequestedPage { get; set; }



	}
}
