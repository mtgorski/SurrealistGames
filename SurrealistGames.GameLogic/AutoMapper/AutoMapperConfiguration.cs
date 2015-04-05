using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SurrealistGames.Models;

namespace SurrealistGames.GameLogic.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static void Register()
        {
            Mapper.CreateMap<ReportRequest, Report>()
                .ForMember(d => d.SubmittedOn, o => o.MapFrom(s => DateTime.UtcNow));

            Mapper.CreateMap<Question, Question>();
            Mapper.CreateMap<Answer, Answer>();
        }
    }
}
