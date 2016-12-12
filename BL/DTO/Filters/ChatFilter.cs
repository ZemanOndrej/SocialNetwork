using BL.DTO.UserDTOs;

namespace BL.DTO.Filters
{
	public class ChatFilter
	{
		public string Name { get; set; }

		public AccountDTO Account { get; set; }
	}
}