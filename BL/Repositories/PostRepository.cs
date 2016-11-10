using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL.Repositories
{
	public class PostRepository : EntityFrameworkRepository<Post, int>
	{
		public PostRepository(IUnitOfWorkProvider provider) : base(provider)
		{
		}
	}
}