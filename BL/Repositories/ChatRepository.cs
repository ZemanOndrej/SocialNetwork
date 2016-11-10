using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL.Repositories
{
	public class ChatRepository : EntityFrameworkRepository<Chat, int>
	{
		public ChatRepository(IUnitOfWorkProvider provider) : base(provider)
		{
		}
	}
}