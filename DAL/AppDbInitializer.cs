using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Entities;
using Utils.Enums;

namespace DAL.Entities
{
	public class AppDbInitializer : DropCreateDatabaseAlways<AppDbContext>
	{
		public override void InitializeDatabase(AppDbContext context)
		{
			base.InitializeDatabase(context);
//			Init(context);

			context.SaveChanges();

		}

		private static void Init(AppDbContext context)
		{
			#region UserInit

			var user1 = new User
			{
				Name = "forsenE",
				DateOfBirth = new DateTime(2000, 1, 1),
				Surname = "Zeman",
				Information = "NaM adas das sadas sda ssdas dasd asdas adsda asd asda sd",
				Email = "Kappa@Kappa.cz",
				Login = "Kappa"
			};
			var user2 = new User
			{
				Name = "Alojz",
				DateOfBirth = new DateTime(1337, 4, 20),
				Surname = "Novak",
				Information = "NaM adas das sadas sda ssdas dasd asdas adsda asd asda sd",
				Email = "PogChamp@Kappa.cz",
				Login = "Kappa1"
			};
			var user3 = new User
			{
				Name = "Ondrej",
				DateOfBirth = new DateTime(1996, 2, 13),
				Surname = "Zeman",
				Information = "forsenE forsen1 forsen2 forsen3 forsen4 forsenE",
				Email = "forsenE@gmail.com",
				Login = "RayMan"
			};

			user1.FriendList.Add(new Friendship(user1, user2));
			user1.FriendList.Add(new Friendship(user1, user3));
			user2.FriendList.Add(new Friendship(user2, user3));



			#endregion

			#region GroupInit

			var group1 = new Group
			{
				DateCreated = DateTime.Now,
				Name = "MUNI"
			};

			#endregion

			#region ChatInit

			var chatu1u2 = new Chat
			{
				Name = "groupchat user1 user2"
			};
			chatu1u2.ChatUsers.AddRange(new List<User> { user1, user2 });

			var chatMsgU1U2 = new ChatMessage
			{
				Message = "Ahoj",
				Chat = chatu1u2,
				Time = new DateTime(2001, 9, 11, 12, 32, 25),
				Sender = user1

			};
			chatu1u2.Messages.Add(chatMsgU1U2);
			group1.Accounts.Add(user1);
			group1.Name = "MUNI";
			context.Groups.Add(group1);

			#endregion

			#region Post/Comment/ReactInit

			var postU2 = new Post
			{
				Message = "Kappa",
				Sender = user2,
				Time = new DateTime(2001, 9, 11, 9, 11, 25)
			};


			var reacU1 = new Reaction
			{
				User = user1,
				UserReaction = ReactionEnum.PogChamp
			};
			postU2.Reactions.Add(reacU1);


			var commentU1 = new Comment
			{
				CommentMessage = "MEGALUL",
				Sender = user1,
				Time = new DateTime(2001, 9, 11, 9, 11, 27),
				Post = postU2
			};
			postU2.Comments.Add(commentU1);
			var postU1 = new Post
			{
				Message = "Jet Fuel Can't Melt Steel Beams",
				Sender = user1,
				Time = new DateTime(2001, 9, 11, 9, 57, 57),
				Group = group1
			};
			context.Reactions.Add(reacU1);
			context.ChatSet.Add(chatu1u2);
			context.ChatMessages.Add(chatMsgU1U2);
			context.Comments.Add(commentU1);
			context.Posts.AddRange(new List<Post> { postU1, postU2 });

			#endregion

			#region GameInit

			//			var game = new GameAccount
			//			{
			//				Gold = 123,
			//				Name = "RayMan",
			//				Owner = user3,
			//				Tag = 0
			//			};
			//			user3.GameAccount = game;
			//			context.GameAccounts.Add(game);

			#endregion

			context.Users.AddRange(new List<User> { user1, user2, user3 });
		}
	}
}