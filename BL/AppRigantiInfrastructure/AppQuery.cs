using DAL;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL.AppRigantiInfrastructure
{
	public abstract class AppQuery<T> : EntityFrameworkQuery<T>
	{
		protected AppQuery(IUnitOfWorkProvider provider) : base(provider)
		{
		}

		public new AppDbContext Context => (AppDbContext) base.Context;
	}
}