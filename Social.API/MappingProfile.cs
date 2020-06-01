using AutoMapper;
using Social.API.Dtos;
using Social.API.Models;
using Social.API.Models.Fake;

public class MappingProfile : Profile 
{
    public MappingProfile() {
        // Add as many of these lines as you need to map your objects

        CreateMap<User, UserForReturnDto>().ReverseMap();
        CreateMap<User, UserForCreateDto>().ReverseMap();

        CreateMap<Conversation,ConversationForReturnDto>().ReverseMap();

        CreateMap<Post,PostForReturnDto>().ReverseMap();

        CreateMap<UserConversator,UserConversatorForReturnDto>().ReverseMap();

        CreateMap<Like, LikeForReturnDto>().ReverseMap();

        CreateMap<Comment,CommentForReturnDto>().ReverseMap();
        CreateMap<Comment,CommentToCreateDto>().ReverseMap();
        
        CreateMap<Message, MessageForReturnDto>().ReverseMap();
    }
}