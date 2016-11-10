﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO.Filters;

namespace BL.DTO.GroupDTOs
{
	public class GroupListQueryResultDTO
	{
		public GroupFilter Filter { get; set; }

		public int ResultCount { get; set; }

		public IEnumerable<GroupDTO> ResultGroups { get; set; }

		public int RequestedPage { get; set; }
	}
}
