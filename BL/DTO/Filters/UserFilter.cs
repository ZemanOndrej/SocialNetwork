using BL.DTO.GroupDTOs;
using Utils.Enums;

namespace BL.DTO.Filters
{
	public class UserFilter
	{
		public string Name { get; set; }

		public string Surname { get; set; }

		public Gender? Gender { get; set; }
		public string Email { get; set; }
		public string Login { get; set; }
		public string Fullname { get; set; }
		public GroupDTO Group { get; set; }


	}
}
