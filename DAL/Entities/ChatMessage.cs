using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Entities
{
	public class ChatMessage : IEntity<int>
	{
		[Required]
		public string Message { get; set; }

		[Column(TypeName = "datetime2")]
		public DateTime Time { get; set; }

		[Required]
		public virtual User Sender { get; set; }


		[Required]
		public virtual Chat Chat { get; set; }

//		public bool Seen { get; set; }

		public int ID { get; set; }
	}
}