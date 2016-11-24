using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.DTO;
using BL.DTO.GroupDTOs;

namespace PL.Models
{
	public class GroupPageModel
	{
		public GroupDTO Group { get; set; }
		public List<PostDTO> Posts { get; set; }
		public PostDTO NewPost { get; set; }
//		public int GroupId { get; set; }
	}
}