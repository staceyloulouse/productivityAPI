using AutoMapper;
using SendNotice.Dtos;
using SendNotice.Models;


namespace SendNotice.Profiles
{
    public class SendNoticesProfile : Profile
    {
        public SendNoticesProfile()
        {
            //Source => Target
            CreateMap<Unit, UnitReadDto>();
            CreateMap<Notice, NoticeReadDto>();
            CreateMap<UnitCreateDto, Unit>();
            CreateMap<NoticeCreateDto, Notice>();
            CreateMap<UnitUpdateDto, Unit>();
            CreateMap<Unit,  UnitUpdateDto>();
        }
    }
}