using System;
using System.Data.Entity;
using BL.AppRigantiInfrastructure;
using BL.Identity;
using BL.Services;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DAL;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL
{
	public class BLInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Component.For<Func<DbContext>>()
					.Instance(() => new AppDbContext())
					.LifestyleTransient(),
				Component.For<IUnitOfWorkProvider>()
					.ImplementedBy<AppUnitOfWorkProvider>()
					.LifestyleSingleton(),
				Component.For<IUnitOfWorkRegistry>()
					.Instance(new HttpContextUnitOfWorkRegistry(new ThreadLocalUnitOfWorkRegistry()))
					.LifestyleSingleton(),
				Component.For(typeof(IRepository<,>))
					.ImplementedBy(typeof(EntityFrameworkRepository<,>))
					.LifestyleTransient(),
				Classes.FromAssemblyContaining<AppUnitOfWork>()
					.BasedOn(typeof(AppQuery<>))
					.LifestyleTransient(),
				Classes.FromAssemblyContaining<AppUnitOfWork>()
					.BasedOn(typeof(IRepository<,>))
					.LifestyleTransient(),
				Classes.FromThisAssembly()
					.BasedOn<AppService>()
					.WithServiceDefaultInterfaces()
					.LifestyleTransient(),
				Classes.FromThisAssembly()
					.InNamespace("BL.Facades")
					.LifestyleTransient()
				, Component.For<Func<AppUserManager>>()
					.Instance(() => new AppUserManager(new AppUserStore(new AppDbContext())))
					.LifestyleTransient()
				, Component.For<Func<AppRoleManager>>()
					.Instance(() => new AppRoleManager(new AppRoleStore(new AppDbContext())))
					.LifestyleTransient()
			);

			container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
		}
	}
}