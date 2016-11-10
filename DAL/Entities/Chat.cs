using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Entities
{
	public class Chat : IEntity<int>
	{
		public Chat()
		{
			ChatUsers = new List<User>();
			Messages = new List<ChatMessage>();
		}

		public virtual List<User> ChatUsers { get; set; }

		public virtual List<ChatMessage> Messages { get; set; }

		[MaxLength(100)]
		public string Name { get; set; }


		public int ID { get; set; }

	}
}