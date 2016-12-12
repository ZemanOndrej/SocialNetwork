using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Riganti.Utils.Infrastructure.Core;
using Utils.Enums;

namespace DAL.Entities
{
	public class Group : IEntity<int>
	{
		public Group()
		{
			Accounts = new List<Account>();
			GroupPosts = new List<Post>();
			Requests = new List<Request>();
		}

		[MaxLength(100)]
		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

		[Column(TypeName = "datetime2")]
		public DateTime DateCreated { get; set; }

		public GroupPrivacyLevel GroupPrivacyLevel { get; set; }

		public virtual List<Account> Accounts { get; set; }
		public virtual List<Post> GroupPosts { get; set; }
		public virtual List<Request> Requests { get; set; }

		public int ID { get; set; }


		protected bool Equals(Group other)
		{
			return ID == other.ID;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return (obj.GetType() == GetType()) && Equals((Group) obj);
		}

		public override int GetHashCode()
		{
			return ID;
		}
	}
}