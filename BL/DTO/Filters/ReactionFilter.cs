using BL.DTO.PostDTOs;
using BL.DTO.UserDTOs;

namespace BL.DTO.Filters
{
	public class ReactionFilter
	{
		public PostDTO Post { get; set; }

		public AccountDTO Account { get; set; }
	}
}