using AutoMapper;
using BL.DTO;
using BL.DTO.UserDTOs;
using DAL.Entities;

namespace BL
{
	public class InitMapping
	{
		public static void ConfigMapping()
		{
			Mapper.Initialize(c =>
			{
				c.CreateMap<Group, GroupDTO>().ReverseMap();


				c.CreateMap<Post, PostDTO>()
					.ForMember(m => m.GroupId, opt => opt.MapFrom(src => src.Group.ID));

				c.CreateMap<PostDTO, Post>()
					.ForMember(opt => opt.Comments, x => x.Ignore())
					.ForMember(opt => opt.Reactions, x => x.Ignore());


				c.CreateMap<Reaction, ReactionDTO>().ReverseMap();
				c.CreateMap<ChatMessage, ChatMessageDTO>().ReverseMap();


				c.CreateMap<Chat, ChatDTO>();
				c.CreateMap<ChatDTO, Chat>().ForMember(opt => opt.Messages, o => o.Ignore());



				c.CreateMap<Comment, CommentDTO>().ReverseMap();

				c.CreateMap<UserDTO, User>()
					.ForMember(opt => opt.Chats, x => x.Ignore())
					.ForMember(opt => opt.Groups, x => x.Ignore())
					.ForMember(opt => opt.LikedList, x => x.Ignore())
					.ForMember(opt => opt.Posts, x => x.Ignore())
//					.ForMember(opt=>opt.CommentList, x=>x.Ignore())
					;

				c.CreateMap<User, UserDTO>();



				c.CreateMap<Friendship, FriendshipDTO>()
					.ForMember(o => o.User1Id, ops => ops.MapFrom(src => src.User1.ID))
					.ForMember(o => o.User2Id, ops => ops.MapFrom(src => src.User2.ID));


				c.CreateMap<FriendshipDTO, Friendship>()
					.ForMember(o => o.User1, ops => ops.Ignore())
					.ForMember(o => o.User2, ops => ops.Ignore());


			});
		}

	}
}