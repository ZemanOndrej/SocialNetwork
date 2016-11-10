﻿using System;
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

namespace BL.Services.Reaction
{
	public class ReactionService :AppService,IReactionService
	{

		#region Dependency

		private readonly ReactionRepository reactionRepository;
		private readonly ReactionListQuery reactionListQuery;
		private readonly UserRepository userRepository;
		private readonly PostRepository postRepository;

		public ReactionService(ReactionRepository reactionRepository, ReactionListQuery reactionListQuery, UserRepository userRepository, PostRepository postRepository)
		{
			this.reactionRepository = reactionRepository;
			this.reactionListQuery = reactionListQuery;
			this.userRepository = userRepository;
			this.postRepository = postRepository;
		}
		#endregion

		#region CreateDelete

		public int CreateReaction( PostDTO post, ReactionDTO reaction,UserDTO user)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{
				var reactionEnt = Mapper.Map<DAL.Entities.Reaction>(reaction);
				var userEnt = userRepository.GetById(user.ID);
				var postEnt = postRepository.GetById(post.ID);

				reactionEnt.User = userEnt;
				reactionEnt.Post = postEnt;

				reactionRepository.Insert(reactionEnt);
				uow.Commit();
				return reactionEnt.ID;
			}
		}

		public void DeleteReaction(int id)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{
				reactionRepository.Delete(id);
				uow.Commit();
			}
		}

		#endregion


		#region Update

		public void EditReaction(ReactionDTO reaction)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{
				var reactionEnt = reactionRepository.GetById(reaction.ID,r=>r.User,r=>r.Post);
				reactionEnt.UserReaction = reaction.UserReaction;

				reactionRepository.Update(reactionEnt);
				uow.Commit();
			}
		} 

		#endregion

		#region Get

		public ReactionDTO GetReactionById(int id)
		{
			using (UnitOfWorkProvider.Create())
			{
				var comment = reactionRepository.GetById(id);
				return comment != null ? Mapper.Map<ReactionDTO>(comment) : null;
			}
		}

		public ReactionListQueryResultDTO ListReactions(ReactionFilter filter)
		{
			using (UnitOfWorkProvider.Create())
			{
				
				var query = GetQuery(filter);

				return new ReactionListQueryResultDTO
				{
					Filter = filter,
					ResultCount = query.GetTotalRowCount(),
					ResultReactions = query.Execute(),
					
				};

			}
		}

		#endregion


		#region additional

		private IQuery<ReactionDTO> GetQuery(ReactionFilter filter)
		{
			var query = reactionListQuery;
			query.Filter = filter;
			query.ClearSortCriterias();
			return query;
		}

		#endregion


	}
}