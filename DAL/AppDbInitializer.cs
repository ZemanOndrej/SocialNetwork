using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Entities;
using DAL.Entities.Identity;
using DAL.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Utils.Enums;

namespace DAL.Entities
{
	public class AppDbInitializer : DropCreateDatabaseAlways<AppDbContext>
	{
		public override void InitializeDatabase(AppDbContext context)
		{
			base.InitializeDatabase(context);
			Init(context);

			context.SaveChanges();

		}

		private static void Init(AppDbContext context)
		{

			var roleStore = new AppRoleStore(context);
			var roleManager = new AppRoleManager(roleStore);
			var userStore = new AppUserStore(context);
			var userManager = new AppUserManager(userStore);
			context.Roles.Add(new AppRole { Name = "admin" });
			context.Roles.Add(new AppRole { Name = "user" });

			#region UserInit

			var password = "aaaaaa";

			var user1 = new Account
			{
				Name = "Sebastian",
				DateOfBirth = new DateTime(2000, 1, 1),
				Surname = "Fors",
				Information = "NaM adas das sadas sda ssdas dasd asdas adsda asd asda sd",
				User = new User { Email = "a@a.a",UserName = "Sebastian"}
			};

			var user2 = new Account
			{
				Name = "Alojz",
				DateOfBirth = new DateTime(1337, 4, 20),
				Surname = "Novak",
				Information = "NaM adas das sadas sda ssdas dasd asdas adsda asd asda sd",
				User = new User { Email ="PogChamp@Kappa.cz",UserName = "Kappa1" }

			};
			var user3 = new Account
			{
				Name = "Ondrej",
				DateOfBirth = new DateTime(1996, 2, 13),
				Surname = "Zeman",
				Information = "forsenE forsen1 forsen2 forsen3 forsen4 forsenE",
				User = new User { Email ="forsenE@gmail.com",UserName ="RayMan"}

			};
			var user4 = new Account
			{
				Name = "Admin",
				DateOfBirth = new DateTime(2000, 1, 1),
				Surname = "admin",
				Information = "NaM adas das sadas sda ssdas dasd asdas adsda asd asda sd",
				User = new User { Email = "q@q.q", UserName = "Admin" }
			};

			user1.User.Account = user1;
			user2.User.Account = user2;
			user3.User.Account = user3;
			user4.User.Account = user4;

			userManager.Create(user1.User, password);
			userManager.Create(user2.User, password);
			userManager.Create(user3.User, password);
			userManager.Create(user4.User, password);
			
			userManager.AddToRole(user1.ID, "admin");
			userManager.AddToRole(user2.ID, "user");
			userManager.AddToRole(user3.ID, "user");
			userManager.AddToRole(user4.ID, "admin");


			context.Friendships.Add(new Friendship(user1, user3));
			context.Friendships.Add(new Friendship(user2, user3));
			context.Friendships.Add(new Friendship(user1, user2));


			#endregion

			#region GroupInit

			var group1 = new Group
			{
				DateCreated = DateTime.Now,
				Name = "MUNI",
				Description = "This is Muni School Group"
			};

			group1.Accounts.Add(user1);
			group1.Name = "MUNI";
			context.Groups.Add(group1);

			#endregion

			#region ChatInit

			var chatu1u4 = new Chat
			{
				Name = "groupchat user1 user4"
			};
			chatu1u4.ChatUsers.AddRange(new List<Account> { user1, user4 });


			
			var chatMsgU1U2 = new ChatMessage
			{
				Message = "Ahoj",
				Chat = chatu1u4,
				Time = new DateTime(2001, 9, 11, 12, 32, 25),
				Sender = user1

			};
			chatu1u4.Messages.Add(chatMsgU1U2);
			for (int i = 0; i < 50; i++)
			{
				var msgTmp = new ChatMessage
				{
					Message = string.Concat(Enumerable.Repeat(i.ToString(), i + 1)),
					Sender = user1,
					Time = DateTime.Now
				};
				chatu1u4.Messages.Add(msgTmp);
			}


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
				Account = user1,
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
			context.ChatSet.Add(chatu1u4);
			context.ChatMessages.Add(chatMsgU1U2);
			context.Comments.Add(commentU1);
			context.Posts.AddRange(new List<Post> { postU1, postU2 });
			for (int i = 0; i < 50; i++)
			{
				var postTmp = new Post
				{
					Message = string.Concat(Enumerable.Repeat(i.ToString(), i+1)),
					Sender = user1,
					Time = DateTime.Now
				};
				context.Posts.Add(postTmp);
			}

			#endregion

			#region Requests

			context.Requests.Add(new Request {Group = group1,Receiver = user2,Sender = user1 ,Time = DateTime.Now});
			context.Requests.Add(new Request {Receiver = user4,Sender = user1, Time = DateTime.Now });
			context.Requests.Add(new Request { Group = group1, Receiver = user4, Sender = user1, Time = DateTime.Now });

			#endregion
			



		}
	}
}