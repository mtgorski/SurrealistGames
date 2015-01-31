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
            _reportRepository.Save(report);

            int numberOfReports = GetNumberOfReports(request);
            DecideToDisable(request, numberOfReports);

            return new ReportResponse { NumberOfReports = numberOfReports, ReportsDisabledOn = _config.ReportsDisabledOn } ;
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
                numberOfReports = _reportRepository.GetReportsByAnswerId(request.AnswerId.Value).Count;

                if (numberOfReports >= _config.ReportsDisabledOn)
                {
                    _answerRepository.Disable(request.AnswerId.Value);
                }
            }
        }

        private int GetNumberOfReports(ReportRequest request)
        {
            if (request.QuestionId.HasValue)
            {
                return _reportRepository.GetReportsByQuestionId(request.QuestionId.Value).Count;
            }

            return _reportRepository.GetReportsByAnswerId(request.AnswerId.Value).Count;
        }
    }
}
