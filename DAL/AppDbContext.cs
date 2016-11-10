using System.Data.Entity;
using DAL.Entities;

namespace DAL
{
	public class AppDbContext : DbContext
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
		public DbSet<User> Users { get; set; }
		public DbSet<Friendship> Friendships { get; set; }


//		public DbSet<GameAccount> GameAccounts { get; set; }


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//						modelBuilder.ConfigureMembershipRebootUserAccounts<UserAccount>();

			modelBuilder.Entity<Comment>()
					   .HasRequired(c => c.Sender)
					   .WithMany()
					   .WillCascadeOnDelete(false);
			
			modelBuilder.Entity<Reaction>()
					   .HasRequired(r =>r.User )
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

			//			modelBuilder.Entity<User>()
			//				.HasMany(m => m.FriendList)
			//				.WithMany(m => m.FriendList)
			//				.Map(w => w.ToTable("User_Friend").MapLeftKey("User1Id").MapRightKey("User2Id"));
			//			
			//			modelBuilder.Entity<Player>()
			//				.HasRequired<Team>(p => p.Club)
			//				.WithMany()
			//				.WillCascadeOnDelete(false);
		}

		//
		private void InitializeDbContext()
		{
			Database.SetInitializer(new AppDbInitializer());
//			this.RegisterUserAccountChildTablesForDelete<UserAccount>();
		}
	}
}