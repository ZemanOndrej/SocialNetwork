using System;
using System.Collections.Generic;
using BL;
using BL.DTO;
using BL.DTO.GroupDTOs;
using BL.Facades;
using BL.Services.Chat;
using BL.Services.ChatMessage;
using BL.Services.Comment;
using BL.Services.Group;
using BL.Services.Post;
using BL.Services.Reaction;
using BL.Services.User;
using Castle.Windsor;
using Utils.Enums;


namespace SocialNetwork
{

	public class Program
	{
		
		private static UserDTO userDtoBoris;
		private static UserDTO userDto2David;
		private static UserDTO userDto3Jozko;
		private static UserDTO userDto3Martin;
		private static GroupDTO groupMuni;
		private static GroupDTO groupTwitch;
		private static GroupDTO testGroup;

		private static PostDTO post1;
		private static PostDTO post2;

		private static ReactionDTO reaction1;
		private static ReactionDTO reaction2;

		private static CommentDTO comment1;
		private static CommentDTO comment2;

		private static ChatDTO chatDavadBorisJozko;
		private static ChatDTO chatDavadBoris;

		private static ChatMessageDTO chatMessage1;
		private static ChatMessageDTO chatMessage2;
		private static ChatMessageDTO chatMessage3;

		#region Init
		private static IUserService userService;
		private static IChatService chatService;
		private static IPostService postService;
		private static IGroupService groupService;
		private static ICommentService commentService;
		private static IReactionService reactionService;
		private static IChatMessageService chatMessageService;

		private static PostFacade postFacade;
		private static ChatFacade chatFacade;
		private static UserFacade userFacade;
		private static GroupFacade groupFacade;
		

		private static readonly IWindsorContainer Container = new WindsorContainer();

		private static void InitializeWindsorContainerAndMapper()
		{
			Container.Install(new BLInstaller());

			InitMapping.ConfigMapping();
		}



		public static void InitWindsorAndStart()
		{
			InitializeWindsorContainerAndMapper();
			userService = Container.Resolve<IUserService>();
			chatService = Container.Resolve<IChatService>();
			chatMessageService = Container.Resolve<IChatMessageService>();
			postService = Container.Resolve<IPostService>();
			groupService = Container.Resolve<IGroupService>();
			commentService = Container.Resolve<ICommentService>();
			reactionService = Container.Resolve<IReactionService>();

			postFacade = Container.Resolve<PostFacade>();
			userFacade = Container.Resolve<UserFacade>();
			groupFacade = Container.Resolve<GroupFacade>();
			chatFacade = Container.Resolve<ChatFacade>();


			TestBLFacades();
		}

		public static void Main(string[] args)
		{
			
			InitWindsorAndStart();
			Console.WriteLine("press any key to exit");
			Console.ReadKey();
		}
		private static void TestBL()
		{
			Console.WriteLine("//////USER TEST////////");
			UserServiceTest();
			Console.WriteLine("//////CHAT TEST////////");
			ChatServiceTest();
			Console.WriteLine("//////POST TEST////////");
			PostServiceTest();
			Console.WriteLine("//////GROUP TEST////////");
			GroupServiceTest();

		}



		#endregion


		private static void TestBLFacades()
		{


			Init();

			/*TODO MERGE REACTION AND COMMENT AND MAKE COMMENT REACTABLE 

			Tie Game fily si nevsimaj chcel som spravit aj hru na tu socialnu siet
			ale nevyzsie zatial cas

	
			*/


			Console.WriteLine("//////////////////CHAT FACADE TEST//////////////////");
			ChatFacadeTest();
			Console.WriteLine("//////////////////POST FACADE TEST//////////////////");
			PostFacadeTest();
			Console.WriteLine("//////////////////GROUP FACADE TEST//////////////////");
			GroupFacadeTest();
			Console.WriteLine("//////////////////USER FACADE TEST//////////////////");
			UserFacadeTest();

		}

		private static void PostFacadeTest()
		{
			Console.WriteLine("|||||||||||||||||||  ListAllPosts ||||||||||||||||||||||");
			postFacade.ListAllPosts().ForEach(Console.WriteLine);
			Console.WriteLine("||||||||||||||||||| GET COMMENTS ON POST  ||||||||||||||||||||||");
			postFacade.GetCommentsOnPost(post1).ForEach(Console.WriteLine);
			Console.WriteLine("||||||||||||||||||| GET Reactions ON POST  ||||||||||||||||||||||");
			postFacade.GetReactionsOnPost(post1).ForEach(Console.WriteLine);

			Console.WriteLine("||||||||||||||||||| REMOVE COMMENT  AND REACTION ||||||||||||||||||||||");

			postFacade.DeleteComment(comment2);
			postFacade.DeleteReaction(reaction2);
			postFacade.DeletePost(post2);

			

			Console.WriteLine("||||||||||||||||||| Update comment,Reaction and post ||||||||||||||||||||||");

			comment1.CommentMessage = "FeelsGoodMan";
			post1.Message = "NotLikeThis";
			reaction1.UserReaction=ReactionEnum.ResidentSleeper;
			postFacade.UpdateReaction(reaction1);

			postFacade.UpdatePostMessage(post1);
			postFacade.UpdateComment(comment1);

			postFacade.GetCommentsOnPost(post1).ForEach(Console.WriteLine);
			postFacade.GetReactionsOnPost(post1).ForEach(Console.WriteLine);
			postFacade.ListAllPosts().ForEach(Console.WriteLine);


			Console.WriteLine("||||||||||||||||||| Update comment,Reaction and post ||||||||||||||||||||||");

		}

		private static void ChatFacadeTest()
		{
			Console.WriteLine("|||||||||||||||||||  List AllChats ||||||||||||||||||||||");
			chatFacade.ListAllChats().ForEach(Console.WriteLine);
			Console.WriteLine("|||||||||||||||||||  List AllChatMessages ||||||||||||||||||||||");
			chatFacade.ListAllChatMessages().ForEach(Console.WriteLine);
			Console.WriteLine("|||||||||||||||||||  DELETE and UPDATE Message and Chat ||||||||||||||||||||||");
			chatFacade.DeleteChat(chatDavadBoris);
			chatFacade.DeleteChatMessage(chatMessage1);

			chatFacade.EditChatMessage(chatMessage2,"Vyhodili ma zo skoly");
			chatFacade.EditChatName(chatDavadBorisJozko,"MUNI chat");
			Console.WriteLine("|||||||||||||||||||  List AllChats ||||||||||||||||||||||");
			chatFacade.ListAllChats().ForEach(Console.WriteLine);
			Console.WriteLine("|||||||||||||||||||  List AllChatMessages ||||||||||||||||||||||");
			chatFacade.ListAllChatMessages().ForEach(Console.WriteLine);
			Console.WriteLine("|||||||||||||||||||  List All Users in chat ||||||||||||||||||||||");
			chatFacade.ListAllUsersInChat(chatDavadBorisJozko).ForEach(u=> Console.WriteLine(u.FullName));

			Console.WriteLine("|||||||||||||||||||  Remove Jozko from chat and add martin ||||||||||||||||||||||");
			chatFacade.RemoveUserFromChat(chatDavadBorisJozko,userDto3Jozko);
			chatFacade.AddUserToChat(chatDavadBorisJozko,userDto3Martin);
			chatFacade.ListAllUsersInChat(chatDavadBorisJozko).ForEach(u => Console.WriteLine(u.FullName));

			Console.WriteLine("|||||||||||||||||||  List All Chat Messages||||||||||||||||||||||");

			chatFacade.GetChatMessagesFromChat(chatDavadBorisJozko).ForEach( Console.WriteLine);






		}


		private static void GroupFacadeTest()
		{
			Console.WriteLine("|||||||||||||||||||  ListAllGroups  ||||||||||||||||||||||");
			groupFacade.ListAllGroups().ForEach(Console.WriteLine);
			Console.WriteLine("|||||||||||||||||||  Add Users to MUNI  ||||||||||||||||||||||");

			groupFacade.AddUserToGroup(groupMuni,userDtoBoris);
			groupFacade.AddUserToGroup(groupMuni,userDto2David);
			groupFacade.AddUserToGroup(groupMuni,userDto3Martin);
			Console.WriteLine("|||||||||||||||||||LIST USERS IN GROUP||||||||||||||||||||||");
			groupFacade.ListUsersInGroup(groupMuni).ForEach(u => Console.WriteLine(u.FullName));
			Console.WriteLine("|||||||||||||||||||  REMOVE USER FROM GROUP  ||||||||||||||||||||||");
			groupFacade.RemoveUserFromGroup(groupMuni, userDto2David);
			Console.WriteLine("|||||||||||||||||||LIST USERS IN GROUP||||||||||||||||||||||");
			groupFacade.ListUsersInGroup(groupMuni).ForEach(u => Console.WriteLine(u.FullName));
			Console.WriteLine("|||||||||||||||||||  EDIT GroupName  ||||||||||||||||||||||");
			
			groupMuni.Name = "Masaryk University";
			groupFacade.EditGroupName(groupMuni);
			groupFacade.ListAllGroups().ForEach(Console.WriteLine);
			Console.WriteLine("|||||||||||||||||||  DELETE GROUP  ||||||||||||||||||||||");
			groupFacade.DeleteGroup(testGroup);
			groupFacade.ListAllGroups().ForEach(Console.WriteLine);
			Console.WriteLine("|||||||||||||||||||  LIST GROUPS WITH NAME  ||||||||||||||||||||||");
			groupFacade.ListGroupsWithName("Masaryk").ForEach(Console.WriteLine);

		}

		private static void UserFacadeTest()
		{
			Console.WriteLine("|||||||||||||||||||  ListAllUsers  ||||||||||||||||||||||");
			userFacade.ListAllUsers().ForEach(u=>Console.WriteLine(u.FullName));
			
			Console.WriteLine("||||||||||||||||||| Edit User(default)||||||||||||||||||||||");
			Console.WriteLine(userDto3Jozko);
			Console.WriteLine("||||||||||||||||||| UPDATED USER||||||||||||||||||||||");
			userDto3Jozko.Name = "Majka";
			userDto3Jozko.Gender=Gender.Female;
			userDto3Jozko.Surname = "Mrkvickova";
			userDto3Jozko.Email = "Maja@gmail.com";
			userFacade.UpdateUserInfo(userDto3Jozko);
			userDto3Jozko = userFacade.GetUserByEmail("Maja@gmail.com");
			Console.WriteLine(userDto3Jozko);
			Console.WriteLine("||||||||||||||||||| ADDING FRIENDS(No friends)||||||||||||||||||||||");
			userFacade.ListFriendsOf(userDto3Jozko).ForEach(Console.WriteLine);
			userFacade.AddUsersToFriends(userDto3Jozko,userDto2David);
			userFacade.AddUsersToFriends(userDto3Jozko,userDtoBoris);
			Console.WriteLine("||||||||||||||||||| JozkosFriends FRIENDS||||||||||||||||||||||");
			userFacade.ListFriendsOf(userDto3Jozko).ForEach(u => Console.WriteLine(u.FullName));
			Console.WriteLine("||||||||||||||||||| REMOVING FRIENDS FROM JOZKO||||||||||||||||||||||");
			userFacade.RemoveUsersFromFriends(userDto3Jozko,userDto2David);
			userFacade.ListFriendsOf(userDto3Jozko).ForEach(u => Console.WriteLine(u.FullName));
			Console.WriteLine("||||||||||||||||||| Search FOR USER With email gmail.com||||||||||||||||||||||");
			userFacade.GetUsersWithName("Boris").ForEach(Console.WriteLine);
			Console.WriteLine("|||||||||||||||||||DELETE MARTIN||||||||||||||||||||||");
			userFacade.RemoveUser(userDto3Martin);
			userFacade.ListAllUsers().ForEach(u => Console.WriteLine(u.FullName));
			Console.WriteLine("|||||||||||||||||||  Check borises groups  ||||||||||||||||||||||");

			userFacade.ListGroupsWithUser(userDtoBoris).ForEach(Console.WriteLine);


			Console.WriteLine("|||||||||||||||||||USERFACADE COMPLETE||||||||||||||||||||||");


		}


		private static void Init()
		{
			var userid1= userFacade.CreateNewUser(new UserDTO
			{
				Name = "David",
				DateOfBirth = new DateTime(1996, 2, 13),
				Surname = "Bielik",
				Information = "NaM SHUTTHEFUCKUPWEEBS NaM",
				Email = "dvdblk@q.cz",
				Login = "dvdblk"
			});
			var userid2=userFacade.CreateNewUser(new UserDTO
			{
				Name = "Boris",
				DateOfBirth = new DateTime(1999, 5, 7),
				Surname = "Petrenko",
				Information = "Random info string Boris",
				Email = "boris@gmail.com",
				Login = "borenko"
			});

			var userid3 = userFacade.CreateNewUser(new UserDTO
			{
				Name = "Martin",
				DateOfBirth = new DateTime(1996, 9, 6),
				Surname = "Macak",
				Information = "Random info string Martin",
				Email = "qq@gmail.com",
				Login = "Kappa123"
			});
			var userid4 = userFacade.CreateNewUser(new UserDTO
			{
				Name = "Jozko",
				DateOfBirth = new DateTime(2000, 1, 1),
				Surname = "Mrkvicka",
				Information = "Random info string Jozko",
				Email = "12@123.sk",
				Login = "EleGiggle13"
			});

			userDto3Martin = userFacade.GetUserById(userid3);
			userDto3Jozko = userFacade.GetUserById(userid4);
			userDtoBoris = userFacade.GetUserById(userid2);
			userDto2David = userFacade.GetUserById(userid1);


			var groupId = groupFacade.CreateNewGroup(new GroupDTO
			{
				Name = "MUNI"
			});

			var groupId2 = groupFacade.CreateNewGroup(new GroupDTO
			{
				Name = "Twtich"
			});
			var groupTest = groupFacade.CreateNewGroup(new GroupDTO
			{
				Name = "test"
			});

			
			testGroup = groupFacade.GetGroupById(groupTest);
			groupMuni = groupFacade.GetGroupById(groupId);
			groupTwitch = groupFacade.GetGroupById(groupId2);

			groupFacade.AddUserToGroup(testGroup,userDtoBoris);


			var post1Id = postFacade.SendPost(new PostDTO
			{
				Message = "Hello world"
				
			},userDtoBoris);

			var post2Id = postFacade.SendPost(new PostDTO
			{
				Message = "MEGALUL TRUMP 1337"

			}, userDto2David,testGroup);

			post2 = postFacade.GetPostById(post2Id);
			post1 = postFacade.GetPostById(post1Id);

			var reaction1Id = postFacade.ReactOnPost(post1, 
				new ReactionDTO {UserReaction = ReactionEnum.LUL}, userDto2David);
			var reaction2Id = postFacade.ReactOnPost(post1, 
				new ReactionDTO {UserReaction = ReactionEnum.FeelsBadMan},userDto3Jozko);

			reaction1 = postFacade.GetReactionById(reaction1Id);
			reaction2 = postFacade.GetReactionById(reaction2Id);

			var comment1Id = postFacade.CommentPost(post1, new CommentDTO {CommentMessage = "ALOHA"}, userDto2David);
			var comment2Id = postFacade.CommentPost(post1, new CommentDTO { CommentMessage = "How are you man?" }, userDtoBoris);

			comment1 = postFacade.GetCommentById(comment1Id);
			comment2 = postFacade.GetCommentById(comment2Id);

			var chat1iD= chatFacade.CreateChat(userDto2David, userDtoBoris);

			var chat2Id = chatFacade.CreateGroupChat(new List<UserDTO> { userDto2David, userDtoBoris, userDto3Jozko});

			chatDavadBorisJozko = chatFacade.GetChatById(chat2Id);
			chatDavadBoris = chatFacade.GetChatById(chat1iD);

			var chatMsg1 = chatFacade.SendChatMessageToChat(chatDavadBorisJozko, userDto2David,
				new ChatMessageDTO {Message = "jak sa dari v skole?"});
			var chatMsg2 = chatFacade.SendChatMessageToChat(chatDavadBorisJozko, userDtoBoris,
				new ChatMessageDTO { Message = "Vidime sa v skole" });
			var chatMsg3 = chatFacade.SendChatMessageToChat(chatDavadBorisJozko, userDto3Jozko,
				new ChatMessageDTO { Message = "Kedy?" });
			chatMessage1 = chatFacade.GetChatMessageById(chatMsg1);

			chatMessage2 = chatFacade.GetChatMessageById(chatMsg2);
			chatMessage3 = chatFacade.GetChatMessageById(chatMsg3);




		}



		#region ServiceTest

		private static void GroupServiceTest()
		{

		}

		private static void PostServiceTest()
		{
//			postService.GetAllPosts().ResultPosts.ForEach(Console.WriteLine);
//			Console.WriteLine("<<<<<GetChatById>>>>>");
//			var post = postService.GetPostById(1);
//			Console.WriteLine("<<<<<CreatePost>>>>>");
//			var postId =postService.CreatePost(new PostDTO
//			{
//				Message = "Hey buddy, I think you've got the wrong door, the leather club's two blocks down.",
//				Sender = userDtoBoris
//			});
//			var postEnt = postService.GetPostById(postId);
//			Console.WriteLine("<<<<<ListAllPostsfrom user boris>>>>>");
//			postService.GetPostsFromUser(userDtoBoris).ResultPosts.ForEach(Console.WriteLine);
//			Console.WriteLine("<<<<<ListAllCommentsOnPost with is {postDd}(should be 0>>>>>");
//			commentService.GetCommentsForPost(postEnt).ResultPosts.ForEach(Console.WriteLine);
//
//			Console.WriteLine("<<<<<CreateCommentForPostWithId{postId}>>>>>");
//			var commentId=commentService.CreateComment(userDto2David,new CommentDTO{CommentMessage = "Halo"}, postEnt);
//			Console.WriteLine("<<<<<ListAllCommentsOnPost with is {postDd}(should be 1)>>>>>");
//			commentService.GetCommentsForPost(postEnt).ResultPosts.ForEach(Console.WriteLine);
//
//			Console.WriteLine("<<<<<UpdateComment>>>>>");
//			var commentEnt = commentService.GetCommentById(commentId);
//			commentEnt.CommentMessage = "Aloha";
//			commentService.EditCommentMessage(commentEnt);
//			Console.WriteLine("<<<<<ListAllCommentsOnPost with is {postDd}(should be 1)>>>>>");
//
//			commentService.GetCommentsForPost(postEnt).ResultPosts.ForEach(Console.WriteLine);



		}

		private static void ChatServiceTest()
		{

//			Console.WriteLine("<<<<<GetChatById>>>>>");
//			var user1 = userService.GetUserById(1);
//			Console.WriteLine("<<<<<ListAllUsersInTheChat>>>>>");
//
//			chatService.ListAllUsersChats(user1).ResultChats.ForEach(Console.WriteLine);
//
//			Console.WriteLine("<<<<<CreateChat>>>>>");
//
//
//			var chat = new ChatDTO
//			{
//				Name = "chat medzi davadom a patrenkom"
//			};
//			chat.ChatUsers.Add(user1);
//			chat.ChatUsers.Add(userDtoBoris);
//			var id = chatService.CreateChat(chat);
//			chat = chatService.GetChatById(id);
//			chatService.AddUserToChat(chat,userDto2David);
//			Console.WriteLine("<<<<<ListAllChats>>>>>");
//			chatService.ListAllChats().ForEach(Console.WriteLine);
//
//
//			Console.WriteLine("<<<<<ListAllMsgsInChat with is {id}>>>>>");
//			chatService.GetChatMessagesFromChat(chat).ResultMessages.ForEach(Console.WriteLine);
//
//			Console.WriteLine("<<<<<AddNewMsg>>>>>");
//			chatMessageService.PostMessageToChat(chat,userDto2David,new ChatMessageDTO
//			{
//				Message = "ahoj ako sa mas?"
//			});
//
//			Console.WriteLine("<<<<<ListAllMsgsInChat with is {id} again>>>>>");
//
//			chatService.GetChatMessagesFromChat(chat).ResultMessages.ForEach(Console.WriteLine);






		}

		private static void UserServiceTest()
		{

//			#region creatingUsers
//
//			userService.CreateUser(new UserDTO
//			{
//				Name = "David",
//				DateOfBirth = new DateTime(1996, 2, 13),
//				Surname = "Bielik",
//				Information = "NaM SHUTTHEFUCKUPWEEBS NaM",
//				Email = "dvdblk@q.cz",
//				Login = "dvdblk"
//			});
//
//			userService.CreateUser(new UserDTO
//			{
//				Name = "Boris",
//				DateOfBirth = new DateTime(1996, 2, 13),
//				Surname = "Petrenko",
//				Information = "Faggot",
//				Email = "gg@gg.gg",
//				Login = "borenko"
//			});
//			#endregion
//
//			var userListDto = userService.ListUsers(new UserFilter { Name = "Boris" }).ResultUsers.FirstOrDefault();
//			userDtoBoris = userService.GetUserById(userListDto.ID);
//			userDtoBoris.Information = "THANK YOU SIR, MAY I HAVE ANOTHER?";
//			userService.EditUser(userDtoBoris);
//
//
//
//			var userListDto2 = userService.ListUsers(new UserFilter { Name = "David" }).ResultUsers.FirstOrDefault();
//			userDto2David = userService.GetUserById(userListDto2.ID);
//
//			var userListDto3 = userService.ListUsers(new UserFilter { Email = "PogChamp@Kappa.cz" }).ResultUsers.FirstOrDefault();
//			userDto3Alojz = userService.GetUserById(userListDto3.ID);
//
//			userService.AddUsersToFriends(userDto2David, userDtoBoris);
//			userService.AddUsersToFriends(userDto2David.ID, userDto3Alojz.ID);
//
//
//
//
//			Console.WriteLine(userDto2David);
//			Console.WriteLine(userDtoBoris);
//
//			Console.WriteLine("////////////Friends of " + userDto2David.FullName);
//			userService.ListFriends(userDto2David).ForEach(Console.WriteLine);
		}


		#endregion

		
		
	}
}