using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Entities
{
	public class Request : IEntity<int>
	{
		[Required]
		public virtual Account Sender { get; set; }

		[Required]
		public virtual Account Receiver { get; set; }

		public virtual Group Group { get; set; }

		[Column(TypeName = "datetime2")]
		public DateTime Time { get; set; }

		public int ID { get; set; }
	}
}