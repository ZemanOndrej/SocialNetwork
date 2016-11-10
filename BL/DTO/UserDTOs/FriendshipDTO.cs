using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace BL.DTO.UserDTOs
{
	public class FriendshipDTO
	{

		public int ID { get; set; }

		public int User1Id { get; set; }

		public int User2Id { get; set; }

	}
}
