using AutoMapper;
using Social.API.Dtos;
using Social.API.Models;
using Social.API.Models.Fake;

public class MappingProfile : Profile 
{
    public MappingProfile() {
        // Add as many of these lines as you need to map your objects
        CreateMap<UserForReturnDto,User>();
        CreateMap<User,UserForReturnDto>();
        CreateMap<Conversation,ConversationForReturnDto>();
        CreateMap<ConversationForReturnDto,Conversation>();
        CreateMap<Post,PostForReturnDto>();
        CreateMap<PostForReturnDto,Post>();
        CreateMap<UserConversator,UserConversatorForReturnDto>();
        CreateMap<UserConversatorForReturnDto,UserConversator>();
        CreateMap<LikeForReturnDto,Like>();
        CreateMap<Like,LikeForReturnDto>();
        CreateMap<Comment,CommentForReturnDto>();
        CreateMap<CommentForReturnDto,Comment>();
        CreateMap<Message, MessageForReturnDto>();
        CreateMap<MessageForReturnDto,Message>();
    }
}