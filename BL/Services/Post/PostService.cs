using System;
using System.Collections.Generic;
using AutoMapper;
using BL.DTO;
using BL.DTO.Filters;
using BL.DTO.PostDTOs;
using BL.Queries;
using BL.Repositories;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Services.Post
{
	public class PostService :AppService,IPostService
	{


		public int PostPageSize => 20;

		#region Dependency
		private readonly PostRepository postRepository;
		private readonly UserRepository userRepository;
		private readonly PostListQuery postListQuery;
		private readonly GroupRepository groupRepository;



		public PostService(PostRepository postRepository, UserRepository userRepository, PostListQuery postListQuery, GroupRepository groupRepository)
		{
			this.postRepository = postRepository;
			this.userRepository = userRepository;
			this.postListQuery = postListQuery;
			this.groupRepository = groupRepository;
		}
		#endregion

		#region CreateDelete
		public int CreatePost(PostDTO post)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{
				var postEnt = Mapper.Map<DAL.Entities.Post>(post);
				postEnt.Sender = userRepository.GetById(post.Sender.ID);
				if(post.Group!=null)
					postEnt.Group = groupRepository.GetById(post.Group.ID);
				postEnt.Time= DateTime.Now;
				postRepository.Insert(postEnt);
				uow.Commit();
				return postEnt.ID;
			}
		}

		public void DeletePost(int id)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{
				postRepository.Delete(id);
				uow.Commit();
			}
		}

		public void DeletePost(PostDTO post)
		{
			DeletePost(post.ID);
		}

		#endregion

		#region Update
		public void EditPostMessage(PostDTO post)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{
				var postEnt = postRepository.GetById(post.ID,p=>p.Comments,p=>p.Group,p=>p.Reactions,p=>p.Sender);
				postEnt.Message = post.Message;
				postRepository.Update(postEnt);
				uow.Commit();
			}
		}


		#endregion

		#region Get

		public PostDTO GetPostById(int id)
		{
			using (UnitOfWorkProvider.Create())
			{
				var message = postRepository.GetById(id);
				return message != null ? Mapper.Map<PostDTO>(message) : null;
			}
		}

		public PostListQueryResultDTO ListPosts(PostFilter filter, int page=0)
		{
			using (UnitOfWorkProvider.Create())
			{
				
				var query = GetQuery(filter);
				query.Skip = (page > 0 ? page - 1 : 0) * PostPageSize;
				query.Take = PostPageSize;

				

				query.AddSortCriteria(customer => customer.Time,SortDirection.Descending);

				return new PostListQueryResultDTO
				{
					RequestedPage = page,
					ResultCount = query.GetTotalRowCount(),
					ResultPosts = query.Execute(),
					Filter = new PostFilter()
				};

			}
		}


		public List<ReactionDTO> GetReactionsOnPost(PostDTO post)
		{
			using (UnitOfWorkProvider.Create())
			{
				var postEnt = postRepository.GetById(post.ID, p=>p.Reactions);

				return Mapper.Map<List<ReactionDTO>>(postEnt.Reactions);

			}
		}
		#endregion



		#region additional

		private IQuery<PostDTO> GetQuery(PostFilter filter )
		{
			var query = postListQuery;
			query.ClearSortCriterias();
			query.Filter = filter;
			return query;
		}

		#endregion
	}
}
