using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Riganti.Utils.Infrastructure.Core;
using Utils.Enums;

namespace DAL.Entities
{
	public class Post : IEntity<int>
	{
		public Post()
		{
			Comments = new List<Comment>();
			Reactions = new List<Reaction>();
		}


		public PostPrivacyLevel PrivacyLevel { get; set; } = PostPrivacyLevel.OnlyFriends;

		[Required]
		public string Message { get; set; }

		[Required]
		public virtual Account Sender { get; set; }

		[Column(TypeName = "datetime2")]
		public DateTime Time { get; set; }

		public virtual Group Group { get; set; }

		public virtual List<Comment> Comments { get; set; }
		public virtual List<Reaction> Reactions { get; set; }


		public int ID { get; set; }
	}
}