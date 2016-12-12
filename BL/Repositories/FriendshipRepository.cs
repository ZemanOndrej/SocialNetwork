using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL.Repositories
{
	public class FriendshipRepository : EntityFrameworkRepository<Friendship, int>
	{
		public FriendshipRepository(IUnitOfWorkProvider provider) : base(provider)
		{
		}
	}
}