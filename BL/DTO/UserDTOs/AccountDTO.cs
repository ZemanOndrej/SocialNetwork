using System;
using Utils.Enums;

namespace BL.DTO.UserDTOs
{
	public class AccountDTO
	{
		

		public int ID { get; set; }


		#region userInfo

		public string Password { get; set; }
		public string Email { get; set; }
		public string UserName { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string FullName => Name + " " + Surname;


		public string Information { get; set; }
		public DateTime DateOfBirth { get; set; }
	
		public byte[] ProfilePicture { get; set; }

		public Gender Gender { get; set; }
		public PostPrivacyLevel DefaultPostPrivacy { get; set; } = PostPrivacyLevel.OnlyFriends;
		public FriendListVisibilityLevel FriendListVisibility { get; set; } = FriendListVisibilityLevel.Public;


		

		#endregion

		#region Methods

		public override string ToString()
		{
			return $" {nameof(FullName)}: {FullName}, {nameof(Gender)}: {Gender},\n {nameof(Information)}: {Information},\n {nameof(DateOfBirth)}: {DateOfBirth} ";
		}

		protected bool Equals(AccountDTO other)
		{
			return ID==other.ID;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && Equals((AccountDTO) obj);
		}

		public override int GetHashCode()
		{
			return Email?.GetHashCode() ?? 0;
		}


		#endregion

	}

	
}
