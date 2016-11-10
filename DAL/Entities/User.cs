using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Game;
using Riganti.Utils.Infrastructure.Core;
using Utils.Enums;

namespace DAL.Entities
{
	public class User : IEntity<int>
	{

		#region UserInfo		
		public int ID { get; set; }

		public virtual List<Group> Groups { get; set; }
		public virtual List<Post> Posts { get; set; }
		public virtual List<Friendship> FriendList { get; set; }
		public virtual List<Reaction> LikedList { get; set; }
		public virtual List<Chat> Chats { get; set; }

		[MaxLength(100)]
		[Required]
		[Index(IsUnique = true)]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[MaxLength(100)]
		[Required]
		[Index(IsUnique = true)]
		public string Login { get; set; }

		[MaxLength(100)]
		[Required]
		public string Name { get; set; }

		[MaxLength(100)]
		[Required]
		public string Surname { get; set; }

		public string Information { get; set; }

		[Required]
		[Column(TypeName = "datetime2")]
		public DateTime DateOfBirth { get; set; }

		public byte[] ProfilePicture { get; set; }

		public Gender Gender { get; set; }

		#endregion




		#region UserSettings

		[DefaultValue(PostPrivacyLevel.OnlyFriends)]
		public PostPrivacyLevel DefaultPostPrivacyLevel { get; set; }



		#endregion

		#region Methods

		public User()
		{
			Groups = new List<Group>();
			Posts = new List<Post>();
			FriendList = new List<Friendship>();
			LikedList = new List<Reaction>();
			Chats = new List<Chat>();
		}

		private bool Equals(User other)
		{
			return string.Equals(Email, other.Email) && string.Equals(Login, other.Login);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && Equals((User) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Email?.GetHashCode() ?? 0)*397) ^ (Login?.GetHashCode() ?? 0);
			}
		}

		

		#endregion
		
	}
}