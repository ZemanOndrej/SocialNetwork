using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL.Repositories
{
	public class RequestRepository : EntityFrameworkRepository<Request, int>
	{
		public RequestRepository(IUnitOfWorkProvider provider) : base(provider)
		{
		}
	}
}