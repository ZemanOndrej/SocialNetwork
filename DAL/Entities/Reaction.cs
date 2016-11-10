using System.ComponentModel.DataAnnotations;
using Riganti.Utils.Infrastructure.Core;
using Utils.Enums;

namespace DAL.Entities
{
	public class Reaction : IEntity<int>
	{
		[Required]
		public virtual Post Post { get; set; }

		[Required]
		public virtual User User { get; set; }
		
		[Required]
		public ReactionEnum UserReaction { get; set; }


		public int ID { get; set; }
	}
}