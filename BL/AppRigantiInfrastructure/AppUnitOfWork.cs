﻿using System;
using System.Data.Entity;
using DAL;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL.AppRigantiInfrastructure
{
	public class AppUnitOfWork : EntityFrameworkUnitOfWork
	{
		public AppUnitOfWork(IUnitOfWorkProvider provider, Func<DbContext> dbContextFactory, DbContextOptions options)
			: base(provider, dbContextFactory, options)
		{
		}

		public new AppDbContext Context => (AppDbContext) base.Context;
	}
}