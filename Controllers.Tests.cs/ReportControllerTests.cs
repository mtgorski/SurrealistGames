using Moq;
using Ninject.MockingKernel.Moq;
using NUnit.Framework;
using SurrealistGames.WebUI.Controllers;
using SurrealistGames.WebUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurrealistGames.Models;
using System.Net;
using System.Reflection;
using System.Web.Http;
using SurrealistGames.GameLogic.Helpers;
using SurrealistGames.GameLogic.Helpers.Interfaces;

namespace Controllers.Tests.cs
{
    [TestFixture]
    class ReportControllerTests
    {

        [Test]
        public void ReportApiController_HasAuthorizeAttribute()
        {
            var mockReportHelper = new Mock<IReportHelper>();
            var target = new ReportApiController(mockReportHelper.Object);

            Func<ReportRequest, ReportResponse> func = target.Post;

            var attribute = func.Method.GetCustomAttribute(typeof(AuthorizeAttribute));
            Assert.IsNotNull(attribute);
        }

        [Test]
        public void GivenReportRequest_WhenPost_ReturnResultOfReportHelper()
        {
            var mockReportHelper = new Mock<IReportHelper>();
            var expectedResponse = new ReportResponse();
            var givenReportRequest = new ReportRequest();
            mockReportHelper.Setup(m => m.MakeReport(It.Is<ReportRequest>(r => r == givenReportRequest)))
                .Returns(expectedResponse);
            var target = new ReportApiController(mockReportHelper.Object);

            var actualResponse = target.Post(givenReportRequest);

            Assert.AreSame(expectedResponse, actualResponse);
        }
    
    }
}
