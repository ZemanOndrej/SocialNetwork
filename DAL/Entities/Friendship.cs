using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Entities
{
	public class Friendship : IEntity<int>
	{
		public int ID { get; set; }

		[Required]
		public virtual User User1 { get; set; }

		[Required]
		public virtual User User2 { get; set; }

		protected bool Equals(Friendship other)
		{
			return (User1.Equals(other.User1) && User2.Equals(other.User2)) ||
			       (User2.Equals(other.User1) && User1.Equals(other.User2));
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return (obj.GetType() == GetType() || obj.GetType()== ObjectContext.GetObjectType(GetType())) && Equals((Friendship) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 23;
				hash = hash * 31 + User1.GetHashCode();
				hash = hash * 31 + User2.GetHashCode();
				return hash;
			}
		}


		public Friendship(User user1, User user2)
		{
			if (user1 == null || user2 == null)return;
			if (user1.Equals(user2)) return;

			User1 = user1;
			User2 = user2;
		}

		public Friendship(){}
	}
}
