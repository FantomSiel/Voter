using AutoMapper;
using System;
using System.Linq;
using Voter.Dal.Models;
using Voter.Dto.Dtos;
using Voter.Dto.Requests.Question;
using Voter.Dto.Requests.Variant;
using Voter.Dto.Responses.Poll;
using Voter.Dto.Responses.Question;

namespace Voter.Service.Mappings
{
    public class PollMappingProfile : Profile
    {
        public PollMappingProfile() : base(nameof(PollMappingProfile))
        {
            CreateMap<QuestionDto, Question>();
            CreateMap<Question, QuestionDto>();

            CreateMap<VariantDto, Variant>();
            CreateMap<Variant, VariantDto>();

            CreateMap<Poll, PollDto>().ForMember(x => x.CreatedBy, act => act.MapFrom(scr => scr.User.Name));
            CreateMap<Poll, GetPollResponse>()
                .ForMember(x => x.CreatedBy, act => act.MapFrom(scr => scr.User.Name))
                .ForMember(x => x.Questions, act => act.MapFrom(scr => scr.Questions.AsEnumerable()));
            CreateMap<User, UserDto>().ForMember(x => x.Id, act => act.MapFrom(scr => scr.UserId));

            CreateMap<Question, GetQuestionResponse>();

            CreateMap<VariantDto, AddVariantRequest>().ForMember(x => x.QuestionId, act => act.MapFrom(scr => new Guid().ToString()));
            CreateMap<QuestionDto, AddQuestionRequest>().ForMember(x => x.PollId, act => act.MapFrom(scr => new Guid().ToString()));
        }
    }
}
