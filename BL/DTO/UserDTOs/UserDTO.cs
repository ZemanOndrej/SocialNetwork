namespace BL.DTO.UserDTOs
{
	public class UserDTO
	{
		public int Id { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string UserName { get; set; }
		public AccountDTO Account { get; set; }
	}
}