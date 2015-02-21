using AutoMapper;
using SurrealistGames.GameLogic.Helpers.Interfaces;
using SurrealistGames.GameLogic.Utility;
using SurrealistGames.Models;
using SurrealistGames.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.GameLogic.Helpers
{
    public class ReportHelper : IReportHelper
    {
        private readonly IMappingEngine _mapper;
        private readonly IReportRepository _reportRepository;
        private readonly IConfig _config;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;
        
        public ReportHelper(IMappingEngine mappingEngine, IReportRepository reportRepository, IConfig config,
            IQuestionRepository questionRepository, IAnswerRepository answerRepository)
        {
            _mapper = mappingEngine;
            _reportRepository = reportRepository;
            _config = config;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
        }

        public Models.ReportResponse MakeReport(Models.ReportRequest request)
        {
            var report = _mapper.Map<ReportRequest, Report>(request);

            var reports = GetPreviousReports(request);
            var previousNumberOfReports = reports.Distinct(new ReportComparer()).Count(); 

            var numberOfReports = NewNumberOfReports(request, reports, previousNumberOfReports);

            _reportRepository.Save(report);

            DecideToDisable(request, numberOfReports);

            return new ReportResponse 
                        { NumberOfReports = numberOfReports, 
                          ReportsDisabledOn = _config.ReportsDisabledOn,
                          UserHasReportedBefore = previousNumberOfReports == numberOfReports
                        };
        }

        private int NewNumberOfReports(ReportRequest request, List<Report> reports, int previousNumberOfReports)
        {
            var userHasReportedBefore = reports.Any(x => x.UserInfoId == request.UserInfoId);

            return userHasReportedBefore ? previousNumberOfReports : previousNumberOfReports + 1;
        }

        private List<Report> GetPreviousReports(ReportRequest request)
        {
            if( request.AnswerId.HasValue)
            {
                return _reportRepository.GetReportsByAnswerId(request.AnswerId.Value);
            }

            return _reportRepository.GetReportsByQuestionId(request.QuestionId.Value);
        }

        private void DecideToDisable(ReportRequest request, int numberOfReports)
        {
            if (request.QuestionId.HasValue)
            {
                if (numberOfReports >= _config.ReportsDisabledOn)
                {
                    _questionRepository.Disable(request.QuestionId.Value);
                }
            }

            if (request.AnswerId.HasValue)
            {

                if (numberOfReports >= _config.ReportsDisabledOn)
                {
                    _answerRepository.Disable(request.AnswerId.Value);
                }
            }
        }

        private class ReportComparer : IEqualityComparer<Report>
        {

            public bool Equals(Report x, Report y)
            {
                return x.UserInfoId == y.UserInfoId;
            }

            public int GetHashCode(Report obj)
            {
                return 1;
            }
        }

    }
}
