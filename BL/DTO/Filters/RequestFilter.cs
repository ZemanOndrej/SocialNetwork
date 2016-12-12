using BL.DTO.GroupDTOs;
using BL.DTO.UserDTOs;

namespace BL.DTO.Filters
{
	public class RequestFilter
	{
		public AccountDTO Sender { get; set; }

		public GroupDTO Group { get; set; }

		public AccountDTO Receiver { get; set; }
	}
}