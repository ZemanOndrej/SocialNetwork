﻿using System;

namespace Riganti.Utils.Infrastructure.Core
{
	/// <summary>
	///     An interface that represents a boundary of a business transaction.
	/// </summary>
	public interface IUnitOfWork : IDisposable
	{
		/// <summary>
		///     Commits the changes made inside this unit of work.
		/// </summary>
		void Commit();

		/// <summary>
		///     Registers an action to be applied after the work is committed.
		/// </summary>
		void RegisterAfterCommitAction(Action action);

		/// <summary>
		///     Occurs when this unit of work is disposed.
		/// </summary>
		event EventHandler Disposing;
	}
}