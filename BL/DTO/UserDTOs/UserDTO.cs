using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO.UserDTOs;
using Utils.Enums;

namespace BL.DTO
{
	public class UserDTO
	{
		

		public int ID { get; set; }

		#region userInfo
		public Gender Gender { get; set; }
		public string Email { get; set; }

		public string Login { get; set; }


		public string Name { get; set; }


		public string Surname { get; set; }

		public string FullName => Name + " " + Surname;


		public string Information { get; set; }


		public DateTime DateOfBirth { get; set; }

		public byte[] ProfilePicture { get; set; }

		public List<FriendshipDTO> FriendList { get; set; }


		#endregion

		#region Methods

		public override string ToString()
		{
			return $"{nameof(Login)}: {Login}, {nameof(FullName)}: {FullName}, {nameof(Email)}: {Email}\n, {nameof(Gender)}: {Gender}, {nameof(Information)}: {Information},\n {nameof(DateOfBirth)}: {DateOfBirth} ";
		}

		protected bool Equals(UserDTO other)
		{
			return string.Equals(Email, other.Email) && string.Equals(Login, other.Login);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && Equals((UserDTO) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Email?.GetHashCode() ?? 0)*397) ^ (Login?.GetHashCode() ?? 0);
			}
		}
		public UserDTO()
		{
			FriendList = new List<FriendshipDTO>();
		}

		#endregion

	}

	
}
