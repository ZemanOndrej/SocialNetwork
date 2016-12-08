using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Identity;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Entities
{
	public class Request : IEntity<int>
	{
		public int ID { get; set; }

		[Required]
		public virtual Account Sender { get; set; }

		[Required]
		public virtual Account Receiver { get; set; }

		public virtual Group Group { get; set; }

		[Column(TypeName = "datetime2")]
		public DateTime Time { get; set; }

	}
}