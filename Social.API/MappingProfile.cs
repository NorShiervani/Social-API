using AutoMapper;
using Social.API.Dtos;
using Social.API.Models;
using Social.API.Models.Fake;

public class MappingProfile : Profile 
{
    public MappingProfile() {
        // Add as many of these lines as you need to map your objects

        CreateMap<UserForReturnDto,User>().ReverseMap();

        CreateMap<User,UserForCreateDto>().ReverseMap();

        CreateMap<Conversation,ConversationForReturnDto>().ReverseMap();

        CreateMap<Post,PostForReturnDto>().ReverseMap();

        CreateMap<UserConversator,UserConversatorForReturnDto>().ReverseMap();

        CreateMap<LikeForReturnDto,Like>().ReverseMap();

        CreateMap<Comment,CommentForReturnDto>().ReverseMap();
        
        CreateMap<Message, MessageForReturnDto>().ReverseMap();
    }
}