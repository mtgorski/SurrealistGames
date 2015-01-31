using SurrealistGames.GameLogic.Helpers.Interfaces;
using SurrealistGames.Models;
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

        public ReportApiController(IReportHelper reportHelper)
        {
            _reportHelper = reportHelper;
        }

        [Authorize]
        public ReportResponse Post(SurrealistGames.Models.ReportRequest reportRequest)
        {
            return _reportHelper.MakeReport(reportRequest);
        }
    }
}