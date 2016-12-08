using System.Data.Entity;
using DAL.Entities;
using DAL.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL
{
	public class AppDbContext : IdentityDbContext<User, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
	{
		public AppDbContext() : base("SocialNetworkDBContext")
		{
			InitializeDbContext();
		}

		public AppDbContext(string connectionName) : base(connectionName)
		{
			InitializeDbContext();
		}


		public DbSet<Chat> ChatSet { get; set; }
		public DbSet<ChatMessage> ChatMessages { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Group> Groups { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Reaction> Reactions { get; set; }
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Friendship> Friendships { get; set; }
		public DbSet<Request> Requests { get; set; }



		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);



			modelBuilder.Entity<Comment>()
					   .HasRequired(c => c.Sender)
					   .WithMany()
					   .WillCascadeOnDelete(false);

			modelBuilder.Entity<Reaction>()
					   .HasRequired(r => r.Account)
					   .WithMany()
					   .WillCascadeOnDelete(false);

			modelBuilder.Entity<Friendship>()
					   .HasRequired(r => r.User1)
					   .WithMany()
					   .WillCascadeOnDelete(false);

			modelBuilder.Entity<Friendship>()
					   .HasRequired(r => r.User2)
					   .WithMany()
					   .WillCascadeOnDelete(false);


			modelBuilder.Entity<Request>()
						.HasRequired(r=>r.Sender)
						.WithMany()
						.WillCascadeOnDelete(false);

			modelBuilder.Entity<Request>()
						.HasRequired(r => r.Receiver)
						.WithMany()
						.WillCascadeOnDelete(false);


			modelBuilder.Entity<Account>()
						.HasOptional(u => u.User)
						.WithRequired(u => u.Account);
//						.WillCascadeOnDelete(true);


		}

		private void InitializeDbContext()
		{
			Database.SetInitializer(new AppDbInitializer());
		}
	}
}