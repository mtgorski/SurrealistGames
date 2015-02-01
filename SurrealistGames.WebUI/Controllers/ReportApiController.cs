using SurrealistGames.GameLogic.Helpers.Interfaces;
using SurrealistGames.Models;
using SurrealistGames.Models.Interfaces;
using SurrealistGames.WebUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SurrealistGames.WebUI.Controllers
{
    public class ReportApiController : ApiController
    {
        private IReportHelper _reportHelper;
        private IUserUtility _userUtility;
        private IUserInfoRepo _userInfoRepo;

        public ReportApiController(IReportHelper reportHelper, IUserUtility userUtility, IUserInfoRepo userInfoRepo)
        {
            _reportHelper = reportHelper;
            _userUtility = userUtility;
            _userInfoRepo = userInfoRepo;
        }

        [Authorize]
        public ReportResponse Post(SurrealistGames.Models.ReportRequest reportRequest)
        {
            var aspId = _userUtility.GetAspId(this);
            reportRequest.UserInfoId = _userInfoRepo.GetByAspId(aspId).UserInfoId;

            return _reportHelper.MakeReport(reportRequest);
        }
    }
}