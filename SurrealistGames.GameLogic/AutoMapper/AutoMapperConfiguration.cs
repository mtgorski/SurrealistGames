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
            Mapper.CreateMap<ReportRequest, Report>();
        }
    }
}
