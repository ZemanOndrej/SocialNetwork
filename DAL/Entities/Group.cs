using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Entities
{
	public class Group : IEntity<int>
	{
		public Group()
		{
			Accounts = new List<User>();
			GroupPosts = new List<Post>();
		}

		[MaxLength(100)]
		[Required]
		public string Name { get; set; }

		[Column(TypeName = "datetime2")]
		public DateTime DateCreated { get; set; }

		public virtual List<User> Accounts { get; set; }
		public virtual List<Post> GroupPosts { get; set; }


		protected bool Equals(Group other)
		{
			return ID == other.ID;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && Equals((Group) obj);
		}

		public override int GetHashCode()
		{
			return ID;
		}

		public int ID { get; set; }
	}
}