using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL.Repositories
{
	public class ReactionRepository : EntityFrameworkRepository<Reaction, int>
	{
		public ReactionRepository(IUnitOfWorkProvider provider) : base(provider)
		{
		}
	}
}