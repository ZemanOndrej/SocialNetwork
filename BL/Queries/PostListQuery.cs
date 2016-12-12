using System.Linq;
using AutoMapper.QueryableExtensions;
using BL.AppRigantiInfrastructure;
using BL.DTO.Filters;
using BL.DTO.PostDTOs;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Utils.Enums;

namespace BL.Queries
{
	public class PostListQuery : AppQuery<PostDTO>
	{
		public PostListQuery(IUnitOfWorkProvider provider) : base(provider)
		{
		}

		public PostFilter Filter { get; set; }


		protected override IQueryable<PostDTO> GetQueryable()
		{
			IQueryable<Post> query = Context.Posts;

			if (Filter.Group != null)
				query = query.Where(p => p.Group.ID == Filter.Group.ID);
			if (Filter.Message != null)
				query = query.Where(u => u.Message.ToLower()
					.Contains(Filter.Message.ToLower()));
			if (Filter.Sender != null)
				if (Filter.CurrentUser)
				{
					query = query.Where(u => u.Sender.ID == Filter.Sender.ID);
				}
				else
				{
					if (Filter.AreFriends)
						query = query.Where(u => u.Sender.ID == Filter.Sender.ID)
							.Where(u => (u.PrivacyLevel == PostPrivacyLevel.Public) ||
										(u.PrivacyLevel == PostPrivacyLevel.OnlyFriends));
					else
						query = query.Where(u => (u.Sender.ID == Filter.Sender.ID) && (u.PrivacyLevel == PostPrivacyLevel.Public));
				}
			if (Filter.PrivacyLevel != null)
				query = query.Where(u => u.PrivacyLevel == Filter.PrivacyLevel);
			if (Filter.FrontPageFilter != null)
			{
				var userQuery = query.Where(p => Filter.FrontPageFilter.Account.ID == p.Sender.ID);


				var groupIdList = Filter.FrontPageFilter.Groups.Select(g => g.ID);
				var friendIdList = Filter.FrontPageFilter.Friends.Select(u => u.ID);
				var groupQuery = query
					.Where(p => p.Group != null)
					.Where(post => groupIdList.Any(id => post.Group.ID == id));

				var friendQuery = query.Where(
						p => (p.PrivacyLevel == PostPrivacyLevel.OnlyFriends) || (p.PrivacyLevel == PostPrivacyLevel.Public))
					.Where(p => friendIdList.Any(id => id == p.Sender.ID));

				query = userQuery.Concat(groupQuery);
				query = query.Concat(friendQuery).Distinct();
			}

			return query.ProjectTo<PostDTO>();
		}
	}
}