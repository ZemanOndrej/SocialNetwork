using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTO;
using BL.DTO.Filters;
using BL.DTO.PostDTOs;
using BL.DTO.UserDTOs;
using BL.Queries;
using BL.Repositories;
using Riganti.Utils.Infrastructure.Core;
using Utils.Enums;

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

		public int CreateReaction( PostDTO post, ReactionEnum reaction,AccountDTO account)
		{
			var reactCheck = ListReactions(new ReactionFilter {Post = post, Account = account}).ResultReactions.ToList();
			if (reactCheck.Any())
			{
				if (reactCheck[0].UserReaction.Equals(reaction)) return 0;
				reactCheck[0].UserReaction = reaction;
				EditReaction(reactCheck.FirstOrDefault());

				return 0;
			}
			using (var uow = UnitOfWorkProvider.Create())
			{
				var reactionEnt = new DAL.Entities.Reaction {UserReaction = reaction};
				var userEnt = userRepository.GetById(account.ID);
				var postEnt = postRepository.GetById(post.ID);

				reactionEnt.Account = userEnt;
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
				var reactionEnt = reactionRepository.GetById(reaction.ID,r=>r.Account,r=>r.Post);
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
