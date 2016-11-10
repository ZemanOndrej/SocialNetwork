using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL.Repositories
{
	public class GroupRepository : EntityFrameworkRepository<Group, int>
	{
		public GroupRepository(IUnitOfWorkProvider provider) : base(provider)
		{
		}
	}
}