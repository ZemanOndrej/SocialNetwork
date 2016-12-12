using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Identity;
using Riganti.Utils.Infrastructure.Core;
using Utils.Enums;

namespace DAL.Entities
{
	public class Account : IEntity<int>
	{
		#region UserSettings

		[DefaultValue(PostPrivacyLevel.OnlyFriends)]
		public PostPrivacyLevel DefaultPostPrivacyLevel { get; set; }

		#endregion

		#region UserInfo		

		public int ID { get; set; }

		public virtual List<Group> Groups { get; set; }
		public virtual List<Post> Posts { get; set; }
//		public virtual List<Friendship> FriendList { get; set; }
		public virtual List<Reaction> LikedList { get; set; }
		public virtual List<Chat> Chats { get; set; }
//		public virtual List<Request> Requests { get; set; }
		[Required]
		public virtual User User { get; set; }


		[MaxLength(100)]
		public string Name { get; set; }

		[MaxLength(100)]
		public string Surname { get; set; }

		public string Information { get; set; }

		[Column(TypeName = "datetime2")]
		public DateTime DateOfBirth { get; set; }

		public byte[] ProfilePicture { get; set; }

		public Gender Gender { get; set; }

		public PostPrivacyLevel DefaultPostPrivacy { get; set; } = PostPrivacyLevel.OnlyFriends;
		public FriendListVisibilityLevel FriendListVisibility { get; set; } = FriendListVisibilityLevel.Public;

		#endregion

		#region Methods

		public Account()
		{
			Groups = new List<Group>();
			Posts = new List<Post>();
//			FriendList = new List<Friendship>();
			LikedList = new List<Reaction>();
			Chats = new List<Chat>();
//			Requests= new List<Request>();
		}

		private bool Equals(Account other)
		{
			return other.User.Equals(User);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return (obj.GetType() == GetType()) && Equals((Account) obj);
		}

		public override int GetHashCode()
		{
			return User.GetHashCode();
		}

		#endregion
	}
}