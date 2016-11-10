using Riganti.Utils.Infrastructure.Core;

namespace BL.Services
{
	public abstract class AppService
	{
		public IUnitOfWorkProvider UnitOfWorkProvider { get; set; }
	}
}
