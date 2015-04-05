using AutoMapper;
using SurrealistGames.GameLogic.Factories.Interfaces;
using SurrealistGames.GameLogic.Helpers.Interfaces;
using SurrealistGames.GameLogic.Utility;
using SurrealistGames.Models;
using SurrealistGames.Models.Abstract;
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
        private readonly IContentRepositoryFactory _contentRepositoryFactory;

        private IContentRepository _contentRepository;
        
        public ReportHelper(IMappingEngine mappingEngine, IReportRepository reportRepository, IConfig config,
             IContentRepositoryFactory contentRepositoryFactory)
        {
            _mapper = mappingEngine;
            _reportRepository = reportRepository;
            _config = config;
            _contentRepositoryFactory = contentRepositoryFactory;
        }

        #region MakeReport
        public Models.ReportResponse MakeReport(Models.ReportRequest request)
        {
            var report = _mapper.Map<ReportRequest, Report>(request);

            _contentRepository = GetContentRepositoryFor(request);

            var reports = GetPreviousReports(request);
            var previousNumberOfReports = reports.Distinct(new ReportComparer()).Count(); 

            var newNumberOfReports = NewNumberOfReports(request, reports, previousNumberOfReports);

            if(previousNumberOfReports == newNumberOfReports)
            {
                return new ReportResponse
                {
                    NumberOfReports = newNumberOfReports,
                    ReportsDisabledOn = _config.ReportsDisabledOn,
                    UserHasReportedBefore = true,
                    ContentIsModeratorApproved = ContentIsModeratorApproved(request)
                };
            }

            _reportRepository.Save(report);

            var contentIsModeratorApproved = ContentIsModeratorApproved(request);

            if(!contentIsModeratorApproved)
            {
                DecideToDisable(request, newNumberOfReports);
            }

            return new ReportResponse 
                        { NumberOfReports = newNumberOfReports, 
                          ReportsDisabledOn = _config.ReportsDisabledOn,
                          UserHasReportedBefore = false,
                          ContentIsModeratorApproved = contentIsModeratorApproved
                        };
        }

        private IContentRepository GetContentRepositoryFor(ReportRequest request)
        {
            if(request.AnswerId.HasValue)
            {
                return _contentRepositoryFactory.GetRepositoryFor<Answer>();
            }

            return _contentRepositoryFactory.GetRepositoryFor<Question>();
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

        private bool ContentIsModeratorApproved(ReportRequest request)
        {
            var content = _contentRepository.GetContentById(request.ContentId);

            return content.IsApproved;
        }

        private void DecideToDisable(ReportRequest request, int numberOfReports)
        {
            if (numberOfReports >= _config.ReportsDisabledOn)
            {
                _contentRepository.Disable(request.ContentId);
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
        #endregion

        public IEnumerable<Content> GetTopReportedAndUnmoderatedContent<T>(int numberOfResults) where T : Content
        {
            var repository = _contentRepositoryFactory.GetRepositoryFor<T>();

            return repository.GetTopReportedAndUnmoderatedContent(numberOfResults);
        }
    }
}
