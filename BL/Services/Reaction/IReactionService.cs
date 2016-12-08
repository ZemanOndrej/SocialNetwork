using BL.DTO;
using BL.DTO.Filters;
using BL.DTO.PostDTOs;
using BL.DTO.UserDTOs;

namespace BL.Services.Reaction
{
	public interface IReactionService
	{

		int CreateReaction(PostDTO post, ReactionDTO reaction, AccountDTO account);

		void EditReaction(ReactionDTO reaction);

		void DeleteReaction(int id);

		ReactionDTO GetReactionById(int id);

		ReactionListQueryResultDTO ListReactions(ReactionFilter filter);
	}
}
