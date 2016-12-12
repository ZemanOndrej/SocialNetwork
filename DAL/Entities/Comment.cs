using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Entities
{
	public class Comment : IEntity<int>
	{
		[Required]
		public string CommentMessage { get; set; }

		[Column(TypeName = "datetime2")]
		public DateTime Time { get; set; }

		[Required]
		public virtual Account Sender { get; set; }

		[Required]
		public virtual Post Post { get; set; }

//		public virtual List<Reaction> Reactions { get; set; }


		public int ID { get; set; }
	}
}