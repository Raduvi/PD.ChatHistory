using AutoMapper;
using PD.ChatHistory.Application.DTOs;
using PD.ChatHistory.Domain.Entities.Chatroom;

namespace PD.ChatHistory.Application.Profiles
{
    public class ChatHistoryMapProfile : Profile
    {
        public ChatHistoryMapProfile()
        {
            CreateMap<ChatRoom, ChatRoomDTO>();
            CreateMap<ChatRoom, ChatRoomHourDTO>()
                .ForMember(dest => dest.HourlyView, opt => opt.MapFrom(src => src.GetHourly()));
            CreateMap<ChatRoom, ChatRoomMinuteDTO>()
                .ForMember(dest => dest.MinutelyView, opt => opt.MapFrom(src => src.GetMinutely()));
        }
    }
}
