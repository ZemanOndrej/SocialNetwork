using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTO;
using BL.DTO.Filters;
using BL.DTO.PostDTOs;
using BL.Queries;
using BL.Repositories;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Services.Comment
{
	public class CommentService : AppService, ICommentService
	{
		public int CommentPageSize => 30;


		#region Dependency
		private readonly CommentRepository commentRepository;

		private readonly CommentListQuery commentListQuery;

		private readonly UserRepository userRepository;

		private readonly PostRepository postRepository;

		public CommentService(CommentRepository commentRepository, CommentListQuery commentListQuery, UserRepository userRepository, PostRepository postRepository)
		{
			this.commentRepository = commentRepository;
			this.commentListQuery = commentListQuery;
			this.userRepository = userRepository;
			this.postRepository = postRepository;
		}

		#endregion

		#region CreateDelete

		public int CreateComment(UserDTO user, CommentDTO comment, PostDTO post)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{
				var commentEnt = Mapper.Map<DAL.Entities.Comment>(comment);
				var userEnt = userRepository.GetById(user.ID);
				var postEnt = postRepository.GetById(post.ID);


				commentEnt.Time = DateTime.Now;
				commentEnt.Post = postEnt;
				commentEnt.Sender = userEnt;
				commentRepository.Insert(commentEnt);
				uow.Commit();
				return commentEnt.ID;
			}
		}

		public int CreateComment(CommentDTO comment)
		{
			return CreateComment(comment.Sender,comment,comment.Post);
		}

		public void DeleteComment(int id)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{
				commentRepository.Delete(id);
				uow.Commit();
			}
		}

		#endregion

		#region Update

		public void EditCommentMessage(CommentDTO comment)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{
				var commentEnt = commentRepository.GetById(comment.ID,c=> c.Post, c=> c.Sender);

				commentEnt.CommentMessage = comment.CommentMessage;
				commentRepository.Update(commentEnt);
				uow.Commit();
			}
		}

		#endregion

		#region Get

		public CommentDTO GetCommentById(int id)
		{
			using (UnitOfWorkProvider.Create())
			{
				var comment = commentRepository.GetById(id,c=>c.Post,c=>c.Sender);
				return comment != null ? Mapper.Map<CommentDTO>(comment) : null;
			}
		}


		public CommentListQueryResultDTO ListComments(CommentFilter filter, int page=0)
		{

			using (UnitOfWorkProvider.Create())
			{
				
				var query = GetQuery(filter);
				query.Skip = (page > 0 ? page - 1 : 0) * CommentPageSize;
				query.Take = CommentPageSize;

				query.AddSortCriteria(dto => dto.Time);

				return new CommentListQueryResultDTO
				{
					RequestedPage = page,
					ResultCount = query.GetTotalRowCount(),
					ResultComments = query.Execute(),
					Filter = filter
				};

			}
		}

		#endregion

		#region additional

		private IQuery<CommentDTO> GetQuery(CommentFilter filter)
		{
			var query = commentListQuery;
			query.ClearSortCriterias();
			query.Filter = filter;
			return query;
		}

		#endregion

	}
}
